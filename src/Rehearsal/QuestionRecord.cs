using LanguageExt;

namespace Rehearsal
{
    public class QuestionRecord : Record<QuestionRecord>
    {
        public string Question { get; private set; }
        public string Answer { get; private set; }

        public static QuestionRecord Create(string question, string answer) => new QuestionRecord
        {
            Question = question,
            Answer = answer
        };
    }
}