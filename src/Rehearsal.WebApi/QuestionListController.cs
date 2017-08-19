using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rehearsal.WebApi
{
    [Route("api/[controller]")]
    public class QuestionListController : Controller
    {
        private static readonly List<QuestionList> Lists;

        static QuestionListController()
        {
            Lists = new List<QuestionList>()
            {
                new QuestionList("Hoofdstuk 1", "Nederlands", "Duits", new[] { QuestionList.ListItem.Create("Huis", "Haus"), QuestionList.ListItem.Create("Kat", "Katze")  }),
                new QuestionList("Hoofdstuk 2", "Nederlands", "Frans", new[] { QuestionList.ListItem.Create("Huis", "Maison"), QuestionList.ListItem.Create("Kat", "Chat") }),
            };
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Lists);
        }

        [HttpGet, Route("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var list = Lists.SingleOrDefault(x => x.Id == id);

            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        [HttpPut, Route("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] Messages.QuestionList model)
        {
            var list = Lists.SingleOrDefault(x => x.Id == id);

            if (list == null)
            {
                return NotFound();
            }

            UpdateFromModel(list, model);

            return Ok();
        }

        private void UpdateFromModel(QuestionList questionList, Messages.QuestionList model)
        {
            questionList.Update(model.Title, model.AnswerTitle, model.QuestionTitle,
                model.Questions.Select(questionModel => QuestionList.ListItem.Create(questionModel.Question, questionModel.Answer)));
        }

        private QuestionList CreateFromModel(Messages.QuestionList model)
        {
            var questionList = new QuestionList(model.Title, model.AnswerTitle, model.QuestionTitle,
                model.Questions.Select(questionModel => QuestionList.ListItem.Create(questionModel.Question, questionModel.Answer)));

            return questionList;
        }
    }
}
