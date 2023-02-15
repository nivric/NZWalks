using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validators
{
    public class AddWalkRequestValidator : AbstractValidator<AddWalkRequest>
    {
        public AddWalkRequestValidator()
        {
            RuleFor(req => req.Name).NotEmpty();

            RuleFor(req => req.Length).GreaterThan(0);

            RuleFor(req => req.WalkDifficultyId).NotEmpty();

            RuleFor(req => req.RegionId).NotEmpty();
        }
    }
}
