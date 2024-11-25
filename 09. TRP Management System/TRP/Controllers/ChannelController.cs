using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRP.DTOs;
using TRP.EF;

namespace TRP.Controllers
{
    public class ChannelController : Controller
    {
        TRPdbEntities db = new TRPdbEntities();


        // Convert EF model to DTO
        private static ChannelDTO ConvertToDTO(dbChannel c)
        {
            return new ChannelDTO()
            {
                C_ID = c.ChannelId,  
                C_Name = c.ChannelName,  
                E_Year = c.EstablishedYear,  
                Country = c.Country,
            };
        }


        // Convert DTO to EF model
        private static dbChannel ConvertToEF(ChannelDTO c)
        {
            return new dbChannel()
            {
                ChannelId = c.C_ID,  
                ChannelName = c.C_Name, 
                EstablishedYear = c.E_Year,  
                Country = c.Country,
            };
        }



        // Details of a specific channel
        public ActionResult Details(int C_ID)
        {
            var channel = db.dbChannels.Find(C_ID);  
            if (channel == null)
            {
                return HttpNotFound();  // Return a 404 if not found
            }
            return View(ConvertToDTO(channel));  // Convert to DTO and return to the view
        }



        // Display form for creating a new channel (GET request)
        public ActionResult Create()
        {
            return View();
        }



        // Handle form submission for creating a new channel (POST request)
        [HttpPost]
        public ActionResult Create(ChannelDTO TRP)
        {
            if (ModelState.IsValid)  
            {
                var channel = ConvertToEF(TRP);  // Convert DTO to EF model
                db.dbChannels.Add(channel);  
                db.SaveChanges();  // Save changes to the database

                TempData["SuccessMessage"] = "Channel added successfully";
                return RedirectToAction("Index");  // Redirect to the list of channels
            }
            return View(TRP);  // Return to the create form if invalid
        }



        // Display form for editing an existing channel (GET request)
        public ActionResult Edit(int C_ID)
        {
            var channel = db.dbChannels.Find(C_ID);  
            if (channel == null)
            {
                return RedirectToAction("Index");  // Redirect if channel not found
            }
            var channelDTO = ConvertToDTO(channel);  // Convert to DTO for the edit form
            return View(channelDTO);  // Pass the channel DTO to the view
        }



        // Handle form submission for editing an existing channel (POST request)
        [HttpPost]
        public ActionResult Edit(ChannelDTO TRP)
        {
            if (ModelState.IsValid)  
            {
                var channel = db.dbChannels.Find(TRP.C_ID);  // Find the channel by ID
                if (channel != null)
                {
                    // Update channel properties
                    channel.ChannelName = TRP.C_Name;  
                    channel.EstablishedYear = TRP.E_Year;  
                    channel.Country = TRP.Country;

                    db.SaveChanges();  // Save changes to the database
                    TempData["SuccessMessage"] = "Channel Updated successfully";
                    return RedirectToAction("Index");  // Redirect to the list of channels
                }
            }
            return View(TRP);  // Return to the edit form if invalid
        }



        // Display confirmation form for deleting a channel (GET request)
        public ActionResult Delete(int C_ID)
        {
            var channel = db.dbChannels.Find(C_ID);  
            if (channel == null)
            {
                return RedirectToAction("Index");  // Redirect if channel not found
            }
            var channelDTO = ConvertToDTO(channel);  // Convert to DTO for the delete confirmation
            return View(channelDTO);  // Pass the channel DTO to the view
        }



        // Handle form submission for confirming channel deletion (POST request)
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int C_ID)
        {
            var channel = db.dbChannels.Find(C_ID);  // Find the channel by ID
            if (channel != null)
            {
                // Check if there are any associated programs with the channel
                var associatedPrograms = db.dbPrograms.Where(p => p.ChannelId == C_ID).ToList();

                if (associatedPrograms.Any())
                {
                    // If associated programs exist, prevent deletion and show an error message
                    TempData["ErrorMessage"] = "Cannot delete channel because it has associated programs.";
                    return RedirectToAction("Index");  // Redirect to the channel index
                }

                // If no associated programs, proceed with deletion
                db.dbChannels.Remove(channel);  // Remove the channel
                db.SaveChanges();  // Save changes to the database
                TempData["SuccessMessage"] = "Channel deleted successfully";  
            }
            return RedirectToAction("Index");  // Redirect to the Index
        }



        // List all channels with search functionality
        public ActionResult Index(string search = "")
        {
            var channels = db.dbChannels.AsQueryable();  

            // Apply search filter for ID or Name to checks if the search string is not empty or null.
            if (!string.IsNullOrEmpty(search))
            {
                channels = channels.Where(c => c.ChannelId.ToString() == search || c.ChannelName == search);  // Corrected field names based on EF model
            }

            // Convert the filtered channels to DTOs and execute the query to retrieve the results as a list.
            var channelDTOs = channels.Select(ConvertToDTO).ToList();
            return View(channelDTOs);
        }
    }
}
