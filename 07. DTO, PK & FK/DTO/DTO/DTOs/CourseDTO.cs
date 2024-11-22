using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DTO.DTOs
{
    public class CourseDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Course Name is required")]
        public string Credit { get; set; }

        public int D_ID { get; set; }

    }
}