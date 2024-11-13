using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Validation.Models;

namespace Validation.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            // Pass the success message from TempData to the View
            ViewBag.Message = TempData["Message"];
            return View(new Student());
        }

        [HttpPost]
        public ActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                // After processing the create form, set a success message in TempData
                TempData["Message"] = "Your data has been successfully sent!";
                return RedirectToAction("Create"); // Redirect to show the success message on the Create page

                //return RedirectToAction("Index", "Home"); // Redirect to the "Index" action of the "Home" controller after successfull
            }

            return View(s); // If the model is invalid, return the same view with validation errors
        }
    }

}