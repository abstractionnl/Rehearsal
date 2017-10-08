using System;
using CQRSlite.Commands;
using Rehearsal.Messages;
using Rehearsal.Messages.Rehearsal;
using Rehearsal.Rehearsal;
using Rehearsal.WebApi;
using Rehearsal.WebApi.Rehearsal;

namespace Rehearsal.Data.Rehearsal
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