using Entities.DTOs;
using FluentValidation;

namespace CoffeeClub.Validators
{
    public class UpdateCommentValidator : AbstractValidator<CommentsForCreationDTO>
    {
        public UpdateCommentValidator()
        {
            RuleFor(x => x.Comment).NotNull().NotEmpty().WithMessage("Comment is required.");
            RuleFor(x => x.Comment).MinimumLength(8);
            RuleFor(x => x.Comment).MaximumLength(100);
            RuleFor(x => x.Rating).NotNull().NotEmpty().WithMessage("Rating is required.");
            RuleFor(x => x.Rating).GreaterThan(0);
            RuleFor(x => x.Rating).LessThan(6);
            RuleFor(x => x.DateCreated).NotNull().NotEmpty().WithMessage("DateCreated is required.");
        }
    }
}
