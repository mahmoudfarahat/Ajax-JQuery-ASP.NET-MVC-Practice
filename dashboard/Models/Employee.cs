using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dashboard.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public  string Phone { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime BirthDate { get; set; }
    }
}