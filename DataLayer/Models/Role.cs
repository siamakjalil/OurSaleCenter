using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان سیستمی نقش")]
        public string RoleName { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان نمایشی نقش")]
        public string RoleTitle { get; set; }

        public Role()
        {
            
        }

        public virtual List<Admin> Admin { get; set; }
        
    }
}
