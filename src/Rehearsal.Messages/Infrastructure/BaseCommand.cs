using System;
using CQRSlite.Commands;

namespace Rehearsal.Messages.Infrastructure
{
    public class BaseCommand : ICommand
    {
        public Guid Id { get; set; }

        public int ExpectedVersion { get; set; }
    }
}