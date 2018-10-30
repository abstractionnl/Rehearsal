using System;
using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages.Rehearsal
{
    [JsonDescriminator("Type", "Model")]
    public abstract class RehearsalQuestionModel
    {
        public Guid Id { get; set; }
        public string Type => GetType().Name.Replace("Model", "");
        public string QuestionTitle { get; set; }
        public string Question { get; set; }
        public string AnswerTitle { get; set; }
        public string GivenAnswer { get; set; }
        public bool AnsweredCorrectly { get; set; }
    }
}