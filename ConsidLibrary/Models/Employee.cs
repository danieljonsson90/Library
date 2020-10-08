﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsidLibrary.Models
    {
        public class Employee
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public decimal Salary { get; set; }
            public bool IsCEO { get; set; }
            public bool IsManager { get; set; }
            public int? ManagerId { get; set; }
        }
}