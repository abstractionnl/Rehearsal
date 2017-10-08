using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rehearsal.Messages.Rehearsal;
using Rehearsal.WebApi.QuestionList;

namespace Rehearsal.WebApi.Rehearsal
{
    [Route("api/[controller]")]
    [Authorize]
    public class RehearsalController : Controller
    {
        private IQuestionListRepository QuestionListRepository { get; }
        private IRehearsalSessionRepository RehearsalSessionRepository { get; }

        public RehearsalController(IRehearsalSessionRepository rehearsalSessionRepository, IQuestionListRepository questionListRepository)
        {
            RehearsalSessionRepository = rehearsalSessionRepository ?? throw new ArgumentNullException(nameof(rehearsalSessionRepository));
            QuestionListRepository = questionListRepository ?? throw new ArgumentNullException(nameof(questionListRepository));
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> Start([FromBody] StartRehearsalRequest request)
        {
            var questionList = QuestionListRepository.GetById(request.QuestionListId)
                .ValueOrThrow();
            
            var rehearsalId = await RehearsalSessionRepository
                .New()
                .AddQuestionList(questionList)
                .Create();

            return Ok(rehearsalId);
        }
    }
}