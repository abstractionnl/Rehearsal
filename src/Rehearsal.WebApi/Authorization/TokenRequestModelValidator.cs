using FluentValidation;
using Rehearsal.Messages.Authorization;

namespace Rehearsal.WebApi.Authorization
{
    public class TokenRequestModelValidator : AbstractValidator<TokenRequestModel>
    {
        public TokenRequestModelValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
        }
    }
}