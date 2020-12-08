using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGK_Proje2.Model
{
    public class Map
    {
        bool checkMapFuncResult = false;
        string mapFunction = "";
        public void solve()
        {
            Console.WriteLine();
            //map fonksiyon girdisi alınır ve kontrolü yapılır. Girilen map fonksiyonu uygun değilse tekrar istenir.
            while (!checkMapFuncResult)
            {
                Console.Write("Haritalamayı giriniz: ");
                setMapFunction();
                //map fonksiyonunu kontrolü yapılır.
                checkMapFuncResult = checkMapFunc();
            }
            //Haritalama yapılması için map fonksiyonu, giriş map arrayi(gmap) ve boş çıkış map arrayi(cmap) map fonksiyonuna gönderilir.
            //Sonuç olarak map arrayi döner.
            Global.cMap = map(new string[(int)Math.Pow(2, Global.orderOfEquation)]);

            //Haritalamanın sonucu ekrana yazılır.
            writeMap();
           
        }
        /// <summary>
        ///  //Girilen map fonksiyonu algoritma için uygun formata getirilir.
        /// </summary>
        private void setMapFunction()
        {
            mapFunction = "";
            mapFunction = Console.ReadLine().Replace(" ", "").Replace("^", "").Replace("X", "x");
            if (mapFunction.EndsWith("->x"))
                mapFunction += "1";
        }

        /// <summary>
        /// Girilen map fonksiyonunun doğru olup olmadığı kontrol edilir.
        /// </summary>
        /// <returns>map fonksiyonunun uygun olup olmadığı döner</returns>
        bool checkMapFunc()
        {
            if (!string.IsNullOrEmpty(mapFunction))
            {
                if (mapFunction.Contains("->"))
                {
                    mapFunction = mapFunction.Substring(mapFunction.IndexOf(">") + 1);
                    foreach (var item in mapFunction.ToCharArray())
                    {
                        if (char.IsDigit(item))
                            return true;
                    }
                    Console.Write("Girilen bir haritalama fonksiyonu değildir. ");
                }
                else
                {
                    Console.Write("Girilen bir haritalama fonksiyonu değildir. Başına (x->) ekleyiniz. ");
                }
            }
           
           
            return false;
        }

        /// <summary>
        /// harita fonksiyonundaki sayı bulunur ve önceden bulunmuş girş değerlerine göre mod alma işlemi yapılarak çıkış değerleri bulunur.
        /// </summary>
        /// <param name="cmap">çıkış değerleri için boş array</param>
        /// <returns></returns>
        string[] map(string[] cmap)
        {
            int a;
            int b = (int)Math.Pow(2, Global.orderOfEquation) - 1;
            var mod = 0;
            int val = Convert.ToInt32(mapFunction.Substring(mapFunction.IndexOf('x') + 1));
            cmap[mod] = "0";
            for (int i = 1; i < Global.gMap.Length; i++)
            {
                a = val * i;
                mod = (Math.Abs(a * b) + a) % b;
                var aa = Convert.ToInt32(Global.gMap[i], 16);
                cmap[Convert.ToInt32(Global.gMap[i], 16)] = Global.gMap[mod];
            }
            return cmap;
        }
        /// <summary>
        /// giriş ve çıkış değerleri ekrana yazılır.
        /// </summary>
        /// <param name="cmap"></param>
        void writeMap()
        {
            Console.Write("\nGiriş");
            for (int i = 0; i < Math.Pow(2, Global.orderOfEquation); i++)
            {
                if (!string.IsNullOrEmpty(Global.cMap[i]))
                {
                    if (Global.cMap[i].ToCharArray().Length > 1)
                        Console.Write(string.Format("|{0,-2}", $"{Convert.ToInt32(Convert.ToString(i, 2), 2).ToString("X")}"));
                    else
                        Console.Write(string.Format("|{0,0}", $"{Convert.ToInt32(Convert.ToString(i, 2), 2).ToString("X")}"));
                }
                else
                {
                    Console.Write(string.Format("|{0,0}", $"{Convert.ToInt32(Convert.ToString(i, 2), 2).ToString("X")}"));
                }                    
            }
            Console.Write("|\nÇıkış");
            for (int i = 0; i < Math.Pow(2, Global.orderOfEquation); i++)
            {
                if (!string.IsNullOrEmpty(Global.cMap[i]))
                {
                    if (Convert.ToInt32(Convert.ToString(i, 2), 2).ToString("X").ToCharArray().Length > 1)
                        Console.Write(string.Format("|{0,-2}", $"{Global.cMap[i]}"));
                    else
                        Console.Write(string.Format("|{0,0}", $"{Global.cMap[i]}"));
                }
                else
                {
                    if (Convert.ToInt32(Convert.ToString(i, 2), 2).ToString("X").ToCharArray().Length > 1)
                        Console.Write(string.Format("{0,-3}", "|"));
                    else
                        Console.Write(string.Format("{0,-2}", "|"));
                 
                }
            }
            Console.Write("|\n");
           

        }
    }
}
