using System;
using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages.Rehearsal
{
    public class RepeatRehearsalCommand : BaseCommand
    {
        public Guid RehearsalId { get; set; }
    }
}