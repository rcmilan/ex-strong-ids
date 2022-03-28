using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SID.Domain.Entities;
using SID.Domain.Interfaces;

namespace SID.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {

        IRepository<School, SchoolId> _repository;

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
        public IActionResult Post(School school)
        {
            var result = _repository.Add(school);

            return Ok(result);
        }
    }
}
