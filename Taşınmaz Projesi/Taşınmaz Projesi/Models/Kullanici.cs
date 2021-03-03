using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Taşınmaz_Projesi.Models
{
    public class Kullanici
    {
        [Key] //identity yi true yapar.
        public int KullaniciID { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public string KullaniciAdi { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public string KullaniciSoyadi { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [PasswordPropertyText]
        public string Parola { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public string Rol { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [UIHint("MultilineText")]
        public string Adres { get; set; }

    }
}