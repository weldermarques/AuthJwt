using AuthJwt.Core.Contexts.AccountContext.Entities;
using AuthJwt.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using MediatR;

namespace AuthJwt.Core.Contexts.AccountContext.UseCases.Authenticate;

public class Handler(IRepository repository) : IRequestHandler<Request, Response>
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
        
        #region Find User

        User? user;
        try
        {
            user = await repository.GetUserByEmailAsync(request.Email, cancellationToken);
            if(user is null)
                return new Response("Usuário não encontrado", 404);
        }
        catch (Exception e)
        {
            return new Response(e.Message, 400);
        }

        #endregion

        #region Password validation

        if (!user.Password.Challenge(request.Password))
            return new Response("Usuário ou senha inválida", 400);

        #endregion

        #region User Validation

        try
        {
            if (!user.Email.Verification.IsActive)
                return new Response("Usuário inativo", 400);
        }
        catch
        {
            return new Response("Erro ao verificar usuário", 500);
        }

        #endregion

        #region Create Response

        try
        {
            var data = new ResponseData
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                Roles = user.Roles.Select(x => x.Name).ToArray()
            };
            
            return new Response(string.Empty, data);
        }
        catch
        {
            return new Response("Erro ao obter dados usuário", 500);
        }

        #endregion
    }
}
