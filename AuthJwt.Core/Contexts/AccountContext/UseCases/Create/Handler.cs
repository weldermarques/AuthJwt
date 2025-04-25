using AuthJwt.Core.Contexts.AccountContext.Entities;
using AuthJwt.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using AuthJwt.Core.Contexts.AccountContext.ValueObjects;
using MediatR;

namespace AuthJwt.Core.Contexts.AccountContext.UseCases.Create;

public class Handler(IRepository repository, IService service) : IRequestHandler<Request, Response>
{
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region Request Validation

        try
        {
            var result = Specification.Ensure(request);
            if(!result.IsValid)
                return new Response("Requisição inválida", 400, result.Notifications);
        }
        catch
        {
            return new Response("Erro ao processar a requisição", 400);
        }

        #endregion

        #region Create Object

        User user;
        try
        {
            user = new User(request.Name, request.Email, request.Password);
        }
        catch (Exception e)
        {
            return new Response(e.Message, 400);
        }

        #endregion

        #region Verify Email Exist

        try
        {
            var exists = await repository.AnyAsync(request.Email, cancellationToken);

            if (exists)
                return new Response("Este e-mail está em uso", 400);
        }
        catch
        {
            return new Response("Erro ao verificar e-mail cadastrado", 500);
        }

        #endregion

        #region Save User

        try
        {
            await repository.SaveAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Erro ao adicionar conta", 500);
        }

        #endregion

        #region Send verification code

        try
        {
            await service.SendVerificationEmailAsync(user, cancellationToken);
        }
        catch
        {
            //Implementar log
        }

        #endregion
        
        return new Response("Conta criada", new ResponseData(user.Id, user.Name, user.Email));
    }
}
