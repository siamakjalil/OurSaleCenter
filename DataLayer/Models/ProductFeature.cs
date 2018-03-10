using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
  public  class ProductFeature
    {
           [Key]
      public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      public int ProductId { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      public int FeatureId { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      public string FeatureValue { get; set; }

      public ProductFeature()
      {
          
      }

      public virtual Product Product { get; set; }
      public virtual Feature Feature { get; set; }
        
    }
}
