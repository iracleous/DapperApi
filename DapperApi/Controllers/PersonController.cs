using DapperApi.Model;
using DapperApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonCrud _service;
        public PersonController(IPersonCrud service) =>_service = service;

        [HttpGet]
        [Route("{personId}")]
        public Person? GetPerson([FromRoute] long personId) => _service.GetPerson(personId);
       
        [HttpGet]
        public IEnumerable<Person>? GetPerson() => _service.GetPerson();

        [HttpGet]
        [Route("details")]
        public IEnumerable<Person>? GetPersonDetails() => _service.GetPersonDetails();

        [HttpPost]
        public Person CreatePerson(Person person) => _service.CreatePerson(person);

        [HttpPost]
        [Route("delete/{personId}")]
        public bool DeletePerson([FromRoute] long personId) => _service.DeletePerson(personId);

        [HttpPost]
        [Route("update/{personId}")]
        public bool UpdatePerson([FromRoute] long personId, Person person) => _service.UpdatePerson(personId, person);

        [HttpPost]
        [Route("table")]
        public bool CreatePersonTable() => _service.CreateTable();
    }
}
