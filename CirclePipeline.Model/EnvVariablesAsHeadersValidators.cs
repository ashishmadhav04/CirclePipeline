using FluentValidation;

namespace CirclePipeline.Model
{
    public class EnvVariablesAsHeadersValidators : AbstractValidator<EnvVariablesAsHeaders>
    {
        public EnvVariablesAsHeadersValidators()
        {
            RuleFor(x => x.LOADTEST_ENVIRONMENT)
                .NotNull()
                .WithMessage("Load test environment has to be set as either Test or Beta")
                .Must(x => x.ToLower().Equals("beta") || x.ToLower().Equals("test"))
                .WithMessage("Load test environment has to be set as either Test or Beta");

            RuleFor(x => x.LOADTEST_MAX_TARGET_DURATION)
                .Must(x => x.EndsWith("h") || x.EndsWith("s") || x.EndsWith("m"))
                .WithMessage("Maximum Target Duration value is incorrect. Please set values ending with h(hour), m(minute), s(second).");

            RuleFor(x => x.LOADTEST_DURATION_PER_STAGE)
                .Must(x => x.EndsWith("h") || x.EndsWith("s") || x.EndsWith("m"))
                .WithMessage("Test Duration value is incorrect. Please set values ending with h(hour), m(minute), s(second).");

            RuleFor(x => x.LOADTEST_MAXVU)
                .NotNull()
                .WithMessage("Enter a numeric value for Load test Max VUs")
                .InclusiveBetween(1, 250)
                .WithMessage("Enter a numeric value for Load test Max VUs between 1 and 250");

            RuleFor(x => x.LOADTEST_MAX_TPS)
                .NotNull()
                .WithMessage("Enter a numeric value for Load test Max TPS")
                .InclusiveBetween(1, 1000)
                .WithMessage("Enter a numeric value for Load test Max TPS between 1 and 1000");

            RuleFor(x => x.LOADTEST_STAGES)
                .NotNull()
                .WithMessage("Enter a numeric value for Load test stages")
                .InclusiveBetween(1, 1000)
                .WithMessage("Enter a numeric value for Load test stages between 1 and 1000");
        }
    }
}
