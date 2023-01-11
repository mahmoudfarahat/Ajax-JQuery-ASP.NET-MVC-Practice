using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dashboard.Models
{
    public class Employee
    {

        public int Id { get; set; }
        [Required(ErrorMessage ="This Field is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string Email { get; set; }

        public  string Phone { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime BirthDate { get; set; }
    }
}