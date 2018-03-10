using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "گروه")]
        [Required]
        public int  ProductCategoryId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "مقدار {0} نمیتواند بیشتر از {1} باشد")]
        public string Title { get; set; }
        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "مقدار {0} نمیتواند بیشتر از {1} باشد")]

        public string ShortDescription { get; set; }
        [Display(Name = "متن")]
        [MaxLength(500, ErrorMessage = "مقدار {0} نمیتواند بیشتر از {1} باشد")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float Price { get; set; }
        [Display(Name = "تصویر")]
     
        [MaxLength(150, ErrorMessage = "مقدار {0} نمیتواند بیشتر از {1} باشد")]
        public string Image { get; set; }

        public Product()
        {
            
        }

        public virtual ProductCategory ProductCategory { get; set; }
        public virtual List<ProductFeature> ProductFeatures { get; set; }
        public virtual List<ProductGallery> ProductGalleries { get; set; }


    }
}
