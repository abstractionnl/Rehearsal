using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Rehearsal.Messages;
using Rehearsal.Messages.QuestionList;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Tests
{
    public static class FakerExtensions
    {
        public static QuestionListProperties QuestionListProperties(this Faker faker, int questionCount = 3) => new QuestionListProperties()
        {
            Title = faker.Lorem.Word(),
            QuestionTitle = faker.Lorem.Word(),
            AnswerTitle = faker.Lorem.Word(),
            Questions = Enumerable.Range(0, questionCount).Select(_ => faker.Question()).ToList()
        };

        public static QuestionModel Question(this Faker faker, string question=null, string answer=null) => new QuestionModel()
        {
            Question = question ?? faker.Lorem.Word(),
            Answer = answer ?? faker.Lorem.Word()
        };

        public static QuestionListModel QuestionList(this Faker faker, int questionCount = 3) => 
            faker.QuestionList(
                Enumerable.Range(0, questionCount).Select(_ => faker.Question()).ToArray()
            );
        
        public static QuestionListModel QuestionList(this Faker faker, params QuestionModel[] questions) => new QuestionListModel()
        {
            Title = faker.Lorem.Word(),
            QuestionTitle = faker.Lorem.Word(),
            AnswerTitle = faker.Lorem.Word(),
            Questions = questions,
            Version = 1
        };

        public static OpenRehearsalQuestionModel OpenRehearsalQuestion(this Faker faker) =>
            new OpenRehearsalQuestionModel()
            {
                Id = Guid.NewGuid(),
                QuestionTitle = faker.Lorem.Word(),
                Question = faker.Lorem.Sentence(),
                AnswerTitle = faker.Lorem.Word(),
                CorrectAnswers = new [] { faker.Lorem.Sentence() }
            };
        
        public static OpenRehearsalQuestionModel OpenRehearsalQuestion(this Faker faker, params string[] correctAnswers) =>
            new OpenRehearsalQuestionModel()
            {
                Id = Guid.NewGuid(),
                QuestionTitle = faker.Lorem.Word(),
                Question = faker.Lorem.Sentence(),
                AnswerTitle = faker.Lorem.Word(),
                CorrectAnswers = correctAnswers
            };
    }
}