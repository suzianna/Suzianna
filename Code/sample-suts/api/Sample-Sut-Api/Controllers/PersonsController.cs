using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample_Sut_Api.Model;

namespace Sample_Sut_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private static List<Person> _people = new List<Person>();

        [HttpGet]
        public List<Person> Get()
        {
            return _people;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var candidate = _people.FirstOrDefault(a => a.Id == id);
            if (candidate == null) return NotFound();

            return Ok(candidate);
        }

        [HttpPost]
        public IActionResult Post(Person person)
        {
            person.Id = Guid.NewGuid();
            _people.Add(person);
            return CreatedAtAction(nameof(Get), new {id = person.Id});
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var candidate = _people.FirstOrDefault(a => a.Id == id);
            if (candidate != null)
                _people.Remove(candidate);

            return NoContent();
        }

        [HttpPost("{id}")]
        public IActionResult Put(Guid id, Person person)
        {
            var candidate = _people.FirstOrDefault(a => a.Id == id);
            if (candidate == null)
                return NotFound();

            candidate.Firstname = person.Firstname;
            candidate.Lastname = person.Lastname;

            return Ok();
        }
    }
}