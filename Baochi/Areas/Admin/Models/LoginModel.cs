using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Baochi.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Mời nhập email")]
        public string email { set; get; }
        [Required(ErrorMessage ="Mời nhập password")]
        public string password { set; get; }
        public bool rememberMe { set; get; }
    }
}