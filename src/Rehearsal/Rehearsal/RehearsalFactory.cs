using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rehearsal.Common;
using Rehearsal.Messages.QuestionList;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Rehearsal
{
    public class RehearsalFactory : IRehearsalFactory
    {
        private IQuestionGenerator QuestionGenerator { get; set; }
        
        public RehearsalFactory()
        {
            Questions = new List<QuestionDefinition>();
            QuestionGenerator = new OpenRehearsalQuestionGenerator();
        }

        private IList<QuestionDefinition> Questions { get; }
        
        public Task<StartRehearsalCommand> Create()
        {
            var id = Guid.NewGuid();

            var cmd = new StartRehearsalCommand()
            {
                Id = id,
                Questions = Questions.Randomize().Select(PrepareQuestion).ToList()
            };
            
            return Task.FromResult(cmd);
        }

        private RehearsalQuestionModel PrepareQuestion(QuestionDefinition question) => 
            QuestionGenerator.PrepareQuestion(question);

        public IRehearsalFactory AddQuestionList(QuestionListModel questionList)
        {
            foreach (var question in questionList.Questions)
            {
                GetOrAddQuestion(questionList.QuestionTitle, question.Question, questionList.AnswerTitle)
                    .AddAnswer(question.Answer);
            }

            return this;
        }

        public IRehearsalFactory UseOpenQuestions()
        {
            QuestionGenerator = new OpenRehearsalQuestionGenerator();

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
                Answers = new HashSet<string>();
            }

            public void AddAnswer(string answer)
            {
                Answers.Add(answer);
            }

            public string QuestionTitle { get; }
            public string Question { get; }
            public string AnswerTitle { get; }
            public ISet<string> Answers { get; }
        }

        private class OpenRehearsalQuestionGenerator : IQuestionGenerator
        {
            public RehearsalQuestionModel PrepareQuestion(QuestionDefinition question)
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
        }
        
        private interface IQuestionGenerator
        {
            RehearsalQuestionModel PrepareQuestion(QuestionDefinition question);
        }
    }
}