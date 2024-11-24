using Auth.DTO;
using Auth.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auth.Controllers
{
    // LoginController handles user authentication
    public class LoginController : Controller
    {
        UserEntities db = new UserEntities();


        // GET: Login - Returns the login view
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        // POST: Login - Handles user login, sets session, and redirects based on user role
        [HttpPost]
        public ActionResult Login(LoginDTO log)
        {
            if (ModelState.IsValid)
            {
                // Look for the user in the database
                var user = db.UserDBs.SingleOrDefault(u => u.UserName == log.UserName && u.Password == log.Password);

                if (user != null)
                {
                    // Boxing: Store the user object in session (implicitly boxing)
                    Session["user"] = user;  //storing the user object in the session under the key "user"

                    // Redirect to the appropriate page based on the user's role
                    if (user.UserType == "Admin")
                    {
                        return RedirectToAction("AdminIndex", "User"); // Admin dashboard
                    }
                    else
                    {
                        return RedirectToAction("UserIndex", "User"); // Regular user dashboard
                    }
                }

                // If user is not found, display error message
                TempData["Msg"] = "Invalid username or password";
            }

            // If validation fails, return to the login view with error messages
            return View();
        }
    }

}