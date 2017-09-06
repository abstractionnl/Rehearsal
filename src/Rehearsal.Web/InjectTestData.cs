using System;
using CQRSlite.Commands;
using LanguageExt;
using Rehearsal.Messages;

namespace Rehearsal.Web
{
    public class InjectTestData
    {
        public InjectTestData(ICommandSender commandSender)
        {
            CommandSender = commandSender;
        }

        private ICommandSender CommandSender { get; }

        public void Run()
        {
            CommandSender.Send(new CreateQuestionListCommand()
            {
                Id = Guid.NewGuid(),
                QuestionList = new QuestionListProperties
                {
                    Title = "Hoodstuk 1",
                    QuestionTitle = "Nederlands",
                    AnswerTitle = "Duits",
                    Questions = new[] {
                        new QuestionListProperties.Item
                        {
                            Question = "het huis",
                            Answer = "das Haus"
                        },
                        new QuestionListProperties.Item
                        {
                            Question = "de kat",
                            Answer = "die Katze"
                        },
                    }
                }
            }).Wait();
            
            CommandSender.Send(new CreateQuestionListCommand()
            {
                Id = Guid.NewGuid(),
                QuestionList = new QuestionListProperties
                {
                    Title = "Hoodstuk 2",
                    QuestionTitle = "Nederlands",
                    AnswerTitle = "Frans",
                    Questions = new[] {
                        new QuestionListProperties.Item
                        {
                            Question = "het huis",
                            Answer = "le maison"
                        },
                        new QuestionListProperties.Item
                        {
                            Question = "de kat",
                            Answer = "le chat"
                        },
                    }
                }
            }).Wait();
        }
    }
}
