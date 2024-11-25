using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TRP.DTOs
{
    public class ProgramDTO
    {
        public int P_ID { get; set; } // Primary Key

        [Required(ErrorMessage = "Program Name is required.")]
        [StringLength(150, ErrorMessage = "Program Name cannot exceed 150 characters.")]
        public string P_Name { get; set; }

        [Required(ErrorMessage = "TRP Score is required.")]
        [Range(0.0, 10.0, ErrorMessage = "TRP Score must be between 0.0 and 10.0.")]
        public decimal TRPScore { get; set; }

        public int C_ID { get; set; } // Foreign Key without validation

        [Required(ErrorMessage = "Air Time is required.")]
        [DataType(DataType.Time, ErrorMessage = "Air Time must be in HH:mm:ss format.")]
        public TimeSpan AirTime { get; set; }
    }
}