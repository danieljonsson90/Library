using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsidLibrary.Models
{
    public static class StringHelper
    {
        public static string[] CreateAccronyms(string[] acronyms,List<LibraryItem> libraryItems,int size)
        {
            for (int i = 0; i < size; i++)
            {
                var res = libraryItems[i].Title.Split(' ');
                acronyms[i] += "(";
                for (int j = 0; j < res.Length; j++)
                {   if (res[j] == "") continue;
                    var c = res[j][0];
                    acronyms[i] += c.ToString().ToUpper();
                }
                acronyms[i] += ")";
            }
            return acronyms;
        }
    }
}