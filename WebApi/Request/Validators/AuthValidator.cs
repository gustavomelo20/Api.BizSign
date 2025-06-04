using FluentValidation;

namespace Api.BizSign.WebApi.Request.Validators;

public class AuthValidator : AbstractValidator<AuthRequest>
{
    public AuthValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O campo Email é obrigatório.")
            .EmailAddress().WithMessage("Email inválido.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.");
    }
}