using System;
using System.Threading.Tasks;
using AutoMapper;
using CQRSlite.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rehearsal.Messages.Rehearsal;
using Rehearsal.WebApi.QuestionList;
using LanguageExt;

namespace Rehearsal.WebApi.Rehearsal
{
    [Route("api/[controller]")]
    [Authorize]
    public class RehearsalController : Controller
    {
        private IQuestionListRepository QuestionListRepository { get; }
        private IRehearsalSessionRepository RehearsalSessionRepository { get; }
        private ICommandSender CommandSender { get; }

        public RehearsalController(
            IRehearsalSessionRepository rehearsalSessionRepository, 
            IQuestionListRepository questionListRepository, 
            ICommandSender commandSender)
        {
            CommandSender = commandSender ?? throw new ArgumentNullException(nameof(commandSender));
            RehearsalSessionRepository = rehearsalSessionRepository ?? throw new ArgumentNullException(nameof(rehearsalSessionRepository));
            QuestionListRepository = questionListRepository ?? throw new ArgumentNullException(nameof(questionListRepository));
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> Start([FromBody] StartRehearsalRequest request)
        {
            var questionList = QuestionListRepository.GetById(request.QuestionListId)
                .ValueOrThrow();
            
            var cmd = await RehearsalSessionRepository
                .New()
                .AddQuestionList(questionList)
                .Create();

            await CommandSender.Send(cmd);
            
            return Ok(cmd.Id);
        }

        [HttpGet, Route("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var session = RehearsalSessionRepository.GetById(id);
            
            return session
                .Map<IActionResult>(Ok)
                .IfNone(NotFound);
        }

        [HttpPut, Route("{id:guid}")]
        public Task<IActionResult> GiveAnswer([FromRoute] Guid id, [FromBody] GiveAnswerRequest request)
        {
            return RehearsalSessionRepository.GetSession(id)
                .ToAsync()
                .Map(session => session.GiveAnswer(request.QuestionId, request.Answer))
                .Map(async answerResult =>
                {
                    await CommandSender.Send(new AnswerRehearsalQuestionCommand()
                    {
                        Id = id,
                        QuestionId = request.QuestionId,
                        Result = answerResult
                    });

                    return answerResult;
                })
                .Match<IActionResult>(x => Ok(x), () => BadRequest());
        }
    }
}