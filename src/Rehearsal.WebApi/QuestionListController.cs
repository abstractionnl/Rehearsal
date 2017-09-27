using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CQRSlite.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rehearsal.Messages;

namespace Rehearsal.WebApi
{
    [Route("api/[controller]")]
    [Authorize]
    public class QuestionListController : Controller
    {
        public IQuestionListRepository QuestionListRepository { get; }
        private IMapper Mapper { get; }
        private ICommandSender CommandSender { get; }

        public QuestionListController(IQuestionListRepository questionListRepository, IMapper mapper, ICommandSender commandSender)
        {
            if (questionListRepository == null) throw new ArgumentNullException(nameof(questionListRepository));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            if (commandSender == null) throw new ArgumentNullException(nameof(commandSender));
            
            QuestionListRepository = questionListRepository;
            Mapper = mapper;
            CommandSender = commandSender;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(QuestionListRepository.GetAll());
        }

        [HttpGet, Route("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var list = QuestionListRepository.GetById(id);

            return list
                .Map<IActionResult>(Ok)
                .IfNone(NotFound);
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> Create([FromBody] QuestionListProperties questionList)
        {
            var command = new CreateQuestionListCommand()
            {
                Id = Guid.NewGuid(),
                QuestionList = questionList
            };

            await CommandSender.Send(command);

            return Ok(command.Id);
        }
        
        [HttpPut, Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] QuestionListProperties questionList)
        {
            var command = new UpdateQuestionListCommand()
            {
                Id = id,
                QuestionList = questionList
            };

            await CommandSender.Send(command);

            return Ok();
        }

        [HttpDelete, Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id)
        {
            var command = new DeleteQuestionListCommand()
            {
                Id = id
            };

            await CommandSender.Send(command);

            return Ok();
        }
    }
}
