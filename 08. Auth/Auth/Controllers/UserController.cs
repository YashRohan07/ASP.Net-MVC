using Auth.Auth; // Importing the Auth namespace for custom authorization attributes
using Auth.DTO;  // Importing the Data Transfer Object namespace for user DTOs
using Auth.EF;   // Importing the Entity Framework namespace for database models
using System.Linq; // Importing LINQ methods for querying collections
using System.Web.Mvc; // Importing MVC classes for creating the controller and actions

namespace Auth.Controllers
{
    [Logged] // Ensures that only logged-in users can access the actions in this controller
    public class UserController : Controller
    {
        UserEntities db = new UserEntities(); // Creating a new instance of the database context


        // Convert the EF to DTO
        private static UserDTO ConvertToDTO(UserDB u)
        {
            return new UserDTO()
            {
                UserID = u.UserID, 
                UserName = u.UserName, 
                Email = u.Email, 
                Address = u.Address, 
                Password = u.Password, 
                UserType = u.UserType 
            };
        }


        // Convert the DTO to EF
        private static UserDB ConvertToEF(UserDTO u)
        {
            return new UserDB()
            {
                UserID = u.UserID,
                UserName = u.UserName, 
                Email = u.Email, 
                Address = u.Address, 
                Password = u.Password, 
                UserType = u.UserType 
            };
        }



        // GET: Displays the logged-in Admin's dashboard
        [HttpGet]
        public ActionResult AdminIndex()
        {
            var currentUser = Session["user"] as UserDB; // Retrieve the current user from the session

            if (currentUser == null) 
            {
                return RedirectToAction("Login", "Login"); // If no user is found in the session, redirect to login
            }

            // Only allow Admin to access this page
            if (currentUser.UserType == "Admin") 
            {
                var allUsers = db.UserDBs.ToList(); // Retrieve all users from the database
                var allUserDTOs = allUsers.Select(ConvertToDTO).ToList(); // Convert each user to DTO
                return View(allUserDTOs); // Return the Admin Index view with the list of users
            }
            else 
            {
                return RedirectToAction("UserIndex"); // If the user is not an Admin, redirect to User Index
            }
        }



        // GET:  Displays the logged-in user's dashboard
        public ActionResult UserIndex()
        {
            var currentUser = Session["user"] as UserDB; // Retrieve the current user from the session

            if (currentUser == null) 
            {
                return RedirectToAction("Login", "Login"); // If no user is found in the session, redirect to login
            }

            var user = db.UserDBs.SingleOrDefault(u => u.UserID == currentUser.UserID); // Find the user in the database by their UserID
        
            // Convert the user data to DTO and send to the User Index view
            var userDTO = ConvertToDTO(user);
            return View(userDTO);
        }



        // Displays the form for creating a new user 
        [AllowAnonymous]  // This allows unauthenticated users to access this 
        public ActionResult Create()
        {
            return View(); // Return the Create view 
        }



        // Handles the form submission for creating a new user 
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(UserDTO data)
        {
            if (ModelState.IsValid) 
            {
                if (data == null)
                {
                    return View(data); // Return to the form with the current data if any errors exist
                }

                var user = ConvertToEF(data); // Convert the DTO to EF model
                db.UserDBs.Add(user); // Add the new user to the database
                db.SaveChanges(); // Save the changes to the database

                TempData["Msg"] = "User created successfully!"; 
                return RedirectToAction("Login", "Login"); // Redirect to the login page after user creation
            }

            return View(data); // Return the form with validation errors if any
        }



        // Displays the form for editing a user 
        public ActionResult Edit(int id)
        {
            var user = db.UserDBs.Find(id); // Retrieve the user by ID from the database
            if (user == null) 
            {
                return HttpNotFound(); // If the user is not found, return a 404 error
            }

            // Convert the user data to DTO and send to the Edit view
            var userDTO = ConvertToDTO(user);
            return View(userDTO); 
        }



        // Handles the form submission for editing the user 
        [HttpPost]
        public ActionResult Edit(UserDTO data)
        {
            if (ModelState.IsValid) 
            {
                var user = db.UserDBs.Find(data.UserID); // Find the user by their UserID
                if (user != null) 
                {
                    user.UserName = data.UserName;
                    user.Email = data.Email;
                    user.Address = data.Address;
                    user.Password = data.Password;
                    user.UserType = data.UserType;

                    db.SaveChanges(); // Save the updated data to the database
                    TempData["Msg"] = "User updated successfully!"; 

                    return RedirectToAction("AdminIndex"); // Redirect to the Admin Index page
                }
            }

            return View(data); // Return the form with validation errors if any
        }



        // Displays the confirmation for deleting a user 
        public ActionResult Delete(int id)
        {
            var user = db.UserDBs.Find(id); // Retrieve the user by ID from the database
            if (user == null) 
            {
                return HttpNotFound(); // If the user is not found, return a 404 error
            }

            // Convert the user data to DTO and send to the Delete view
            var userDTO = ConvertToDTO(user);
            return View(userDTO);       
        }



        // Confirms and deletes a user 
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = db.UserDBs.Find(id); // Retrieve the user by ID from the database
            if (user != null) 
            {
                db.UserDBs.Remove(user); 
                db.SaveChanges();
                TempData["Msg"] = "User deleted successfully!"; 
            }
            return RedirectToAction("AdminIndex"); // Redirect to the Admin Index page
        }



        // Displays the details of a user
        public ActionResult Details(int id)
        {
            var user = db.UserDBs.Find(id); // Retrieve the user by ID from the database
            if (user == null) 
            {
                return HttpNotFound(); // If the user is not found, return a 404 error
            }

            // Convert the user data to DTO and send to the Details view
            var userDTO = ConvertToDTO(user);
            return View(userDTO);
        }


        // Logout - Clears the session and redirects to the login page
        public ActionResult Logout()
        {
            Session["user"] = null; // Clear the session to log the user out
            TempData["Msg"] = "You have logged out successfully."; // Set a success message
            return RedirectToAction("Login", "Login"); // Redirect to the login page
        }
    }
}
