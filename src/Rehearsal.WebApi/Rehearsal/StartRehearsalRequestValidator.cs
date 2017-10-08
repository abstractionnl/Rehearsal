using System;
using FluentValidation;
using Rehearsal.Messages.Rehearsal;
using Rehearsal.WebApi.QuestionList;

namespace Rehearsal.WebApi.Rehearsal
{
    public class StartRehearsalRequestValidator : AbstractValidator<StartRehearsalRequest>
    {
        public StartRehearsalRequestValidator(IQuestionListRepository questionListRepository)
        {
            QuestionListRepository = questionListRepository;

            RuleFor(x => x.QuestionListId).Must(QuestionListExists).WithMessage("QuestionList does not exist");
        }
        
        private IQuestionListRepository QuestionListRepository { get; }

        private bool QuestionListExists(Guid id) => QuestionListRepository.GetById(id).IsSome;
    }
}