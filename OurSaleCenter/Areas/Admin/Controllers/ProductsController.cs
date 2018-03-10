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
using Newtonsoft.Json;

namespace OurSaleCenter.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private OurSaleCenterContext db = new OurSaleCenterContext();

        // GET: Admin/Products
        public ActionResult Index()
        {
            Session["FeaturesList"] = null;
            var products = db.Products.Include(p => p.ProductCategory);
            return View(products.ToList());
        }

        public ActionResult IndexByGroupId(int id)
        {
            ViewBag.Name = db.ProductCategories.Find(id).Title;
            ViewBag.Id = id;
            var products = db.Products.Where(u=>u.ProductCategoryId==id);
            return View(products.ToList());
        }
        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            Session["FeaturesList"] = null;
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "Id", "Title");
            
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductCategoryId,Title,ShortDescription,Text,Price,Image")] Product product,HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                if (imgUp != null)
                {
                    if (CheckContentImage.IsImage(imgUp))
                    {
                        product.Image = Guid.NewGuid().ToString() + Path.GetExtension(imgUp.FileName);
                        imgUp.SaveAs(Server.MapPath("/Images/Products/" + product.Image));
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "تصویر معتبر نیست");

                        return View(product);
                    }
                }
                db.Products.Add(product);
                List<FeaturesViewModel> list = new List<FeaturesViewModel>();
                if (Session["FeaturesList"] != null)
                {
                    list = Session["FeaturesList"] as List<FeaturesViewModel>;
                }
                foreach(var item in list)
                {
                    db.ProductFeatures.Add(new ProductFeature()
                    {
                        FeatureId = item.FeatureID,
                        FeatureValue = item.FeatureValue,
                        ProductId = product.Id
                    });
                }
                Session["FeaturesList"] = null;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "Id", "Title", product.ProductCategoryId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            Session["FeaturesList"] = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "Id", "Title", product.ProductCategoryId);
            if(product.ProductFeatures.Any())
            {
                Session["FeaturesList"] = product.ProductFeatures.Select(s => new FeaturesViewModel()
                {
                    FeatureID = s.FeatureId,
                    FeatureValue = s.FeatureValue,
                    FeatureTitle = s.Feature.FeatureTitle,

                }).ToList();
            }
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductCategoryId,Title,ShortDescription,Text,Price,Image")] Product product,HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                if (imgUp != null)
                {
                    if (CheckContentImage.IsImage(imgUp))
                    {
                        if (product.Image != null)
                        {
                            System.IO.File.Delete(Server.MapPath("/Images/products/" + product.Image));
                        }

                        product.Image = Guid.NewGuid().ToString() + Path.GetExtension(imgUp.FileName);
                        imgUp.SaveAs(Server.MapPath("/Images/products/" + product.Image));
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "تصویر معتبر نیست");

                        return View(product);
                    }
                }
                db.Entry(product).State = EntityState.Modified;
                db.ProductFeatures.Where(u => u.ProductId == product.Id).ToList().ForEach(u => db.ProductFeatures.Remove(u));
                List<FeaturesViewModel> list = new List<FeaturesViewModel>();
                if (Session["FeaturesList"] != null)
                {
                    list = Session["FeaturesList"] as List<FeaturesViewModel>;
                }
                foreach (var item in list)
                {
                    db.ProductFeatures.Add(new ProductFeature()
                    {
                        FeatureId = item.FeatureID,
                        FeatureValue = item.FeatureValue,
                        ProductId = product.Id
                    });
                }
                Session["FeaturesList"] = null;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "Id", "Title", product.ProductCategoryId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return PartialView(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            if (product.Image != null)
            {
                System.IO.File.Delete(Server.MapPath("/Images/Products/" + product.Image));
            }
            db.Products.Remove(product);
            db.ProductFeatures.Where(u => u.ProductId == id).ToList().ForEach(u => db.ProductFeatures.Remove(u));
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

        #region ProductFeatures
        public ActionResult GetFeatures()
        {
            ViewBag.FeatureId = new SelectList(db.Features, "FeatureId", "FeatureTitle");
            List<FeaturesViewModel> list = new List<FeaturesViewModel>();
            if (Session["FeaturesList"] != null)
            {
                list= Session["FeaturesList"] as List<FeaturesViewModel>;
            }
            return PartialView(list);
        }
        [HttpPost]
        public ActionResult AddFeatures()
        {
            var detail = JsonConvert.DeserializeObject<FeaturesViewModel>(Request.Form["FeatureDetails"]);
            List<FeaturesViewModel> list = new List<FeaturesViewModel>();
            if (Session["FeaturesList"] != null)
            {
                list = Session["FeaturesList"] as List<FeaturesViewModel>;
            }
            list.Add(new FeaturesViewModel()
            {
                FeatureID = detail.FeatureID,
                FeatureTitle = detail.FeatureTitle,
                FeatureValue = detail.FeatureValue
            });
            Session["FeaturesList"] = list;
            ViewBag.FeatureId = new SelectList(db.Features, "FeatureId", "FeatureTitle");
            return PartialView("GetFeatures", list);
        }
        public ActionResult RemoveFeatures()
        {
            var detail = JsonConvert.DeserializeObject<FeaturesViewModel>(Request.Form["FeatureDetails"]);
            List<FeaturesViewModel> list = new List<FeaturesViewModel>();
            if (Session["FeaturesList"] != null)
            {
                list = Session["FeaturesList"] as List<FeaturesViewModel>;
            }
            var model = list.FirstOrDefault(u => u.FeatureID == detail.FeatureID && u.FeatureValue == detail.FeatureValue);
            if (model != null)
            {
                list.Remove(model);
                Session["FeaturesList"] = list;
            }
            ViewBag.FeatureId = new SelectList(db.Features, "FeatureId", "FeatureTitle");
            return PartialView("GetFeatures", list);
        }
        #endregion
    }
}
