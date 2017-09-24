using System;
using System.Linq;
using System.Threading.Tasks;
using CQRSlite.Commands;
using CQRSlite.Events;
using Microsoft.Extensions.Logging;
using Rehearsal.Data;
using Rehearsal.Messages;
using Rehearsal.WebApi;

namespace Rehearsal.Web
{
    public class StartupService
    {
        public StartupService(ICommandSender commandSender, IQuestionListRepository questionListRepository, IEventPublisher eventPublisher, IEventRepository eventRepository, ILogger<StartupService> logger)
        {
            CommandSender = commandSender;
            QuestionListRepository = questionListRepository;
            EventPublisher = eventPublisher;
            EventRepository = eventRepository;
            Logger = logger;
        }

        private IEventPublisher EventPublisher { get; }
        private ICommandSender CommandSender { get; }
        private IQuestionListRepository QuestionListRepository { get; }
        private IEventRepository EventRepository { get; }
        private ILogger<StartupService> Logger { get; }

        public async Task Run()
        {
            using (Logger.BeginScope("Bootstrapping the application"))
            {
                Logger.LogDebug("Replaying previous events to fill in memory repositories");

                await EventPublisher.ReplayEvents(
                    EventRepository.GetEventStream()
                );

                Logger.LogDebug("Finished replaying previous events to fill in memory repositories");

                if (QuestionListRepository.GetAll().Any())
                    return;

                Logger.LogDebug("Injecting test data into application for development");

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

                Logger.LogDebug("Finished injecting test data into application for development");
            }
        }
    }
}
