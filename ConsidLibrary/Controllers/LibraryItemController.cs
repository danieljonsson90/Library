using ConsidLibrary.DAL;
using ConsidLibrary.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

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
            libraryItems = SwitchHelper.SortOrder(sortOrder, libraryItems);
            var size = libraryItems.Count();
            string[] acronyms = new string[size];

            var libraryItemsList = libraryItems.ToList();
            acronyms = StringHelper.CreateAccronyms(acronyms, libraryItemsList, size);
           
            var viewModel = new ViewModel { listOfLibraryItems = libraryItemsList, titleAcronyms = acronyms };

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
        public ActionResult Create([Bind(Include = "CategoryId,Type")] LibraryItem libraryItem)
        {

            if (ModelState.IsValid)
            {
                if (libraryItem.CategoryId != 0)
                {
                    switch (libraryItem.Type)
                    {
                        case "Book":
                            return RedirectToAction("Book", new LibraryItem { Id = libraryItem.Id, CategoryId = libraryItem.CategoryId, Type = libraryItem.Type });
                        case "Audio Book":
                            return RedirectToAction("AudioBook", new LibraryItem { Id = libraryItem.Id, CategoryId = libraryItem.CategoryId, Type = libraryItem.Type });
                        case "Reference Book":
                            return RedirectToAction("ReferenceBook", new LibraryItem { Id = libraryItem.Id, CategoryId = libraryItem.CategoryId, Type = libraryItem.Type });
                        case "DVD":
                            return RedirectToAction("DVD", new LibraryItem { Id = libraryItem.Id, CategoryId = libraryItem.CategoryId, Type = libraryItem.Type });
                        default:
                            return RedirectToAction("Index");
                    }
                }else
                {
                    
                    return RedirectToAction("CreateCategory");
                }
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


        public ActionResult Book(LibraryItem libraryItem)
        {
            if (libraryItem == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = new Book { };
            
            //LibraryItem Item = db.LibraryItem.Find(libraryItem.Id);
            return View(book);

        }

        [HttpPost, ActionName("Book")]
        [ValidateAntiForgeryToken]
        public ActionResult BookPost(LibraryItem libraryItem)
        {
            if (libraryItem == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = new Book { };
            if (TryUpdateModel(book, "",
               new string[] { "Title", "Author","Pages" }))
            {
                try
                {
                    
                    libraryItem.IsBorrowable = true;
                    db.LibraryItem.Add(libraryItem);
                    db.SaveChanges();

                    return RedirectToAction("Details", new { id = libraryItem.Id });
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(book);
        }

        public ActionResult ReferenceBook(LibraryItem libraryItem)
        {
            if (libraryItem == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var referenceBook = new ReferenceBook { };

            //LibraryItem Item = db.LibraryItem.Find(libraryItem.Id);
            return View(referenceBook);

        }

        [HttpPost, ActionName("ReferenceBook")]
        [ValidateAntiForgeryToken]
        public ActionResult ReferenceBookPost(LibraryItem libraryItem)
        {
            if (libraryItem == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var referenceBook = new ReferenceBook { };
            if (TryUpdateModel(referenceBook, "",
               new string[] { "Title", "Author", "Pages" }))
            {
                try
                {
                    libraryItem.IsBorrowable = false;
                    db.LibraryItem.Add(libraryItem);
                    db.SaveChanges();

                    return RedirectToAction("Details", new { id = libraryItem.Id });
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(referenceBook);
        }

        public ActionResult DVD(LibraryItem libraryItem)
        {
            if (libraryItem == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dvd = new DVD { };

            //LibraryItem Item = db.LibraryItem.Find(libraryItem.Id);
            return View(dvd);

        }

        [HttpPost, ActionName("DVD")]
        [ValidateAntiForgeryToken]
        public ActionResult DVDPost(LibraryItem libraryItem)
        {
            if (libraryItem == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dvd = new DVD { };
            if (TryUpdateModel(dvd, "",
               new string[] { "Title", "RunTimeMinutes" }))
            {
                try
                {
                    libraryItem.IsBorrowable = true;
                    db.LibraryItem.Add(libraryItem);
                    db.SaveChanges();

                    return RedirectToAction("Details", new { id = libraryItem.Id });
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(dvd);
        }

        public ActionResult AudioBook(LibraryItem libraryItem)
        {
            /*if (libraryItem.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            var audioBook = new AudioBook { };

            //LibraryItem Item = db.LibraryItem.Find(libraryItem.Id);
            return View(audioBook);

        }

        [HttpPost, ActionName("AudioBook")]
        [ValidateAntiForgeryToken]
        public ActionResult AudioBookPost(LibraryItem libraryItem)
        {
            if (libraryItem == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Have to create the right class (audioBook here) so that the required fields are right.
            var audioBook = new AudioBook { };
            if (TryUpdateModel(audioBook, "",
               new string[] { "Title", "RunTimeMinutes" }))
            {
                try
                {
                    libraryItem.IsBorrowable = true;
                    db.LibraryItem.Add(libraryItem);
                    db.SaveChanges();

                    return RedirectToAction("Details", new { id = libraryItem.Id });
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(audioBook);
        }


        [HttpGet]
        [ActionName("CheckOut")]
        public ActionResult CheckOut_Get(LibraryItem libraryItem)
        {
            if (libraryItem == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var checkOut = new CheckOut { };
            return View(checkOut);

        }

        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOutPost(LibraryItem libraryItem)
        {
            if (libraryItem == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var checkOut = new CheckOut { };
            
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Details", new { id = libraryItem.Id });
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", libraryItem.CategoryId);
            return View(checkOut);
        }

        public ActionResult CheckIn(int? id)
        {
            var libraryItem = db.LibraryItem.Find(id);
            if (libraryItem == null)
            {
                return HttpNotFound();
            }
            if (libraryItem.IsBorrowable)
            {
                if (libraryItem.Borrower != null)
                {
                    libraryItem.BorrowDate = null;
                    libraryItem.Borrower = null;
                    db.SaveChanges();
                    return View(libraryItem);
                }
            }

            return RedirectToAction("ItemNotBorrowed");

        }

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


        // GET: LibraryItem/Edit/5
        /* public ActionResult Edit(int? id)
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
         }*/

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,Type")] LibraryItem libraryItem)
        {
            if (ModelState.IsValid)
            {
                switch(libraryItem.Type)
                {
                    case "Book":
                        return RedirectToAction("EditBook",libraryItem );
                    case "Audio Book":
                        return RedirectToAction("AudioBook", new LibraryItem { Id = libraryItem.Id, CategoryId = libraryItem.CategoryId, Type = libraryItem.Type });
                    case "Reference Book":
                        return RedirectToAction("ReferenceBook", new LibraryItem { Id = libraryItem.Id, CategoryId = libraryItem.CategoryId, Type = libraryItem.Type });
                    case "DVD":
                        return RedirectToAction("DVD", new LibraryItem { Id = libraryItem.Id, CategoryId = libraryItem.CategoryId, Type = libraryItem.Type });
                    default:
                        return RedirectToAction("Index");
                }
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", libraryItem.CategoryId);
            return View(libraryItem);
        }*/

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

        
        /// <summary>
        /// Below are the Views that is displayed to inform that some actions or functions 
        /// are not possible or allowed in the application. Like a page that informs that it is not
        /// possible to create a library item when there is no Category created.
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateCategory()
        {
            return View();
        }
        public ActionResult NotBorrowable()
        {
            return View();
        }

        public ActionResult ItemNotBorrowed()
        {
            return View();
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
