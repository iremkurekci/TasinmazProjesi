using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Taşınmaz_Projesi.Models
{
    public class Mahalle
    {
        [Key]
        public int MahalleID { get; set; }
        public string MahalleAdi { get; set; }
        public int IlceID { get; set; }

        [ForeignKey("IlceID")]
        public virtual Ilce Ilce { get; set; }

    }
}