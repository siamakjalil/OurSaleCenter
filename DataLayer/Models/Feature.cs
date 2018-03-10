using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Feature
    {
        [Key]
        public int FeatureId { get; set; }
        [Display(Name = "عنوان ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "مقدار {0} نمیتواند بیشتر از {1} باشد")]
        public string FeatureTitle { get; set; }

        public Feature()
        {
            
        }

        public virtual List<ProductFeature> ProductFeatures { get; set; }

    }
    
}
