using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ConsidLibrary.Models
{
    [MetadataType(typeof(Category))]
    public partial class Categorys
    {
    }
    public class Category
    {
        [Required(ErrorMessage = "CategoryId is required. Create a Category to choose a categoryId.")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "CateoryName is required.")]
        public string CategoryName { get; set; }
    }
}