using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Rehearsal.Web
{
    [Route("api/[controller]")]
    public class HeroesController : Controller
    {
        private static List<Hero> _heroes = new List<Hero>()
        {
            new Hero() {Id = 0, Name = "Zero"},
            new Hero() {Id = 11, Name = "Mr. Nice"},
            new Hero() {Id = 12, Name = "Narco"},
            new Hero() {Id = 13, Name = "Bombasto"},
            new Hero() {Id = 14, Name = "Celeritas"},
            new Hero() {Id = 15, Name = "Magneta"},
            new Hero() {Id = 16, Name = "RubberMan"},
            new Hero() {Id = 17, Name = "Dynama"},
            new Hero() {Id = 18, Name = "Dr IQ"},
            new Hero() {Id = 19, Name = "Magma"},
            new Hero() {Id = 20, Name = "Tornado"}
        };

        [HttpGet]
        public IEnumerable<Hero> Get([FromQuery] string term)
        {
            return _heroes.Where(x => x.Name.Contains(term));
        }

        [HttpGet, Route("{id}")]
        public IActionResult Get(int id)
        {
            var hero = _heroes.FirstOrDefault(x => x.Id == id);

            if (hero == null)
               return NotFound();

            return Ok(hero);
        }

        public IActionResult Add([FromBody] Hero model)
        {
            var hero = new Hero()
            {
                Id = _heroes.Select(x => x.Id).Max() + 1,
                Name = model.Name
            };

            _heroes.Add(hero);

            return Ok(hero);
        }

        [HttpPut, Route("{id}")]
        public IActionResult Save([FromRoute] int id, [FromBody] Hero model)
        {
            var hero = _heroes.FirstOrDefault(x => x.Id == id);

            if (hero == null)
              return NotFound();

            hero.Name = model.Name;

            return Ok(hero);
        }

        [HttpDelete, Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var hero = _heroes.FirstOrDefault(x => x.Id == id);

            if (hero == null)
                return BadRequest();

            _heroes.Remove(hero);

            return Ok();
        }
    }

    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}