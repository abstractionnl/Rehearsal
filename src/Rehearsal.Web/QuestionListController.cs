using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rehearsal.Web
{
    [Route("api/[controller]")]
    public class QuestionListController : Controller
    {
        private static readonly List<QuestionList> Lists;

        static QuestionListController()
        {
            Lists = new List<QuestionList>()
            {
                new QuestionList("Hoofdstuk 1", "Nederlands", "Duits"),
                new QuestionList("Hoofdstuk 2", "Nederlands", "Frans"),
            };

            Lists[0].Questions.Add(QuestionList.ListItem.Create("Huis", "Haus"));
            Lists[0].Questions.Add(QuestionList.ListItem.Create("Kat", "Katze"));
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
    }
}
