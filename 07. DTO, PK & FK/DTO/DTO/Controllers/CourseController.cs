using DTO.DTOs;
using DTO.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTO.Controllers
{
    public class CourseController : Controller
    {
        // Create an instance to interact with the database
        private DTOdbEntities db = new DTOdbEntities();

        // Convert EF model to DTO
        private static CourseDTO ConvertToDTO(CourseDB c)
        {
            return new CourseDTO()
            {
                ID = c.ID,
                Name = c.Name,
                Credit = c.Credit,
                D_ID = c.D_ID
            };
        }


        // Convert DTO to EF model
        private static CourseDB ConvertToEF(CourseDTO c)
        {
            return new CourseDB()
            {
                ID = c.ID,
                Name = c.Name,
                Credit = c.Credit,
                D_ID = c.D_ID
            };
        }



        // List all courses
        public ActionResult Index()
        {
            var courses = db.CourseDBs.ToList(); // Retrieve all courses from the database
            var courseDTOs = courses.Select(ConvertToDTO).ToList(); // Convert EF models to DTOs
            return View(courseDTOs); // Pass DTOs to the view
        }



        // Details of a specific course
        public ActionResult Details(int id)
        {
            var course = db.CourseDBs.Find(id); // Find the course by ID
            if (course == null)
            {
                return HttpNotFound(); // Return a 404 if not found
            }
            return View(ConvertToDTO(course)); // Convert to DTO and return to the view
        }



        // Display form for creating a new course (GET request)
        public ActionResult Create()
        {
            return View();
        }



        // Handle form submission for creating a new course (POST request)
        [HttpPost]
        public ActionResult Create(CourseDTO data)
        {
            if (ModelState.IsValid) // Check if the model is valid
            {
                var course = ConvertToEF(data); // Convert DTO to EF model
                db.CourseDBs.Add(course); // Add the course entity
                db.SaveChanges(); // Save changes to the database
                return RedirectToAction("Index"); // Redirect to the list of courses
            }
            return View(data); // Return to the create form if invalid
        }



        // Display form for editing an existing course (GET request)
        public ActionResult Edit(int id)
        {
            var course = db.CourseDBs.Find(id); // Find the course by ID
            if (course == null)
            {
                return RedirectToAction("Index"); // Redirect if course not found
            }
            var courseDTO = ConvertToDTO(course); // Convert to DTO for the edit form
            return View(courseDTO); // Pass the course DTO to the view
        }



        // Handle form submission for editing an existing course (POST request)
        [HttpPost]
        public ActionResult Edit(CourseDTO data)
        {
            if (ModelState.IsValid) // Check if the model is valid
            {
                var course = db.CourseDBs.Find(data.ID); // Find the course by ID
                if (course != null)
                {
                    // Update course properties
                    course.Name = data.Name;
                    course.Credit = data.Credit;
                    course.D_ID = data.D_ID;

                    db.SaveChanges(); // Save changes to the database
                    return RedirectToAction("Index"); // Redirect to the list of courses
                }
            }
            return View(data); // Return to the edit form if invalid
        }



        // Display confirmation form for deleting a course (GET request)
        public ActionResult Delete(int id)
        {
            var course = db.CourseDBs.Find(id); // Find the course by ID
            if (course == null)
            {
                return RedirectToAction("Index"); // Redirect if course not found
            }
            var courseDTO = ConvertToDTO(course); // Convert to DTO for the delete confirmation
            return View(courseDTO); // Pass the course DTO to the view
        }



        // Handle form submission for confirming course deletion (POST request)
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var course = db.CourseDBs.Find(id); // Find the course by ID
            if (course != null)
            {
                db.CourseDBs.Remove(course); // Remove the course
                db.SaveChanges(); // Save changes to the database
            }
            return RedirectToAction("Index"); // Redirect to the Index
        }
    }
}
