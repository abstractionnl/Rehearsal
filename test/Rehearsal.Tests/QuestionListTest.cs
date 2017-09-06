using NFluent;
using System;
using Xunit;

namespace Rehearsal.Tests
{
    /* TODO: Refactor tests to test CQRS events
    public class QuestionListTest
    {
        protected QuestionList QuestionList;

        public class GivenNewQuestionList : QuestionListTest
        {
            public GivenNewQuestionList()
            {
                QuestionList = CreateQuestionList();
            }

            [Fact] public void TitlesAreSet()
            {
                Check.That(QuestionList.Title).IsEqualTo(StubTitle);
                Check.That(QuestionList.QuestionTitle).IsEqualTo(StubQuestionTitle);
                Check.That(QuestionList.AnswerTitle).IsEqualTo(StubAnswerTitle);
            }

            [Fact] public void ListIsEmpty()
            {
                Check.That(QuestionList.Questions.Count).IsEqualTo(0);
                
            }
        }

        public class GivenOneQuestionIsAdded : QuestionListTest
        {
            public GivenOneQuestionIsAdded()
            {
                QuestionList = CreateQuestionListWithOneQuestion();
            }

            [Fact] public void ListContainsOneQuestion()
            {
                Check.That(QuestionList.Questions.Count).IsEqualTo(1);
            }

            [Fact] public void FirstQuestionIsAvailable()
            {
                Check.That(QuestionList.Questions[0]).IsNotNull();
                Check.That(QuestionList.Questions[0].Question).IsEqualTo(StubQuestion);
                Check.That(QuestionList.Questions[0].Answer).IsEqualTo(StubAnswer);
            }
        }

        public class GivenListWithTenQuestions : QuestionListTest
        {
            public GivenListWithTenQuestions()
            {
                QuestionList = CreateQuestionList();
            }
        }

        private const string StubTitle = "Sample List";
        private const string StubQuestionTitle = "Dutch";
        private const string StubAnswerTitle = "English";
        private const string StubQuestion = "kat";
        private const string StubAnswer = "cat";

        private static QuestionList CreateQuestionList() => new QuestionList(StubTitle, StubQuestionTitle, StubAnswerTitle, new QuestionList.ListItem[0]);
        private static QuestionList CreateQuestionListWithOneQuestion()
        {
            var questionList = CreateQuestionList();
            questionList.Questions.Add(QuestionList.ListItem.Create(StubQuestion, StubAnswer));
            return questionList;
        }
    }
    */
}
