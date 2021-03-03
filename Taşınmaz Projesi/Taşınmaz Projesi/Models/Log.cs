using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace Taşınmaz_Projesi.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int KullaniciID { get; set; }
        public string Durum { get; set; } // başarılı/başarısız
        public string IslemTipi { get; set; } // create/edit/delete/login/logout vs..
        public DateTime TarihSaat { get; set; }
        public string Ip { get; set; }
        public long IslemSuresi { get; set; }
        public string Aciklama { get; set; } //ayrıntılar
    }


   
}