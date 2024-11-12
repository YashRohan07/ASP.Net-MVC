using Basic.Models;
using System;
using System.Web.Mvc;

namespace Basic.Controllers
{
    public class PortfolioController : Controller
    {
        // GET: Portfolio/Information
        public ActionResult Information()
        {
            // Using ViewBag for passing simple data
            ViewBag.Name = "Yash Rohan";
            ViewBag.Age = 24;
            ViewBag.Email = "yashrohan22@gmail.com";
            ViewBag.Phone = "01738360521";

            return View();
        }

        // GET: Portfolio/Education
        public ActionResult Education()
        {
            // Using ViewData to pass data to the view
            Degree d1 = new Degree() { Title = "SSC", Institute = "School", Year = "2018", Result = "2.50" };
            Degree d2 = new Degree() { Title = "HSC", Institute = "College", Year = "2020", Result = "3.50" };
            Degree d3 = new Degree() { Title = "BSC", Institute = "University", Year = "2024", Result = "1.50" };

            ViewData["Degrees"] = new Degree[] { d1, d2, d3 };

            // Using TempData to show a one-time success message
            TempData["Message"] = "Education data loaded successfully!";

            return View();
        }

        // GET: Portfolio/Contact
        public ActionResult Contact()
        {
            return View();
        }

        // POST: Portfolio/Contact
        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            // After processing the contact form then it will pass a success message using TempData
            TempData["Message"] = "Your message has been successfully sent!";
            return RedirectToAction("Contact");
        }

        // GET: Portfolio/About
        public ActionResult About()
        {
            // Using ViewBag for application details
            ViewBag.AppName = "My Portfolio App";
            ViewBag.Description = "This app showcases my personal and educational details.";

            return View();
        }
    }
}
