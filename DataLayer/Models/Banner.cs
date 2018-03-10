using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Banner
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "مقدار {0} نمیتواند بیشتر از {1} باشد")]
        public string Title { get; set; }
        [Display(Name = "تصویر")]
     
        [MaxLength(150, ErrorMessage = "مقدار {0} نمیتواند بیشتر از {1} باشد")]
        public string ImageName { get; set; }
        [Display(Name = "تاریخ ثبت")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [Required]
        public System.DateTime DateTime { get; set; }
        [Display(Name = "فعال")]
        [Required]
        public bool IsActive { get; set; }
    }
}
