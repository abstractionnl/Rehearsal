using FluentValidation;
using Rehearsal.Messages;

namespace Rehearsal.WebApi
{
    public class QuestionListPropertiesValidator : AbstractValidator<QuestionListProperties>
    {
        public QuestionListPropertiesValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.QuestionTitle).NotEmpty();
            RuleFor(x => x.AnswerTitle).NotEmpty();
            RuleFor(x => x.Questions).NotNull().SetCollectionValidator(new ItemValidator());
        }

        private class ItemValidator : AbstractValidator<QuestionListProperties.Item>
        {
            public ItemValidator()
            {
                RuleFor(x => x.Question).NotEmpty();
                RuleFor(x => x.Answer).NotEmpty();
            }
        }
    }
}