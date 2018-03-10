using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using OurSaleCenter;

namespace OurSaleCenter.Areas.Admin.Controllers
{
    public class BannersController : Controller
    {
        private OurSaleCenterContext db = new OurSaleCenterContext();

        // GET: Admin/Banners
        public ActionResult Index(int pageId=1)
        {

            int skip = (pageId - 1) * 10;
            int count = db.Banners.Count();
            var div = count / 10;
            ViewBag.pageId = pageId;
            if (count - (div * 10) == 0)
            {
                ViewBag.pageCount = div;
            }
            else
            {
                ViewBag.pageCount = div + 1;
            }

            return View(db.Banners.OrderByDescending(u=>u.DateTime).Skip(skip).Take(10).ToList());
        }

        // GET: Admin/Banners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner Banners = db.Banners.Find(id);
            if (Banners == null)
            {
                return HttpNotFound();
            }
            return View(Banners);
        }

        // GET: Admin/Banners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Banners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ImageName,IsActive,DateTime")] Banner Banners,HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                if (imgUp != null)
                {
                    if (CheckContentImage.IsImage(imgUp))
                    {
                        Banners.ImageName =Guid.NewGuid().ToString()+Path.GetExtension(imgUp.FileName);
                        imgUp.SaveAs(Server.MapPath("/Images/Banners/" + Banners.ImageName));
                    }
                    else
                    {
                        ModelState.AddModelError("ImageName", "تصویر معتبر نیست");
                        
                        return View(Banners);
                    }
                }
                else
                {
                    ModelState.AddModelError("ImageName", "تصویر را وارد کنید");
                }
                Banners.DateTime=DateTime.Now;
                db.Banners.Add(Banners);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Banners);
        }

        // GET: Admin/Banners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner Banners = db.Banners.Find(id);
            if (Banners == null)
            {
                return HttpNotFound();
            }
            return View(Banners);
        }

        // POST: Admin/Banners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,ImageName,IsActive,DateTime")] Banner Banners,HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                if (imgUp != null)
                {
                    if (CheckContentImage.IsImage(imgUp))
                    {
                        if (Banners.ImageName != null)
                        {
                            System.IO.File.Delete(Server.MapPath("/Images/Banners/" + Banners.ImageName));
                        }

                        Banners.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imgUp.FileName);
                        imgUp.SaveAs(Server.MapPath("/Images/Banners/" + Banners.ImageName));
                    }
                    else
                    {
                        ModelState.AddModelError("ImageName", "تصویر معتبر نیست");
                       
                        return View(Banners);
                    }
                }
                db.Entry(Banners).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Banners);
        }

        // GET: Admin/Banners/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner Banners = db.Banners.Find(id);
            ViewBag.Name = Banners.Title;
            if (Banners == null)
            {
                return HttpNotFound();
            }
            return PartialView(Banners);
        }

        // POST: Admin/Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Banner Banners = db.Banners.Find(id);
            if (Banners.ImageName != null)
            {
                System.IO.File.Delete(Server.MapPath("/Images/Banners/" + Banners.ImageName));
            }
            db.Banners.Remove(Banners);
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

        public ActionResult test()
        {
            return View();
        }
    }
}
