using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BurcApi.Controllers
{
    public class Burclar
    {
        public string BurcAdi { get; set; }
        public string Yorum { get; set; }
    }
    public class BurcApiController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public List<Burclar> AllBurcs()
        {
            string url = "";
            List<Burclar> burcList= new List<Burclar>();
            string[] burcDizi = { "koc", "boga", "ikizler", "yengec", "aslan", "basak", "terazi", "akrep", "yay", "oglak", "kova", "balik" };
            string[] burcIsimler = { "KOÇ", "BOĞA", "İKİZLER", "YENGEÇ", "ASLAN", "BAŞAK", "TERAZİ", "AKREP", "YAY", "OĞLAK", "KOVA", "BALIK" };
            
            int i = 0;
            foreach (string item in burcDizi)
            {
                url = "http://www.milliyet.com.tr/ashx/Astroloji.ashx?hCase=BurcDetayPre&tname=burcdetay_"+item+"_1";

                WebRequest req = HttpWebRequest.Create(url);
                WebResponse res;
                try
                {
                    res = req.GetResponse();
                    StreamReader data = new StreamReader(res.GetResponseStream());
                    string icerik = data.ReadToEnd();
                    int start = icerik.IndexOf("row\">")+5;
                    int end = icerik.IndexOf("</div")-1;
                    string yorum = icerik.Substring(start, end - start);
                    Burclar brc = new Burclar();
                    brc.BurcAdi = burcIsimler[i];
                    brc.Yorum = yorum;
                    burcList.Add(brc);

                }
                catch (Exception ex)
                {
                    break;
                }
                i++;
            }
            return burcList;
        }
    }
}
