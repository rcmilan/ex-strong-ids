using Microsoft.AspNetCore.Mvc;
using SID.Domain.Entities;
using SID.Domain.Interfaces;

namespace SID.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private IRepository<School, SchoolId> _repository;

        public SchoolsController(IRepository<School, SchoolId> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repository.GetAll();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(AddSchoolEvent addSchool)
        {
            School school = new School(addSchool.Name, addSchool.Address);

            var result = _repository.Add(school);

            return Ok(result);
        }

        public record struct AddSchoolEvent(string Name, string Address);
    }
}