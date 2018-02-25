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
                CorrectAnswers = question.Answers.ToList()
            };
        }

        public IRehearsalFactory AddQuestionList(QuestionListModel questionList)
        {
            foreach (var question in questionList.Questions)
            {
                GetOrAddQuestion(questionList.QuestionTitle, question.Question, questionList.AnswerTitle)
                    .AddAnswer(question.Answer);
            }

            return this;
        }

        private QuestionDefinition GetOrAddQuestion(string questionTitle, string question, string answerTitle) => 
            Questions
                .Where(q => q.Question == question && q.QuestionTitle == questionTitle && q.AnswerTitle == answerTitle)
                .HeadOrNone()
                .IfNone(() =>
                {
                    var q = new QuestionDefinition(
                        questionTitle,
                        question,
                        answerTitle
                    );
                    
                    Questions.Add(q);
    
                    return q;
                });

        private class QuestionDefinition
        {
            public QuestionDefinition(string questionTitle, string question, string answerTitle)
            {
                QuestionTitle = questionTitle;
                Question = question;
                AnswerTitle = answerTitle;
                Answers = new List<string>();
            }

            public void AddAnswer(string answer)
            {
                Answers.Add(answer);
            }

            public string QuestionTitle { get; }
            public string Question { get; }
            public string AnswerTitle { get; }
            public ICollection<string> Answers { get; }
        }
    }
}