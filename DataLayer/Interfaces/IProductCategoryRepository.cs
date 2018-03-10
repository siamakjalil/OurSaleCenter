using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
  public  interface IProductCategoryRepository:IDisposable
    {
        List<ProductCategory> GetAllCategory();
      ProductCategory GetCategoryById();
      bool InsertCategory(ProductCategory productCategory);
      bool UpdateCategory(ProductCategory productCategory);
      bool DeleteCategory(ProductCategory productCategory);
      void Save();

    }
}
