using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Taşınmaz_Projesi.Models
{
    public class Ilce
    {
        [Key]
        public int IlceID { get; set; }
        public string IlceAdi { get; set; }
        public int IlID { get; set; }

        [ForeignKey("IlID")]
        public virtual Il Il { get; set; }

    }
}