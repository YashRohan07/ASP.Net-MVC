using CRUD_Operations.EF; // Importing the Entity Framework model
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Operations.Controllers
{
    public class StudentController : Controller
    {
        // Create an instance of the database context class CRUDdbEntities to interact with the database (StudentDB table)
        CRUDdbEntities dbObj = new CRUDdbEntities();


        // GET: Student (Index) - Retreives the list of all students from the database
        public ActionResult Index()
        {
            var std = dbObj.StudentDBs.ToList(); // Retrieve all the records from the StudentDB table.
            return View(std); // Pass the student list to the 'Index' view.

            // dbObj.StudentDBs is a DbSet<StudentDB>, a collection of all student records in StudentDB table.
            // The ToList() method convert it to a list
        }



        // GET: Details - Retrieves the details of a specific student by ID
        public ActionResult Details(int id)
        {
            var std = dbObj.StudentDBs.FirstOrDefault(x => x.ID == id); //Get student by id

            // Check if the student was found in the database.
            if (std == null)
            {
                return RedirectToAction("Index"); // Redirect to Index page if the student is not found
            }

            return View(std); // If the student is found, pass the student details to the 'Details' view.


            // 'FirstOrDefault' fetches the first student where the condition is met, or 'null' if no student is found.
            // (x => x.ID == id) is a lambda expression used in LINQ to filter data
            // 'x' represents each student in the StudentDBs collection
            // '=>' is the lambda operator, which means "for each student x, check if..."
            // 'x.ID == id' compares the 'ID' of the current student (x) with the 'id' passed to the method
            // If a student with the matching ID is found, it returns that student; otherwise, it returns null
            
        }



        // GET: Create - Displays the form to create a new student
        public ActionResult Create()
        {
            return View();  // Returns the Create view where the user can input student details
        }


        // POST: Create - Handles form submission to create a new student
        [HttpPost]
        public ActionResult Create(StudentDB data)
        {
            if (ModelState.IsValid)  // Checks if the data submitted from the form is valid based on model validation rules
            {
                dbObj.StudentDBs.Add(data);  // Adds the new student data in the database
                dbObj.SaveChanges();  // Saves the new student record to the database

                return RedirectToAction("Index");  // Redirects to the Index page to display the list of all students after successful creation
            }
            return View(data);  // Returns to the Create view with validation errors, if this is not valid 
        }



        // GET: Edit - Fetches the student details for editing
        public ActionResult Edit(int id)
        {
            var std = dbObj.StudentDBs.FirstOrDefault(x => x.ID == id);  // Get student by ID
            if (std == null)
            {
                return RedirectToAction("Index");  // Redirect to Index page if the student is not found
            }
            return View(std);  // If the student is found, pass the student details to the 'Edit' view.

        }


        // POST: Edit - Handles the form submission to update student details
        [HttpPost]
        public ActionResult Edit(StudentDB data)  // Receives the updated student details (data) from the form
        {
            if(ModelState.IsValid)  // Check if the data received from the form is valid based on validation rules
            {
                var std = dbObj.StudentDBs.FirstOrDefault(x => x.ID == data.ID); // Search student by ID 

                if (std == null)
                {
                    return RedirectToAction("Index");  // Redirect to Index page if the student is not found
      
                }

                // Update the student record with the new values from the form
                std.Name = data.Name;
                std.Email = data.Email;
                std.Mobile = data.Mobile;

                dbObj.SaveChanges();  // Save the changes to the database
                return RedirectToAction("Index");  // Redirect to the Index page after the update
                
            }
            return View(data);  // Return to the Edit view with validation errors if this is not valid


            // (x => x.ID == data.ID) 
            // 'x' represents the current student object 
            // '=>' is the lambda operator, meaning "for each student x, check if..."

            //  x.ID: Refers to the ID property of each student in the StudentDB table.
            //  data.ID: Refers to the ID property of the student object that was passed into the method

            // 'x.ID == data.ID' compares the 'ID' of the current student (x) with the 'ID' from the passed 'data' object
        }



        // GET: Delete - Retrieves student data for deletion confirmation
        public ActionResult Delete(int id)
        {
            var student = dbObj.StudentDBs.FirstOrDefault(x => x.ID == id);  // Find the student by iD
            if (student == null)
            {
                return RedirectToAction("Index");  // Redirect to Index page if the student is not found
            }
            return View(student);  // Pass the student details to the Delete view for confirmation
        }



        // POST: DeleteConfirmed - Handles the deletion of a student
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var std = dbObj.StudentDBs.FirstOrDefault(x => x.ID == id);  // Find the student by ID
            if (std == null)
            {
                return RedirectToAction("Index");  // Redirect to index view if the student is not found
            }

            dbObj.StudentDBs.Remove(std);  // Remove the student from the database
            dbObj.SaveChanges();  // Save the changes to the database

            return RedirectToAction("Index");  // Redirect to the Index page after deletion
        }


    }
}