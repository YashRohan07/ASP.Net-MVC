using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DTO.DTOs
{
    public class StudentDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CGPA is required")]
        public decimal Cgpa { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        public int D_ID { get; set; }
    }
}