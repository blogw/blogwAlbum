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
    public class AlbumController : Controller
    {
        private AlbumContext db = new AlbumContext();

        //
        // GET: /Album/

        public ActionResult Index()
        {
            return View(db.Albums.ToList());
        }

        //
        // GET: /Album/Details/5

        public ActionResult Details(int id = 0)
        {
            Album album = db.Albums.Find(id);
            album.Pictures = db.Pictures.Where(a => a.AlbumID == id).ToArray<Picture>();

            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        //
        // GET: /Album/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Album/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = Guid.NewGuid().ToString() + extension;
                    var path = Path.Combine(Server.MapPath("~/Images/album/"), fileName);
                    file.SaveAs(path);
                    album.Thumbnail = fileName;
                }

                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(album);
        }

        //
        // GET: /Album/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        //
        // POST: /Album/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                var old = db.Albums.Single(a => a.AlbumID == album.AlbumID);
                album.Thumbnail = old.Thumbnail;                

                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var path = Server.MapPath("~/Images/album/");
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = Guid.NewGuid().ToString() + extension;
                    file.SaveAs(Path.Combine(path, fileName));
                    album.Thumbnail = fileName;

                    // delete old thumbnail
                    System.IO.File.Delete(Path.Combine(path, old.Thumbnail));
                }

                //db.Entry(album).State = EntityState.Modified;
                //db.Albums.Attach(album);                
                db.Entry(old).CurrentValues.SetValues(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(album);
        }

        //
        // GET: /Album/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        //
        // POST: /Album/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IEnumerable<Picture> pics=db.Pictures.Where(a => a.AlbumID == id).ToList<Picture>();
            var parent = Server.MapPath("~/Images/album/" + id + "/");

            foreach(Picture p in pics)
            {                
                var path = Path.Combine(parent, p.Path);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                db.Pictures.Remove(p);
            }

            if (Directory.Exists(parent))
            {
                Directory.Delete(parent);
            }

            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}