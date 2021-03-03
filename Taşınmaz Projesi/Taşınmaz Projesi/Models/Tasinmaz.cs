using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.UI;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taşınmaz_Projesi.Models
{
    public class Tasinmaz
    {
        [Key]
        public int TasinmazID { get; set; }
        [Required(ErrorMessage = "Lütfen birini seçiniz")]
        [Display(Name = "İl")]
        public int IlID { get; set; }
        [Required(ErrorMessage = "Lütfen birini seçiniz")]
        [Display(Name = "İlçe")]
        public int IlceID { get; set; }
        [Required(ErrorMessage = "Lütfen birini seçiniz")]
        public int MahalleID { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public string Ada { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public string Parsel { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public string Nitelik { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [UIHint("MultilineText")]
        public string Adres { get; set; }

        [ForeignKey ("IlID")]
        public virtual Il Il { get; set; }
        [ForeignKey("IlceID")]
        public virtual Ilce Ilce { get; set; }
        [ForeignKey("MahalleID")]
        public virtual Mahalle Mahalle { get; set; }

    }
}