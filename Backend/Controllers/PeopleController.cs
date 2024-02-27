using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPeopleService _peopleService;

        public PeopleController([FromKeyedServices("people2Services")]IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }
        [HttpGet("all")]
        public List<People> GetPeople()
        {
            return Repository.People;
        }
        [HttpGet("{id}")]
        public ActionResult<People> Get(int id) {
           var person = Repository.People.FirstOrDefault(x => x.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }



        [HttpGet("search/{search}")]
        public List<People> Get(string search)
        {
            return Repository.People.Where(p => p.Name.ToUpper().Contains(search.ToUpper())).ToList();

        }

        [HttpPost]
        public IActionResult Add(People person)
        {
            if(!_peopleService.Validate(person))
            {
                return BadRequest();

            }
            Repository.People.Add(person);
            return NoContent();
        }
    }

    public class Repository
    {
        public  static List<People> People = new List<People> { 
            new People() { Id =1, Name="Edgar", BirthDate= new DateTime(1982, 09, 27)},
            new People() { Id = 2, Name = "Allan", BirthDate = new DateTime(2015, 01, 03) },
        new People() { Id =3, Name="Alejandro", BirthDate= new DateTime(2009, 10, 04)}};
    }
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }

}
