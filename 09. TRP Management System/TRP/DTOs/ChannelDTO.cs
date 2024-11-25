using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TRP.DTOs
{
    public class ChannelDTO
    {
        public int C_ID { get; set; } // Primary Key

        [Required(ErrorMessage = "Channel Name is required.")]
        [StringLength(100, ErrorMessage = "Channel Name cannot exceed 100 characters.")]
        public string C_Name { get; set; }

        [Required(ErrorMessage = "Established Year is required.")]
        [Range(1900, int.MaxValue, ErrorMessage = "Established Year must be between 1900 and the current year.")]
        public int E_Year { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(50, ErrorMessage = "Country cannot exceed 50 characters.")]
        public string Country { get; set; }

        
    }
}