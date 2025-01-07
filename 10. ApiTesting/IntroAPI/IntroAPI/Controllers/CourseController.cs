using IntroAPI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IntroAPI.Controllers
{
    public class CourseController : ApiController
    {
        Context db = new Context();

        [HttpGet]
        [Route("api/course/getall")]
        public IHttpActionResult GetAll()
        {
            var data = db.Courses.ToList();
            if (data != null && data.Count > 0)
            {
                return Ok(new { Success = true, Message = "Courses retrieved successfully.", Data = data });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/course/{id}")]
        public IHttpActionResult Get(int id)
        {
            var data = db.Courses.Find(id);
            if (data != null)
            {
                return Ok(new { Success = true, Message = "Course retrieved successfully.", Data = data });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/course/create")]
        public IHttpActionResult Create(Course c)
        {
            if (c != null)
            {
                db.Courses.Add(c);
                db.SaveChanges();
                return Ok(new { Success = true, Message = "Course created successfully.", Data = c });
            }
            else
            {
                return BadRequest("Invalid course data.");
            }
        }

        [HttpPut]
        [Route("api/course/update/{id}")]
        public IHttpActionResult Update(int id, Course updatedCourse)
        {
            var existingCourse = db.Courses.Find(id);
            if (existingCourse != null)
            {
                existingCourse.Name = updatedCourse.Name;
                existingCourse.Credit = updatedCourse.Credit;
                db.SaveChanges();
                return Ok(new { Success = true, Message = "Course updated successfully.", Data = existingCourse });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("api/course/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var course = db.Courses.Find(id);
            if (course != null)
            {
                db.Courses.Remove(course);
                db.SaveChanges();
                return Ok(new { Success = true, Message = "Course deleted successfully." });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
