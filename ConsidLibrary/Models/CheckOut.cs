using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsidLibrary.Models
{
    public class CheckOut
    {
        [Required(ErrorMessage = "Borrower is required.")]
        public string Borrower { get; set; }
        
        [Required(ErrorMessage = "Date is required.")]
        public DateTime? BorrowDate { get; set; }

    }
}