﻿using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages.QuestionList
{
    public class CreateQuestionListCommand : BaseCommand
    {
        public QuestionListProperties QuestionList { get; set; }
    }
}