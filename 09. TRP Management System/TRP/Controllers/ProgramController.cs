using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRP.DTOs;
using TRP.EF;

namespace TRP.Controllers
{
    public class ProgramController : Controller
    {
        TRPdbEntities db = new TRPdbEntities();


        // Convert EF model to DTO
        private static ProgramDTO ConvertToDTO(dbProgram p)
        {
            return new ProgramDTO()
            {
                P_ID = p.ProgramId,
                P_Name = p.ProgramName,
                TRPScore = p.TRPScore,
                C_ID = p.ChannelId,
                AirTime = p.AirTime,
            };
        }


        // Convert DTO to EF model
        private static dbProgram ConvertToEF(ProgramDTO p)
        {
            return new dbProgram()
            {
                ProgramId = p.P_ID,
                ProgramName = p.P_Name,
                TRPScore = p.TRPScore,
                ChannelId = p.C_ID,
                AirTime = p.AirTime,
            };
        }


        // Details of a specific program
        public ActionResult Details(int P_ID)
        {
            var program = db.dbPrograms.Find(P_ID); // Find the program by ID
            if (program == null)
            {
                return HttpNotFound(); // Return a 404 if not found
            }
            return View(ConvertToDTO(program)); // Convert to DTO and return to the view
        }



        // Display form for creating a new program (GET request)
        public ActionResult Create()
        {
            return View();
        }



        // Handle form submission for creating a new program (POST request)
        [HttpPost]
        public ActionResult Create(ProgramDTO TRP)
        {
            if (ModelState.IsValid) 
            {
                var program = ConvertToEF(TRP); // Convert DTO to EF model
                db.dbPrograms.Add(program); 
                db.SaveChanges(); // Save changes to the database

                TempData["SuccessMessage"] = "Program added successfully";
                return RedirectToAction("Index"); // Redirect to the list of programs
            }
            return View(TRP); // Return to the create form if invalid
        }



        // Display form for editing an existing program (GET request)
        public ActionResult Edit(int P_ID)
        {
            var program = db.dbPrograms.Find(P_ID); // Find the program by ID
            if (program == null)
            {
                return RedirectToAction("Index"); // Redirect if program not found
            }
            var programDTO = ConvertToDTO(program); // Convert to DTO for the edit form
            return View(programDTO); // Pass the program DTO to the view
        }



        // Handle form submission for editing an existing program (POST request)
        [HttpPost]
        public ActionResult Edit(ProgramDTO TRP)
        {
            if (ModelState.IsValid) 
            {
                var program = db.dbPrograms.Find(TRP.P_ID); // Find the program by ID
                if (program != null)
                {
                    // Update program properties
                    program.ProgramName = TRP.P_Name;
                    program.TRPScore = TRP.TRPScore;
                    program.AirTime = TRP.AirTime;
                    program.ChannelId = TRP.C_ID; 

                    db.SaveChanges(); // Save changes to the database
                    TempData["SuccessMessage"] = "Program updated successfully";
                    return RedirectToAction("Index"); // Redirect to the list of programs
                }
            }
            return View(TRP); // Return to the edit form if invalid
        }



        // Display confirmation form for deleting a program (GET request)
        public ActionResult Delete(int P_ID)
        {
            var program = db.dbPrograms.Find(P_ID); // Find the program by ID
            if (program == null)
            {
                return RedirectToAction("Index"); // Redirect if program not found
            }
            var programDTO = ConvertToDTO(program); // Convert to DTO for the delete confirmation
            return View(programDTO); // Pass the program DTO to the view
        }



        // Handle form submission for confirming program deletion (POST request)
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int P_ID)
        {
            var program = db.dbPrograms.Find(P_ID); // Find the program by ID
            if (program != null)
            {
                db.dbPrograms.Remove(program); // Remove the program
                db.SaveChanges(); // Save changes to the database
                TempData["SuccessMessage"] = "Program deleted successfully";
            }
            return RedirectToAction("Index"); // Redirect to the Index
        }


        // Search and Filter functionality with Dashboard
        public ActionResult Index(string search = "", int? selectedChannelId = null)
        {
            // Get programs from the database
            var programs = db.dbPrograms.AsQueryable();

            // Apply search filter for ProgramId or ProgramName
            if (!string.IsNullOrEmpty(search))
            {
                programs = programs.Where(p => p.ProgramId.ToString() == search ||
                                               p.ProgramName == search);
            }

            // Filter by selected ChannelId if provided
            if (selectedChannelId.HasValue)
            {
                programs = programs.Where(p => p.ChannelId == selectedChannelId.Value);
            }

            // Get program DTOs and return to the view
            var programDTOs = programs.Select(ConvertToDTO).ToList();

            // Pass channels for the dropdown
            ViewBag.Channels = db.dbChannels.ToList();

            return View(programDTOs);
        }


    }
}
