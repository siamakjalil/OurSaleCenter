using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "مقدار {0} نمیتواند بیشتر از {1} باشد")]
        public string Title { get; set; }

        public ProductCategory()
        {
            
        }

        public virtual List<Product> Products { get; set; }


    }
    
 
}
