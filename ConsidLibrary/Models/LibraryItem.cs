using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Services.Description;

namespace ConsidLibrary.Models
{
    [MetadataType(typeof(LibraryItem))]
    public partial class LibraryItems
    {
    }   
    public class LibraryItem
    {
        public int Id { get; set; }
        
        public Category Category { get; set; }
        
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        public string Author { get; set; }
        public int? Pages  { get; set; }
        public int? RunTimeMinutes { get; set; }
        public bool IsBorrowable { get; set; }
        public string Borrower { get; set; }
        public DateTime? BorrowDate { get; set; }
        
        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; }

        [NotMapped]
        public List<LibraryTypes> Types { get; set; }
    }
}