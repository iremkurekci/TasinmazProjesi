using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Taşınmaz_Projesi.Models
{
    public class Login
    {
        [Key] //identity yi true yapar.
        public int KullaniciID { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [MaxLength(25)]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [PasswordPropertyText]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [MaxLength(25)]
        [MinLength(8)]
        [Display(Name = "Parola")]
        public string Parola { get; set; }
        public string Rol { get; set; }
       
        public List<Kullanici> Kullanici { get; set; }
        public List<Tasinmaz> Tasinmaz { get; set; }
    }
}