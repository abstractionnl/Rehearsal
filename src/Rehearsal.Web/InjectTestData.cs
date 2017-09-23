using System;
using System.Linq;
using System.Threading.Tasks;
using CQRSlite.Commands;
using LanguageExt;
using Rehearsal.Messages;
using Rehearsal.WebApi;

namespace Rehearsal.Web
{
    public class InjectTestData
    {
        public InjectTestData(ICommandSender commandSender, IQuestionListRepository repository)
        {
            CommandSender = commandSender;
            Repository = repository;
        }

        private ICommandSender CommandSender { get; }
        public IQuestionListRepository Repository { get; }

        public async Task Run()
        {
            if (Repository.GetAll().Any())
                return;

            await CommandSender.Send(new CreateQuestionListCommand()
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
            });

            await CommandSender.Send(new CreateQuestionListCommand()
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
            });
        }
    }
}
