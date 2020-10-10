using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ConsidLibrary.Models
    {
        [MetadataType(typeof(Employee))]
    public partial class Employees
    {
    }
    public class Employee
        {
            public int Id { get; set; }
            
            [Required(ErrorMessage = "FirstName is required.")]
            public string FirstName { get; set; }
        
            [Required(ErrorMessage = "LastName is required.")]
            public string LastName { get; set; }
        
            [Required(ErrorMessage = "Salary is required.")] 
            public decimal Salary { get; set; }
            public bool IsCEO { get; set; }
            public bool IsManager { get; set; }
            public int? ManagerId { get; set; }
        }
}