using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "مقدار {0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "شماره موبایل")]
        public string MobileNumber { get; set; }

        [Display(Name = "کلمه عبور")]
        [MaxLength(50, ErrorMessage = "مقدار {0} نمیتواند بیشتر از {1} باشد")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Display(Name = "نام")]
        [MaxLength(150, ErrorMessage = "مقدار {0} نمیتواند بیشتر از {1} باشد")]
        public string Name { get; set; }
        [Display(Name = "ایمیل")]
        [MaxLength(150, ErrorMessage = "مقدار {0} نمیتواند بیشتر از {1} باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده نامعتبر است")]

        public string Email { get; set; }
        [Display(Name = "تلفن ثابت")]
        [MaxLength(50, ErrorMessage = "مقدار {0} نمیتواند بیشتر از {1} باشد")]
        public string Phone { get; set; }
       
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "مقدار {0} نمیتواند بیشتر از {1} باشد")]
        public string ActiveCode { get; set; }
        [Display(Name = "تاریخ ثبت نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateTime { get; set; }
        [Display(Name = "فعال")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        
        public bool IsActive { get; set; }
    }
}
