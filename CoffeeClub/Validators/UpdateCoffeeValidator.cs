using Entities.DTOs;
using FluentValidation;

namespace CoffeeClub.Validators
{
    public class UpdateCoffeeValidator : AbstractValidator<CoffeeForCreationDTO>
    {
        public UpdateCoffeeValidator()
        {
            RuleFor(x => x.CoffeeName).NotNull().NotEmpty().WithMessage("CoffeeName is required.");
            RuleFor(x => x.CoffeeName).MinimumLength(8);
            RuleFor(x => x.CoffeeName).MaximumLength(20);
            RuleFor(x => x.CoffeePrice).NotNull().NotEmpty().WithMessage("CoffeePrice is required.");
            RuleFor(x => x.CoffeePrice).GreaterThan(0);
            RuleFor(x => x.CoffeePrice).LessThan(100.00);
            RuleFor(x => x.CountryOfOrigin).NotNull().NotEmpty().WithMessage("CountryOfOrigin is required.");
            RuleFor(x => x.CountryOfOrigin).MinimumLength(3);
            RuleFor(x => x.CountryOfOrigin).MaximumLength(20);
        }
    }
}
