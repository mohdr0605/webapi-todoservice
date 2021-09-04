using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using To.Do.Service.Models;

namespace To.Do.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly Random _random = new Random();

        private static List<Student> _students = new List<Student>
            {
               new Student(){Id=1, FirstName = "John", LastName="Doen", Phone="778427598", Email="john@ymail.com"},
               new Student(){Id=2, FirstName = "Tony", LastName="M", Phone="988427598", Email="tony@ymail.com" }
            };

        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetAllStudent")]
        public List<Student> Get()
        {
            return _students;
        }

        [HttpGet("GetStudentById/{id}")]
        public IActionResult Get(int id)
        {
            if(id> 0)
            {
                return Ok(_students.FirstOrDefault(x => x.Id == id));
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("InsertStudent")]
        public IActionResult Post([FromBody]Student student)
        {
            if (!string.IsNullOrEmpty(student?.FirstName) && !string.IsNullOrEmpty(student?.LastName) &&
                !string.IsNullOrEmpty(student?.Email) && !string.IsNullOrEmpty(student?.Phone))
            {
                student.Id = _random.Next(1, 3);
                _students.Add(student);
                return Ok(true);
            }
            else
            {
                return Ok("Request data is not valid");
            }
        }


        [HttpPut("UpdateStudent")]
        public IActionResult Put([FromBody]Student student)
        {
            if (student?.Id > 0)
            {
                var result = _students.FirstOrDefault(x => x.Id == student.Id);
                result.FirstName = student.FirstName;
                result.LastName = student.LastName;
                result.Email = student.Email;
                result.Phone = student.Phone;

                return Ok(true);
            }
            else
            {
                return Ok("Request data is not valid"); 
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id>0)
            {
                var result = _students.FirstOrDefault(x => x.Id == id);
                _students.Remove(result);
                return Ok(true);
            }
            else
            {
                return Ok("Request data is not valid");
            }
           
        }
    }
}
