using FluentValidation;
using TestUCondo.Application.Queries.AccountModule.Query;

namespace TestUCondo.Application.Queries.AccountModule.Validations
{
    public class GetAccountByIdQueryValidator : AbstractValidator<GetAccountByIdQuery>
    {
        public GetAccountByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("O Id deve ser maior que zero.");
        }
    }
}
