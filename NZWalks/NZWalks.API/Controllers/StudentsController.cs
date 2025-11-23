using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    // https://localhost:7192/api/Students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GET: https://localhost:7192/api/Students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            // for demonstration purposes only, assume this is a database
            string[] studentNames = new string[] { "John", "Jane", "Mark", "Emily" };

            return Ok(studentNames);    // successful 200 response
        }
    }
}
