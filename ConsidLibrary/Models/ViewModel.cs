using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsidLibrary.Models
{
    public class ViewModel
    {
        public LibraryItem libraryItem { get; set; }
        public Category category { get; set; }
        public Employee employee { get; set; }
        public string[] titleAcronyms { get; set; }
        public CheckOut checkOut { get; set; }
        public IEnumerable<LibraryItem> listOfLibraryItems { get; set; }
    }
}