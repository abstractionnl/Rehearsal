using System.Threading.Tasks;
using AutoMapper;
using CQRSlite.Commands;
using CQRSlite.Domain;
using Rehearsal.Messages;

namespace Rehearsal
{
    public class CreateQuestionListCommandHandler : ICommandHandler<CreateQuestionListCommand>
    {
        private readonly ISession _session;

        public CreateQuestionListCommandHandler(ISession session)
        {
            _session = session;
        }

        public async Task Handle(CreateQuestionListCommand message)
        {
            var questionList = new QuestionList(message.Id, message.QuestionList);
            await _session.Add(questionList);
            await _session.Commit();
        }
    }

    public class UpdateQuestionListCommandHandler : ICommandHandler<UpdateQuestionListCommand>
    {
        private readonly ISession _session;

        public UpdateQuestionListCommandHandler(ISession session)
        {
            _session = session;
        }
        
        public async Task Handle(UpdateQuestionListCommand message)
        {
            var questionList = await _session.Get<QuestionList>(message.Id);
            questionList.Update(message.QuestionList);
            await _session.Commit();
        }
    }
}