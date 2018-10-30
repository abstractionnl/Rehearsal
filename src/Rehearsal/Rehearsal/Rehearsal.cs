using System;
using System.Collections.Generic;
using System.Linq;
using CQRSlite.Domain;
using Rehearsal.Common;
using Rehearsal.Messages;
using Rehearsal.Messages.QuestionList;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Rehearsal
{
    public class Rehearsal : AggregateRoot
    {
        [Obsolete("For CQRS")]
        private Rehearsal() {}

        private ICollection<RehearsalQuestionModel> Questions { get; set; }
        
        public Rehearsal(Guid id, ICollection<RehearsalQuestionModel> questions)
        {
            Id = id;
            
            ApplyChange(new RehearsalStartedEvent()
            {
                Questions = questions
            });
        }

        public void GiveAnswer(Guid questionId, AnswerResultModel result)
        {
            if (!Questions.Any(x => x.Id == questionId))
                throw new InvalidOperationException("Question does not exist");
            
            ApplyChange(new QuestionAnsweredEvent()
            {
                QuestionId = questionId,
                GivenAnswer = result.GivenAnswer,
                IsCorrect = result.IsCorrect
            });
        }

        public void Apply(RehearsalStartedEvent @event)
        {
            Questions = @event.Questions;
        }

        public static Rehearsal CreateFrom(Guid newRehearsalId, Rehearsal rehearsal)
        {
            var randomizer = new Randomizer();
            return new Rehearsal(newRehearsalId, randomizer.Randomize(rehearsal.Questions).ToList());
        }
    }
}