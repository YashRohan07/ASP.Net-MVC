//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DTO.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudentDB
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Cgpa { get; set; }
        public string Gender { get; set; }
        public int D_ID { get; set; }
    
        public virtual DepartmentDB DepartmentDB { get; set; }
    }
}
