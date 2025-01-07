using IntroAPI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IntroAPI.Controllers
{
    public class StudentController : ApiController
    {
        Context db = new Context();

        [HttpGet]
        [Route("api/student/getall")]
        public IHttpActionResult GetAll()
        {
            var data = db.Students.ToList();
            if (data != null && data.Count > 0)
            {
                return Ok(new { Success = true, Message = "Students retrieved successfully.", Data = data });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/student/{id}")]
        public IHttpActionResult Get(int id)
        {
            var data = db.Students.Find(id);
            if (data != null)
            {
                return Ok(new { Success = true, Message = "Student retrieved successfully.", Data = data });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/student/create")]
        public IHttpActionResult Create(Student s)
        {
            if (s != null)
            {
                db.Students.Add(s);
                db.SaveChanges();
                return Ok(new { Success = true, Message = "Student created successfully.", Data = s });
            }
            else
            {
                return BadRequest("Invalid student data.");
            }
        }

        [HttpPut]
        [Route("api/student/update/{id}")]
        public IHttpActionResult Update(int id, Student updatedStudent)
        {
            var existingStudent = db.Students.Find(id);
            if (existingStudent != null)
            {
                existingStudent.Name = updatedStudent.Name;
                existingStudent.cgpa = updatedStudent.cgpa;
                db.SaveChanges();
                return Ok(new { Success = true, Message = "Student updated successfully.", Data = existingStudent });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("api/student/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var student = db.Students.Find(id);
            if (student != null)
            {
                db.Students.Remove(student);
                db.SaveChanges();
                return Ok(new { Success = true, Message = "Student deleted successfully." });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
