using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taşınmaz_Projesi.Models
{
    public class TasinmazPaging
    {
        public List<Tasinmaz> TasinmazListesi { get; set; }
        public int totalCount { get; set; }

    }
}