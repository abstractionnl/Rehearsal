using FluentValidation;
using Rehearsal.Messages.QuestionList;

namespace Rehearsal.WebApi.QuestionList
{
    public class QuestionListPropertiesValidator : AbstractValidator<QuestionListProperties>
    {
        public QuestionListPropertiesValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.QuestionTitle).NotEmpty();
            RuleFor(x => x.AnswerTitle).NotEmpty();
            RuleFor(x => x.Questions).NotNull().SetCollectionValidator(new QuestionModelValidator());
        }

        private class QuestionModelValidator : AbstractValidator<QuestionModel>
        {
            public QuestionModelValidator()
            {
                RuleFor(x => x.Question).NotEmpty();
                RuleFor(x => x.Answer).NotEmpty();
            }
        }
    }
}