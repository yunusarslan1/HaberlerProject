using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HaberlerProject.Models.ViewModel
{
    public class UserVM
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı girilmesi zorunludur.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifre girilmesi zorunludur  ve minimum 6 karakter olmalıdır.")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}