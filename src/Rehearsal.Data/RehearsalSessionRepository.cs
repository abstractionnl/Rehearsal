using System;
using CQRSlite.Commands;
using Rehearsal.Messages;
using Rehearsal.WebApi;

namespace Rehearsal.Data
{
    public class RehearsalSessionRepository : IRehearsalSessionRepository
    {
        public RehearsalSessionRepository(ICommandSender commandSender)
        {
            CommandSender = commandSender ?? throw new ArgumentNullException(nameof(commandSender));
        }

        private ICommandSender CommandSender { get; }
        
        public IRehearsalFactory New()
        {
            return new RehearsalFactory(CommandSender);
        }
    }
}