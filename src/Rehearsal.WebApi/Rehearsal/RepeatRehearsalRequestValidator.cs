using System;
using FluentValidation;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.WebApi.Rehearsal
{
    public class RepeatRehearsalRequestValidator : AbstractValidator<RepeatRehearsalRequest>
    {
        public IRehearsalSessionRepository RehearsalSessionRepository { get; set; }
        
        public RepeatRehearsalRequestValidator(IRehearsalSessionRepository rehearsalSessionRepository)
        {
            RehearsalSessionRepository = rehearsalSessionRepository;

            RuleFor(x => x.RehearsalId).Must(RehearsalExists).WithMessage("Rehearsal does not exist");
        }

        private bool RehearsalExists(Guid id) => RehearsalSessionRepository.GetById(id).IsSome;
        
    }
}