using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Rehearsal.Messages;
using Rehearsal.Messages.QuestionList;

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
    }
}