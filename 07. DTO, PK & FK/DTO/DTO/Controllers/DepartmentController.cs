using DTO.DTOs;
using DTO.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTO.Controllers
{
    public class DepartmentController : Controller
    {
        // Create an instance to interact with the database
        private DTOdbEntities db = new DTOdbEntities();

        // Convert EF model to DTO
        private static DepartmentDTO ConvertToDTO(DepartmentDB d)
        {
            return new DepartmentDTO()
            {
                ID = d.ID,
                Name = d.Name,
                Location = d.Location
            };
        }


        // Convert DTO to EF model
        private static DepartmentDB ConvertToEF(DepartmentDTO d)
        {
            return new DepartmentDB()
            {
                ID = d.ID,
                Name = d.Name,
                Location = d.Location
            };
        }



        // List all departments
        public ActionResult Index()
        {
            var departments = db.DepartmentDBs.ToList(); // Retrieve all departments from the database
            var departmentDTOs = departments.Select(ConvertToDTO).ToList(); // Convert EF models to DTOs
            return View(departmentDTOs); // Pass DTOs to the view
        }



        // Details of a specific department
        public ActionResult Details(int id)
        {
            var department = db.DepartmentDBs.Find(id); // Find the department by ID
            if (department == null)
            {
                return HttpNotFound(); // Return a 404 if not found
            }
            return View(ConvertToDTO(department)); // Convert to DTO and return to the view
        }



        // Display form for creating a new department (GET request)
        public ActionResult Create()
        {
            return View();
        }



        // Handle form submission for creating a new department (POST request)
        [HttpPost]
        public ActionResult Create(DepartmentDTO data)
        {
            if (ModelState.IsValid) // Check if the model is valid
            {
                var department = ConvertToEF(data); // Convert DTO to EF model
                db.DepartmentDBs.Add(department); // Add the department entity
                db.SaveChanges(); // Save changes to the database
                return RedirectToAction("Index"); // Redirect to the list of departments
            }
            return View(data); // Return to the create form if invalid
        }



        // Display form for editing an existing department (GET request)
        public ActionResult Edit(int id)
        {
            var department = db.DepartmentDBs.Find(id); // Find the department by ID
            if (department == null)
            {
                return RedirectToAction("Index"); // Redirect if department not found
            }
            var departmentDTO = ConvertToDTO(department); // Convert to DTO for the edit form
            return View(departmentDTO); // Pass the department DTO to the view
        }



        // Handle form submission for editing an existing department (POST request)
        [HttpPost]
        public ActionResult Edit(DepartmentDTO data)
        {
            if (ModelState.IsValid) // Check if the model is valid
            {
                var department = db.DepartmentDBs.Find(data.ID); // Find the department by ID
                if (department != null)
                {
                    // Update department properties
                    department.Name = data.Name;
                    department.Location = data.Location;

                    db.SaveChanges(); // Save changes to the database
                    return RedirectToAction("Index"); // Redirect to the list of departments
                }
            }
            return View(data); // Return to the edit form if invalid
        }



        // Display confirmation form for deleting a department (GET request)
        public ActionResult Delete(int id)
        {
            var department = db.DepartmentDBs.Find(id); // Find the department by ID
            if (department == null)
            {
                return RedirectToAction("Index"); // Redirect if department not found
            }
            var departmentDTO = ConvertToDTO(department); // Convert to DTO for the delete confirmation
            return View(departmentDTO); // Pass the department DTO to the view
        }



        // Handle form submission for confirming department deletion (POST request)
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var department = db.DepartmentDBs.Find(id); // Find the department by ID
            if (department != null)
            {
                db.DepartmentDBs.Remove(department); // Remove the department
                db.SaveChanges(); // Save changes to the database
            }
            return RedirectToAction("Index"); // Redirect to the Index
        }
    }
}
