using System;
using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Commands;
using CQRSlite.Events;
using Microsoft.Extensions.Logging;
using StructureMap;

namespace Rehearsal.Data.Infrastructure
{
    public class InProcessBus : ICommandSender, IEventPublisher
    {
        public InProcessBus(IContainer container, ILogger<InProcessBus> logger)
        {
            Container = container;
            Logger = logger;
        }

        private IContainer Container { get; }
        private ILogger<InProcessBus> Logger { get; }
        
        public async Task Send<T>(T command, CancellationToken cancellationToken = default(CancellationToken)) where T : class, ICommand
        {
            using (Logger.BeginScope("Executing command {commandype}", typeof(T).FullName))
            using (var nested = Container.GetNestedContainer())
            {
                var handler = nested.GetInstance<ICommandHandler<T>>();
                
                try
                {
                    Logger.LogDebug(LoggingEvents.Send, "Executing handler {handlertype}", handler.GetType().FullName);
                    
                    await handler.Handle(command);
                
                    Logger.LogDebug(LoggingEvents.Send, "Finished executing handler {handlertype}", handler.GetType().FullName);
                }
                catch (Exception e)
                {
                    Logger.LogError(LoggingEvents.SendError, "Failed executing handler {handlertype}", e, handler.GetType().FullName);
            
                    throw;
                }
            }
        }

        public async Task Publish<T>(T @event, CancellationToken cancellationToken = default(CancellationToken)) where T : class, IEvent
        {
            using (Logger.BeginScope("Publishing event {eventtype}", typeof(T).FullName))
            using (var nested = Container.GetNestedContainer())
            {
                var handlers = nested.GetAllInstances<IEventHandler<T>>();
                foreach (var handler in handlers)
                {
                    try
                    {
                        Logger.LogDebug(LoggingEvents.Send, "Executing handler {handlertype}", handler.GetType().FullName);
                        
                        await handler.Handle(@event);
                        
                        Logger.LogDebug(LoggingEvents.Publish, "Finished executing handler {handlertype}", handler.GetType().FullName);
                    }
                    catch (Exception e)
                    {
                        Logger.LogError(LoggingEvents.PublishError, "Failed executing handler {handlertype}", e, handler.GetType().FullName);
                    }
                }
            }
        }
        
        public static class LoggingEvents
        {
            public const int Send = 1000;
            public const int Publish = 1001;

            public const int SendError = 4000;
            public const int PublishError = 4001;
        }
    }
}