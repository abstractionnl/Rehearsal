using System;
using System.Collections.Generic;
using System.Linq;
using Rehearsal.Common;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Rehearsal
{
    public class RehearsalFactory
    {
        private IQuestionGenerator QuestionGenerator { get; set; }
        private Randomizer _randomizer;
        
        public RehearsalFactory()
        {
            Questions = new List<QuestionDefinition>();
            QuestionGenerator = new OpenRehearsalQuestionGenerator();

            _randomizer = new Randomizer();
        }

        private IList<QuestionDefinition> Questions { get; }
        
        public IList<RehearsalQuestionModel> Create()
        {
            return _randomizer.Randomize(Questions).Select(PrepareQuestion).ToList();
        }

        private RehearsalQuestionModel PrepareQuestion(QuestionDefinition question) => 
            QuestionGenerator.PrepareQuestion(question);

        public RehearsalFactory AddQuestionList(QuestionList.QuestionList questionList)
        {
            foreach (var question in questionList.Questions)
            {
                GetOrAddQuestion(questionList.QuestionTitle, question.Question, questionList.AnswerTitle)
                    .AddAnswer(question.Answer);
            }

            return this;
        }

        public RehearsalFactory UseOpenQuestions()
        {
            QuestionGenerator = new OpenRehearsalQuestionGenerator();

            return this;
        }

        public RehearsalFactory UseMultipleChoiceQuestions(int answerNumber)
        {
            QuestionGenerator = new MultipleChoiceQuestionGenerator(answerNumber, Questions, _randomizer);

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

        private class MultipleChoiceQuestionGenerator : IQuestionGenerator
        {
            private readonly int _answerNumber;
            private readonly IList<QuestionDefinition> _allQuestions;
            private readonly Randomizer _randomizer;

            public MultipleChoiceQuestionGenerator(int answerNumber, IList<QuestionDefinition> allQuestions, Randomizer randomizer)
            {
                if (answerNumber < 2)
                    throw new ArgumentOutOfRangeException(nameof(answerNumber), "multiple choice questions must have at least two answers");
                
                _answerNumber = answerNumber;
                _allQuestions = allQuestions ?? throw new ArgumentNullException(nameof(allQuestions));
                _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
            }

            public RehearsalQuestionModel PrepareQuestion(QuestionDefinition question)
            {
                var correctAnswer = _randomizer.PickRandom(question.Answers);
                var incorrectAnswers = _randomizer.Randomize(_allQuestions.SelectMany(x => x.Answers))
                    .Where(x => !question.Answers.Contains(x))
                    .Take(_answerNumber - 1);

                var allAnswers = _randomizer.Randomize(incorrectAnswers.Concat(new[] { correctAnswer })).ToList();
                var correctAnswerIndex = allAnswers.IndexOf(correctAnswer);
                
                return new MultipleChoiceQuestionModel()
                {
                    Id = Guid.NewGuid(),
                    QuestionTitle = question.QuestionTitle,
                    Question = question.Question,
                    AnswerTitle = question.AnswerTitle,
                    AvailableAnswers = allAnswers,
                    CorrectAnswer = correctAnswerIndex
                };
            }
        }
        
        private interface IQuestionGenerator
        {
            RehearsalQuestionModel PrepareQuestion(QuestionDefinition question);
        }
    }
}