using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Taşınmaz_Projesi.Context;

namespace Taşınmaz_Projesi.Models
{
    //Action filter dediğimiz aslında action metodların çalışma öncesi ve sonrasına hayat veren özel attribute sınıflarıdır.

    /*Partial class, bir class’ ı birden fazla class olarak bölmemize olanak sağlar. Fiziksel olarak birden fazla parça
    ile oluşan partial class’ lar, çalıştığı zaman tek bir class olarak görev yapar.*/
    public partial class LogAttribute : ActionFilterAttribute
    {
        private TasinmazContext db = new TasinmazContext();
        //buradan sonrası attribute aracılığıyla yapılacak işlemler
        private Stopwatch stopWatch; //kronometre

        public LogAttribute()
        {
            stopWatch = new Stopwatch();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext) 
        {
            //işlem başladığında kronometreyi resetle ve yeniden başlat 
            stopWatch.Reset();
            stopWatch.Restart();
        }

        public void GetKullanıcı(Kullanici kullanici) //kullanıcıyı çektim
        {
            var GetKullanici = db.kullanici.Where(k => k.Email == kullanici.Email).Select(k => k.KullaniciID).FirstOrDefault(); 
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //işlem bittiğinde kronometreyi durdur.
            stopWatch.Stop();

            //Log model class'ının alanlarını doldurur
            var request = filterContext.HttpContext.Request; //gelen requesti alır

            Log lg = new Log();
            lg.TarihSaat = DateTime.Now;
            lg.IslemTipi = SerializeRequest(request); //bu metod aşağıda tanımlı. Yukarıda alınan requesti serialize edip string olarak yazar
            lg.IslemSuresi = stopWatch.ElapsedMilliseconds; //işlem süresini tutar
            lg.Ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress; //requestin hangi serverdan yapıldığını yani IP'sini bulur
            lg.KullaniciID = db.kullanici.Where(k => k.KullaniciID == k.KullaniciID).Select(k => k.KullaniciID).FirstOrDefault();
            lg.Aciklama = request.RawUrl; //erişilen sayfanın ham url'i
            lg.Durum = db.kullanici.Where(k => k.Rol == k.Rol).Select(k => k.Rol).FirstOrDefault();
            db.log.Add(lg);
            db.SaveChanges(); //Log class'ı veritabanında kaydedildi
            base.OnActionExecuted(filterContext); // işlem devam etsin
         }

        private string SerializeRequest(HttpRequestBase request)
        {
            string result = string.Empty;

            //#region düzeni sağlayabilmek adına kod bloğu oluşturur
            #region Form 
            List<string> formValues = new List<string>(); // eğer sayfada bir form varsa, formda gönderilen tüm inputları listeler
            if(request.Form.AllKeys != null && request.Form.AllKeys.ToList().Count > 0)
            {
                foreach(string s in request.Form.AllKeys.ToList())
                {
                    formValues.Add(request.Form[s]);
                }
            }
            #endregion
            //JSON --> JavaScript nesne gösterimi
            //Json.Encode --> string tipindeki data objesini json formatına çevirir

            result = Json.Encode(new { 
            
                request.Form, //gönderilen formun tamamı
                formValues, //formdaki inputlara girilen veriler
                request.Browser.IsMobileDevice, // request bir mobil cihazdan mı geldi
                request.Browser.Version, //kullanıcının tarayıcısı
                request.UserLanguages, //tarayıcı dili
                request.UrlReferrer //yönlendiren sayfayı bulmaya yarayan urlreffer bilgisi
            });

            return result;


        }

    }
}