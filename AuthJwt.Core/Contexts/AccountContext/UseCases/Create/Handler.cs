using AuthJwt.Core.Contexts.AccountContext.Entities;
using AuthJwt.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using AuthJwt.Core.Contexts.AccountContext.ValueObjects;

namespace AuthJwt.Core.Contexts.AccountContext.UseCases.Create;

public class Handler
{
    private readonly IRepository _repository;
    private readonly IService _service;

    public Handler(IRepository repository, IService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        try
        {
            var result = Specification.Ensure(request);
            if(result.IsValid)
                return new Response("Requisição inválida", 400, result.Notifications);
        }
        catch
        {
            return new Response("Erro ao processar a requisição", 400);
        }
        
        User user;
        try
        {
            user = new User(request.Name, request.Email, request.Password);
        }
        catch (Exception e)
        {
            return new Response(e.Message, 400);
        }

        try
        {
            var exists = await _repository.AnyAsync(request.Email, cancellationToken);

            if (exists)
                return new Response("Este e-mail está em uso", 400);
        }
        catch
        {
            return new Response("Erro ao verificar e-mail cadastrado", 500);
        }
        
        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Erro ao adicionar conta", 500);
        }
        
        try
        {
            await _service.SendVerificationEmailAsync(user, cancellationToken);
        }
        catch
        {
            //Implementar log
        }
        
        return new Response("Conta criada", new ResponseData(user.Id, user.Name, user.Email));
    }
}
