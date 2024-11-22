using DTO.DTOs;
using DTO.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTO.Controllers
{
    public class StudentController : Controller
    {
        // Create an instance to interact with the database
        private DTOdbEntities db = new DTOdbEntities();


        // Convert EF model to DTO
        private static StudentDTO ConvertToDTO(StudentDB s)
        {
            return new StudentDTO()
            {
                ID = s.ID,
                Name = s.Name,
                Cgpa = s.Cgpa,
                Gender = s.Gender,
                D_ID = s.D_ID,
            };
        }


        // Convert DTO to EF model
        private static StudentDB ConvertToEF(StudentDTO s)
        {
            return new StudentDB()
            {
                ID = s.ID,
                Name = s.Name,
                Cgpa = s.Cgpa,
                Gender = s.Gender,
                D_ID = s.D_ID,
            };
        }


        /*
        // List all students
        public ActionResult Index()
        {
            var students = db.StudentDBs.ToList(); // Retrieve all students from the database
            var studentDTOs = students.Select(ConvertToDTO).ToList(); // Convert EF models to DTOs
            return View(studentDTOs); // Pass DTOs to the view
        }
        */


        // Details of a specific student
        public ActionResult Details(int id)
        {
            var student = db.StudentDBs.Find(id); // Find the student by ID
            if (student == null)
            {
                return HttpNotFound(); // Return a 404 if not found
            }
            return View(ConvertToDTO(student)); // Convert to DTO and return to the view
        }



        // Display form for creating a new student (GET request)
        public ActionResult Create()
        {
            return View();
        }



        // Handle form submission for creating a new student (POST request)
        [HttpPost]
        public ActionResult Create(StudentDTO data)
        {
            if (ModelState.IsValid) // Check if the model is valid
            {
                var student = ConvertToEF(data); // Convert DTO to EF model
                db.StudentDBs.Add(student); // Add the student entity
                db.SaveChanges(); // Save changes to the database
                return RedirectToAction("Index"); // Redirect to the list of students
            }
            return View(data); // Return to the create form if invalid
        }



        // Display form for editing an existing student (GET request)
        public ActionResult Edit(int id)
        {
            var student = db.StudentDBs.Find(id); // Find the student by ID
            if (student == null)
            {
                return RedirectToAction("Index"); // Redirect if student not found
            }
            var studentDTO = ConvertToDTO(student); // Convert to DTO for the edit form
            return View(studentDTO); // Pass the student DTO to the view
        }



        // Handle form submission for editing an existing student (POST request)
        [HttpPost]
        public ActionResult Edit(StudentDTO data)
        {
            if (ModelState.IsValid) // Check if the model is valid
            {
                var student = db.StudentDBs.Find(data.ID); // Find the student by ID
                if (student != null)
                {
                    // Update student properties
                    student.Name = data.Name;
                    student.Cgpa = data.Cgpa;
                    student.Gender = data.Gender;

                    db.SaveChanges(); // Save changes to the database
                    return RedirectToAction("Index"); // Redirect to the list of students
                }
            }
            return View(data); // Return to the edit form if invalid
        }



        // Display confirmation form for deleting a student (GET request)
        public ActionResult Delete(int id)
        {
            var student = db.StudentDBs.Find(id); // Find the student by ID
            if (student == null)
            {
                return RedirectToAction("Index"); // Redirect if student not found
            }
            var studentDTO = ConvertToDTO(student); // Convert to DTO for the delete confirmation
            return View(studentDTO); // Pass the student DTO to the view
        }



        // Handle form submission for confirming student deletion (POST request)
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var student = db.StudentDBs.Find(id); // Find the student by ID
            if (student != null)
            {
                db.StudentDBs.Remove(student); // Remove the student
                db.SaveChanges(); // Save changes to the database
            }
            return RedirectToAction("Index"); // Redirect to the Index
        }
        


        public ActionResult Index(string search = "", bool ScholarshipHolder = false)
        {
            // Get all students from the database. AsQueryable() method allows LINQ queries
            var students = db.StudentDBs.AsQueryable(); 

            // Apply search filter for ID or Name to checks if the search string is not empty or null.
            if (!string.IsNullOrEmpty(search))
            {
                students = students.Where(s => s.ID.ToString() == search || s.Name == search);

            }

            // Filter for scholarship holders
            if (ScholarshipHolder)
            {
                students = students.Where(s => s.Cgpa >= 3.75m); // m suffix denotes that 3.75 is a decimal value.
            }

            // Converts the filtered students to DTOs and executes the query to retrieve the results as a list.
            var studentDTOs = students.Select(ConvertToDTO).ToList();
            return View(studentDTOs);
        }

        /*
         string search = "": The search parameter is used to filter students by their ID or Name. 
         It has a default value of an empty string (""), meaning if the search parameter is not provided, the search will be empty by default.

         bool ScholarshipHolder = false: The ScholarshipHolder parameter is a boolean flag used to filter students who have a CGPA >= 3.75. 
         The default value is false, meaning if the filter is not applied, the CGPA filter is ignored.

         s.ID.ToString() == search: This checks if the ID of the student matches the search string exactly. 
         Since ID is typically an integer, it is converted to a string (ToString()) to compare it with the search string.

         s.Name == search: This checks if the Name of the student matches the search string exactly.

         */



    }
}
