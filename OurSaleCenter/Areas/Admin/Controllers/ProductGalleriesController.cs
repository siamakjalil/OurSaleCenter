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

namespace OurSaleCenter.Areas.Admin.Controllers
{
    public class ProductGalleriesController : Controller
    {
        private OurSaleCenterContext db = new OurSaleCenterContext();

       

       

        // GET: Admin/ProductGalleries/Create
        public ActionResult Create(int id)
        {
            ViewBag.Id = id;
            return View(db.ProductGalleries.Where(u => u.ProductId == id).ToList());
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id,HttpPostedFileBase[] gallery)
        {
            try
            {
                var imgName="";
                foreach (var item in gallery)
                {
                    if (CheckContentImage.IsImage(item))
                    {
                        imgName = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
                        item.SaveAs(Server.MapPath("/Images/Products/" + imgName));
                        ProductGallery productGallery=new ProductGallery()
                        {
                            ProductId = id,
                            ImageName = imgName
                        };
                        db.ProductGalleries.Add(productGallery);
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "تصویر معتبر نیست");

                        return View(db.ProductGalleries.Where(u => u.ProductId == id).ToList());
                    }
                }
                db.SaveChanges();
                return Redirect("/admin/products");
            }
            catch (Exception)
            {


                return View(db.ProductGalleries.Where(u => u.ProductId == id).ToList());
            }
                
               




           
        }


        // POST: Admin/ProductGalleries/Delete/5
       
        public void DeleteConfirmed(int id)
        {
            ProductGallery productGallery = db.ProductGalleries.Find(id);
            System.IO.File.Delete(Server.MapPath("/Images/Products/" + productGallery.ImageName));
            db.ProductGalleries.Remove(productGallery);
            db.SaveChanges();

           
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
