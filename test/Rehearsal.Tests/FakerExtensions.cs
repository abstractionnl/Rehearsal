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

        public static QuestionModel Question(this Faker faker) => new QuestionModel()
        {
            Question = faker.Lorem.Word(),
            Answer = faker.Lorem.Word()
        };

        public static QuestionListModel QuestionList(this Faker faker, int questionCount = 3) => new QuestionListModel()
        {
            Title = faker.Lorem.Word(),
            QuestionTitle = faker.Lorem.Word(),
            AnswerTitle = faker.Lorem.Word(),
            Questions = Enumerable.Range(0, questionCount).Select(_ => faker.Question()).ToList(),
            Version = 1
        };

        public static OpenRehearsalQuestionModel OpenRehearsalQuestion(this Faker faker) =>
            new OpenRehearsalQuestionModel()
            {
                Id = Guid.NewGuid(),
                QuestionTitle = faker.Lorem.Word(),
                Question = faker.Lorem.Sentence(),
                AnswerTitle = faker.Lorem.Word(),
                CorrectAnswer = faker.Lorem.Sentence()
            };
    }
}