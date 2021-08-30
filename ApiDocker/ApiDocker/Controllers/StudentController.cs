using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiDocker.Business;
using ApiDocker.Controllers;
using ApiDocker.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDocker.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private IStudentBusiness _studentBusiness;


        public StudentController(ILogger<StudentController> logger, IStudentBusiness studentBusiness)
        {
            _logger = logger;
            _studentBusiness = studentBusiness;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> Get()
        {
            return _studentBusiness.FindAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetById(long id)
        {
            return _studentBusiness.FindById(id);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> Post([FromBody] Student student)
        {
            return _studentBusiness.Create(student);

        }

        [HttpPut]
        public async Task<ActionResult<Student>> Put([FromBody] Student student)
        {
            return _studentBusiness.Update(student);
        }

        [HttpDelete("{id}")]
        public ActionResult<int> Delete(long id)
        {
            return _studentBusiness.Delete(id);
        }
    }
    [ApiVersion("2")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class Student2Controller : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private IStudentBusiness _studentBusiness;

        public Student2Controller(ILogger<StudentController> logger, IStudentBusiness studentBusiness)
        {
            _logger = logger;
            _studentBusiness = studentBusiness;
        }
        [HttpGet]
        [Authorize(Roles = "diretor,professor")]
        public async Task<ActionResult<List<Student>>> Get()
        {
            return _studentBusiness.FindAll();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "diretor,professor")]
        public async Task<ActionResult<Student>> GetById(long id)
        {
            return _studentBusiness.FindById(id);
        }

        [HttpPost]
        [Authorize(Roles = "diretor")]
        public async Task<ActionResult<Student>> Post([FromBody] Student student)
        {
            return _studentBusiness.Create(student);

        }

        [HttpPut]
        [Authorize(Roles = "diretor")]
        public async Task<ActionResult<Student>> Put([FromBody] Student student)
        {
            return _studentBusiness.Update(student);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "diretor")]
        public ActionResult<int> Delete(long id)
        {
            return _studentBusiness.Delete(id);
        }
    }
}
