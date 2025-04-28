using Flunt.Notifications;
using Flunt.Validations;

namespace AuthJwt.Core.Contexts.AccountContext.UseCases.Authenticate;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request) 
        => new Contract<Notification>()
            .Requires()
            .IsLowerThan(request.Password.Length, 40, "Password",
                "Senha deve conter no máximo 40 caracteres")
            .IsGreaterThan(request.Password.Length, 8, "Password",
                "Senha deve conter no mínimo 8 caracteres")
            .IsEmail(request.Email, "Email", "Email inválido");
}
