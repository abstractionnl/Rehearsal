using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CQRSlite.Commands;
using Rehearsal.Messages;
using Rehearsal.Messages.QuestionList;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Rehearsal
{
    public class RehearsalFactory : IRehearsalFactory
    {
        public RehearsalFactory(ICommandSender commandSender)
        {
            CommandSender = commandSender ?? throw new ArgumentNullException(nameof(commandSender));
            Questions = new List<QuestionModel>();
        }

        private ICommandSender CommandSender { get; }
        
        private IList<QuestionModel> Questions { get; }
        
        public async Task<Guid> Create()
        {
            var id = Guid.NewGuid();

            var cmd = new StartRehearsalCommand()
            {
                Id = id,
                Questions = Questions
            };

            await CommandSender.Send(cmd);
            
            return id;
        }

        public IRehearsalFactory AddQuestionList(QuestionListModel questionList)
        {
            foreach (var question in questionList.Questions)
            {
                Questions.Add(question);
            }

            return this;
        }
    }
}