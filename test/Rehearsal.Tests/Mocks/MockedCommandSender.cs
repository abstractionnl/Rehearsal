using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Commands;

namespace Rehearsal.Tests.Mocks
{
    public class MockedCommandSender : ICommandSender
    {
        public MockedCommandSender()
        {
            SentCommands = new List<ICommand>();
        }

        public IList<ICommand> SentCommands { get; }
        
        public Task Send<T>(T command, CancellationToken cancellationToken = new CancellationToken()) where T : class, ICommand
        {
            SentCommands.Add(command);
            return Task.CompletedTask;
        }
    }
}