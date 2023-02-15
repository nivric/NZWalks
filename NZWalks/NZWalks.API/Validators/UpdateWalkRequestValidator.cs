using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validators
{
    public class UpdateWalkRequestValidator : AbstractValidator<UpdateWalkRequest>
    {
        public UpdateWalkRequestValidator()
        {
            RuleFor(req => req.Name).NotEmpty();

            RuleFor(req => req.Length).GreaterThan(0);

            RuleFor(req => req.WalkDifficultyId).NotEmpty();
        }
    }
}
