using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAlbum.Models;
using System.IO;

namespace MvcAlbum.Controllers
{
    public class PictureController : Controller
    {
        private AlbumContext db = new AlbumContext();

        //
        // GET: /Picture/

        public ActionResult Index(int albumID)
        {
            return View(db.Pictures.Where(a => a.AlbumID == albumID).ToList());
        }

        //
        // GET: /Picture/Details/5

        public ActionResult Details(int id = 0)
        {
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        //
        // GET: /Picture/Create

        public ActionResult Create(int id)
        {
            Picture picture = new Picture();
            picture.AlbumID = id;
            return View(picture);
        }

        //
        // POST: /Picture/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Picture picture)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var path = Server.MapPath("~/Images/album/" + picture.AlbumID + "/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var extension = Path.GetExtension(file.FileName);
                    var fileName = Guid.NewGuid().ToString() + extension;
                    file.SaveAs(Path.Combine(path, fileName));
                    picture.Path = fileName;
                }

                db.Pictures.Add(picture);
                db.SaveChanges();
                return RedirectToAction("Details", "Album", new { id = picture.AlbumID });
            }

            return View(picture);
        }

        //
        // GET: /Picture/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        //
        // POST: /Picture/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Picture picture)
        {
            if (ModelState.IsValid)
            {
                var old = db.Pictures.Single(a => a.PictureID == picture.PictureID);
                picture.Path = old.Path;

                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var path = Server.MapPath("~/Images/album/"+picture.AlbumID+"/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var extension = Path.GetExtension(file.FileName);
                    var fileName = Guid.NewGuid().ToString() + extension;
                    file.SaveAs(Path.Combine(path, fileName));
                    picture.Path = fileName;

                    // delete old Path
                    if (!string.IsNullOrEmpty(old.Path))
                    {
                        System.IO.File.Delete(Path.Combine(path, old.Path));
                    }
                }

                db.Entry(old).CurrentValues.SetValues(picture);
                db.SaveChanges();
                return RedirectToAction("Details", "Album", new { id = picture.AlbumID });
            }
            return View(picture);
        }

        //
        // GET: /Picture/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        //
        // POST: /Picture/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Picture picture = db.Pictures.Find(id);
            var fileName = picture.Path;
            db.Pictures.Remove(picture);
            db.SaveChanges();

            var path = Server.MapPath("~/Images/album/" + picture.AlbumID + "/");            
            path=Path.Combine(path, fileName);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            return RedirectToAction("Details", "Album", new { id = picture.AlbumID });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}