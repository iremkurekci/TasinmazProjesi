using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Taşınmaz_Projesi.Models
{
    public class Il
    {
        [Key]
        public int IlID { get; set; }
        public string IlAdi { get; set; }

    }
}