using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rehearsal.Messages;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rehearsal.WebApi
{
    [Route("api/[controller]")]
    public class QuestionListController : Controller
    {
        private IMapper Mapper { get; }

        private static readonly List<QuestionList> Lists;

        static QuestionListController()
        {
            Lists = new List<QuestionList>()
            {
                new QuestionList("Hoofdstuk 1", "Nederlands", "Duits",
                    new[] { QuestionList.ListItem.Create("het huis", "das Haus"), QuestionList.ListItem.Create("de kat", "die Katze")  }),
                new QuestionList("Hoofdstuk 2", "Nederlands", "Frans",
                    new[] { QuestionList.ListItem.Create("het huis", "le maison"), QuestionList.ListItem.Create("de kat", "le chat") }),
            };
        }

        public QuestionListController(IMapper mapper)
        {
            Mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Lists.Select(Mapper.Map<QuestionListOverviewModel>));
        }

        [HttpGet, Route("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var list = Lists.SingleOrDefault(x => x.Id == id);

            if (list == null)
            {
                return NotFound();
            }

            var model = Mapper.Map<QuestionListModel>(list);
            return Ok(model);
        }

        [HttpPut, Route("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] Messages.QuestionListModel model)
        {
            var list = Lists.SingleOrDefault(x => x.Id == id);

            if (list == null)
            {
                return NotFound();
            }

            UpdateFromModel(list, model);

            return Ok();
        }

        private void UpdateFromModel(QuestionList questionList, Messages.QuestionListModel model)
        {
            questionList.Update(model.Title, model.AnswerTitle, model.QuestionTitle,
                model.Questions.Select(questionModel => QuestionList.ListItem.Create(questionModel.Question, questionModel.Answer)));
        }

        private QuestionList CreateFromModel(Messages.QuestionListModel model)
        {
            var questionList = new QuestionList(model.Title, model.AnswerTitle, model.QuestionTitle,
                model.Questions.Select(questionModel => QuestionList.ListItem.Create(questionModel.Question, questionModel.Answer)));

            return questionList;
        }
    }
}
