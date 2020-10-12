using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ConsidLibrary.Models
{
    public static class SwitchHelper
    {
        public static IQueryable<LibraryItem> SortOrder(string sortOrder, IQueryable<LibraryItem> libraryItems)
        {
            libraryItems = from s in libraryItems select s;
            switch (sortOrder)
            {
                case "type_desc":
                    libraryItems = libraryItems.OrderByDescending(s => s.Type);
                    break;
                case "Type":
                    libraryItems = libraryItems.OrderBy(s => s.Type);
                    break;
                case "categoryName_desc":
                    libraryItems = libraryItems.OrderByDescending(s => s.Category.CategoryName);

                    break;
                default:
                    libraryItems = libraryItems.OrderBy(s => s.Category.CategoryName);
                    break;
            }
            return libraryItems;
        }

    }
}