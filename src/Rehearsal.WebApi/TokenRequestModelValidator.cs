using System.Net;
using FluentValidation;
using Rehearsal.Messages;

namespace Rehearsal.WebApi
{
    public class TokenRequestModelValidator : AbstractValidator<TokenRequestModel>
    {
        public TokenRequestModelValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
        }
    }
}