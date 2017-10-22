using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSlite.Commands;
using Rehearsal.Messages;
using Rehearsal.Messages.QuestionList;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Rehearsal
{
    public class RehearsalFactory : IRehearsalFactory
    {
        public RehearsalFactory()
        {
            Questions = new List<QuestionDefinition>();
        }

        private IList<QuestionDefinition> Questions { get; }
        
        public Task<StartRehearsalCommand> Create()
        {
            var id = Guid.NewGuid();

            var cmd = new StartRehearsalCommand()
            {
                Id = id,
                Questions = Questions.Select(PrepareQuestion).ToList()
            };
            
            return Task.FromResult(cmd);
        }

        private RehearsalQuestionModel PrepareQuestion(QuestionDefinition question)
        {
            return new OpenRehearsalQuestionModel()
            {
                Id = Guid.NewGuid(),
                QuestionTitle = question.QuestionTitle,
                Question = question.Question,
                AnswerTitle = question.AnswerTitle,
                CorrectAnswer = question.Answer
            };
        }

        public IRehearsalFactory AddQuestionList(QuestionListModel questionList)
        {
            foreach (var question in questionList.Questions)
            {
                Questions.Add(new QuestionDefinition(
                    questionList.QuestionTitle,
                    question.Question,
                    questionList.AnswerTitle,
                    question.Answer
                ));
            }

            return this;
        }

        public class QuestionDefinition
        {
            public QuestionDefinition(string questionTitle, string question, string answerTitle, string answer)
            {
                QuestionTitle = questionTitle;
                Question = question;
                AnswerTitle = answerTitle;
                Answer = answer;
            }

            public string QuestionTitle { get; }
            public string Question { get; }
            public string AnswerTitle { get; }
            public string Answer { get; }
        }
    }
}