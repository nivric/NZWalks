using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validators
{
    public class UpdateRegionRequestValidator : AbstractValidator<UpdateRegionRequest>
    {
        public UpdateRegionRequestValidator()
        {
            RuleFor(req => req.Area).GreaterThan(0);
            RuleFor(req => req.Name).NotEmpty();
            RuleFor(req => req.Code).NotEmpty();
            RuleFor(req => req.Population).GreaterThanOrEqualTo(0);
        }
    }
}
