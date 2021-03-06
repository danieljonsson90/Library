﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsidLibrary.Models
{
    public class Book
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        public string Author { get; set; }
        
        [Required(ErrorMessage = "Pages is required.")]
        public int? Pages { get; set; }
    }
}