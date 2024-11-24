using Auth.EF; 
using System.Web; 
using System.Web.Mvc; 

namespace Auth.Auth 
{
    // Custom authorization attribute class that inherits from AuthorizeAttribute
    public class Logged : AuthorizeAttribute
    {
        // Override the AuthorizeCore method to check session for the user
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Attempt to retrieve the user object from the session
            // It assumes that the user object has been stored in the session with the key "user"
            var user = (UserDB)httpContext.Session["user"];

            // If the user object exists in the session, return true (authenticated)
            // The user is considered logged in if the object is present in the session
            if (user != null)
            {
                return true; // The user is authenticated and authorized
            }

            // If the user is not found in the session, return false (unauthenticated)
            return false;
        }
    }
}
