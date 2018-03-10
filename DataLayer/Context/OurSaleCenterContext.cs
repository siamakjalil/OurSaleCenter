using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
  public  class OurSaleCenterContext:DbContext
    {
      public DbSet<Admin> Admins { get; set; }
      public DbSet<Role> Roles { get; set; }
      public DbSet<Product> Products { get; set; }
      public DbSet<ProductCategory> ProductCategories { get; set; }
      public DbSet<ProductFeature> ProductFeatures { get; set; }
      public DbSet<Feature> Features { get; set; }
      public DbSet<User> Users { get; set; }
      public DbSet<ProductGallery> ProductGalleries { get; set; }
      public DbSet<Banner> Banners { get; set; }


    }
}
