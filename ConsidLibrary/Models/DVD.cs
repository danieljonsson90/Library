using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsidLibrary.Models
{
    public class DVD
    {

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "RunTimeMinutes is required.")]
        public int? RunTimeMinutes { get; set; }
    }
}