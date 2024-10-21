using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPeopleServices _peopleServices;
        public PeopleController([FromKeyedServices("people2Service")]IPeopleServices peopleServices)
        {
            _peopleServices = peopleServices;
        }

        [HttpGet("all")]
        public List<People> GetPeople() => Repository.people;

        [HttpGet("{id}")]
        public ActionResult<People> Get(int id)
        {
            var people = Repository.people.FirstOrDefault(p => p.Id == id);

            if (people == null)
            {
                return NotFound();
            }
            return Ok(people);
        }

        [HttpGet("search/{search}")]
        public List<People> Get(string search) =>
            Repository.people.Where(p => p.Name.ToUpper().Contains(search.ToUpper())).ToList();

        [HttpPost]
        public IActionResult Add(People people)
        {
            if (!_peopleServices.Validate(people))
            {
                return BadRequest();
            }
            Repository.people.Add(people);
            return NoContent();
        }
    }

    public class Repository
    {
        public static List<People> people = new List<People>
        {
            new People()
            {
                Id = 1,
                Name = "Foo",
                Birthday = new DateTime(2006,2,12)
            },
            new People()
            {
                Id = 2,
                Name = "Fio",
                Birthday = new DateTime(2003,2,12)
            },
            new People()
            {
                Id = 3,
                Name = "Marc",
                Birthday = new DateTime(2004,2,12)
            },
            new People()
            {
                Id = 4,
                Name = "Misa",
                Birthday = new DateTime(2005,2,12)
            }
        };
    }
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }

    }
}

