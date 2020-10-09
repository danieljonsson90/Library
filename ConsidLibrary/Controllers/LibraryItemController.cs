using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConsidLibrary.DAL;
using ConsidLibrary.Models;

namespace ConsidLibrary.Controllers
{
    public class LibraryItemController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: LibraryItem
        public ActionResult Index(string sortOrder)
        {
            ViewBag.CategorySortParm = String.IsNullOrEmpty(sortOrder) ? "categoryName_desc" : "";
            ViewBag.TypeSortParm = sortOrder == "Type" ? "type_desc" : "Type";

            var libraryItems = db.LibraryItem.Include(l => l.Category);
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
            var size = libraryItems.Count();
            string[] acronyms = new string[size];
            
            var item = libraryItems.ToList();

            for (int i = 0; i < libraryItems.Count(); i++)
            {
                var res = item[i].Title.Split(' ');
                acronyms[i] += "(";
                for (int j = 0; j < res.Length; j++)
                {
                    var c = res[j][0];
                    acronyms[i]+= c.ToString().ToUpper();
                }
                acronyms[i] += ")";
            }
            
            var viewModel = new ViewModel { listOfLibraryItems = libraryItems.ToList(), titleAcronyms = acronyms };
            
            return View(viewModel);
        }

        private string GetAcronym(string title)
        {
            return title.Split(' ').ToList().ToString();
           /* char[] array = null;
            if(title == null)
            {
                return null;
            }
            int count = 0;
            array[count] = title[0];
            for(int i = 2; i<title.Length; i++)
            {
                if(title[i-1] == ' ')
                {
                    count++;
                    array[count] = title[i];
                }
            }
            return new string(array);*/
        }

        // GET: LibraryItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibraryItem libraryItem = db.LibraryItem.Find(id);
            Category category = db.Categories.Find(libraryItem.CategoryId);
            var viewModel = new ViewModel { libraryItem = libraryItem, category = category };
            if (category == null)
            {
                return HttpNotFound();
            }
            if (libraryItem == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: LibraryItem/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: LibraryItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Title,Author,Pages,RunTimeMinutes,IsBorrowable,Borrower,BorrowDate,Type")] LibraryItem libraryItem)
        {
            if (ModelState.IsValid)
            {
                db.LibraryItem.Add(libraryItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", libraryItem.CategoryId);
            return View(libraryItem);
        }

        public ActionResult CheckOut(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibraryItem libraryItem = db.LibraryItem.Find(id);
            if (libraryItem == null)
            {
                return HttpNotFound();
            }
            if (libraryItem.IsBorrowable)
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", libraryItem.CategoryId);
            return View(libraryItem);
            }
            else
            {
                return RedirectToAction("NotBorrowable");
            }
        }

        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOutPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var libraryItemToUpdate = db.LibraryItem.Find(id);
                if (TryUpdateModel(libraryItemToUpdate, "",
                   new string[] { "Borrower", "BorrowDate" }))
                {
                    try
                    {
                        db.SaveChanges();

                        return RedirectToAction("Details", new { id = id });
                    }
                    catch (DataException /* dex */)
                    {
                        //Log the error (uncomment dex variable name and add a line here to write a log.
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }
                return View(libraryItemToUpdate);
        }

        public ActionResult NotBorrowable()
        {
            return View();
        }

        public ActionResult CheckIn(int? id)
        {
            var libraryItem = db.LibraryItem.Find(id);
            if (libraryItem == null)
            {
                return HttpNotFound();
            }
            if (libraryItem.Borrower != null)
            {
                libraryItem.BorrowDate = null;
                libraryItem.Borrower = null;
                db.SaveChanges();
                return View(libraryItem);
            }
            else
            {
                return RedirectToAction("ItemNotBorrowed");
                
            }
        }

        public ActionResult ItemNotBorrowed()
        {
            return View();
        }

        // GET: LibraryItem/Edit/5
        public ActionResult Edit(int? id)
         {
             if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             LibraryItem libraryItem = db.LibraryItem.Find(id);
             if (libraryItem == null)
             {
                 return HttpNotFound();
             }
             ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", libraryItem.CategoryId);
             return View(libraryItem);
         }

        

        // POST: LibraryItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,Title,Author,Pages,RunTimeMinutes,IsBorrowable,Borrower,BorrowDate,Type")] LibraryItem libraryItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(libraryItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", libraryItem.CategoryId);
            return View(libraryItem);
        }

        // GET: LibraryItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibraryItem libraryItem = db.LibraryItem.Find(id);
            if (libraryItem == null)
            {
                return HttpNotFound();
            }
            return View(libraryItem);
        }

        // POST: LibraryItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LibraryItem libraryItem = db.LibraryItem.Find(id);
            db.LibraryItem.Remove(libraryItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
