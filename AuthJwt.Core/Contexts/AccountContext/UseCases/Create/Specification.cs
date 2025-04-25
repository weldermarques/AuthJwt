using Flunt.Notifications;
using Flunt.Validations;

namespace AuthJwt.Core.Contexts.AccountContext.UseCases.Create;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsLowerThan(request.Name.Length, 160, "Name", 
                "Nome deve conter no máximo 160 caracteres")
            .IsGreaterThan(request.Name.Length, 3, "Name", 
                "Nome deve conter no mínimo 3 caracteres")
            .IsLowerThan(request.Password.Length, 40, "Password",
                "Senha deve conter no máximo 40 caracteres")
            .IsGreaterThan(request.Password.Length, 8, "Password",
                "Senha deve conter no mínimo 8 caracteres")
            .IsEmail(request.Email, "Email", "Email inválido");
}
