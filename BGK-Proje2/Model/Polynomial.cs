using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGK_Proje2.Model
{
    public class Polynomial
    {
        bool checkPolynomialResult = false;
        string polynomial = "";
        string[] tempPol;      
        public void solve()
        {
            //polinom girdisi alınır ve kontrolü yapılır. Girilen polinom uygun değilse tekrar istenir.
            while (!checkPolynomial())
            {
                Console.Write("İndirgenemez polinomu giriniz: ");
                tempPol = Console.ReadLine().Replace(" ", "").Replace("^", "").Replace("X", "x").Trim(Global.separators).Split(Global.separators);
                setPolynomial();
            }
            writeMessage();
            Global.gMap = writeScreen(new string[(int)Math.Pow(2, Global.orderOfEquation)]);
        }

        /// <summary>
        /// Girilen polinomun indirgenemez polinom olup olmadığı kontrol edilir.
        /// </summary>
        /// <returns>polinomun uygun olup olmadığı döner</returns>
        bool checkPolynomial()
        {
            if (!string.IsNullOrEmpty(polynomial))
            {
                if(polynomial.Contains("x4") && Global.orderOfEquation == 4)
                {
                    foreach (var item in polynomial.Split(Global.separators))
                    {
                        if (item.All(char.IsNumber))
                            return true;
                    }
                    Console.Write("Girilen polinom bir indirgenemez polinom değildir. ");
                }
                else
                {
                    Console.Write("Girilen polinom bir GF(2^4) indirgenemez polinom değildir. ");
                }
            }
            return false;
        }

        /// <summary>
        /// denklemler oluşturulur, ekrana yazılır ve giriş değerleri arraye doldurulur.
        /// </summary>
        /// <param name="gmap">giriş değerleri için boş array</param>
        /// <returns></returns>
        string[] writeScreen(string[] gmap)
        {
            string equation = "0", binary, pol1, pol2;
            //polinom iki parçaya ayrılır.
            pol1 = $"a^{Global.orderOfEquation}";
            pol2 = polynomial.Replace($"x{Global.orderOfEquation}", "").Replace("x", "a^").Trim(Global.separators);


            for (int i = 0; i < Math.Pow(2, Global.orderOfEquation); i++)
            {
                //denklemlerde ifadeler bir üst ifadeyle yer değiştirir.
                for (int m = Global.orderOfEquation; m >= 1; m--)
                {
                    if (m > 1)
                        equation = equation.Replace($"a^{m - 1}", $"a^{m}");
                    else
                        equation = equation.Replace($"{m}", $"a^{m}").Replace($"{m - 1}", $"{m}");
                }

                //Eğer denklem pol1 var ise pol1 kaldırılır denklemin sonuna pol2 eklenir.
                if (equation.Contains(pol1))
                {
                    equation = equation.Replace(pol1, "") + pol2;
                }

                string[] eqs = equation.Split(Global.separators);
                //denklemde aynı ifadeler iki veya katları kadar var ise onlar denklemden kaldırılır.
                for (int j = 0; j < eqs.Length; j++)
                {
                    for (int k = j + 1; k < eqs.Length; k++)
                    {
                        if (eqs[j] == eqs[k])
                        {
                            eqs[j] = eqs[k] = "";
                        }
                    }
                }

                //denklem son halini alır. ve binary ifade bulunur.
                if (i >= Global.orderOfEquation)
                {
                    equation = "";
                    foreach (var item in eqs)
                    {
                        if (item != "")
                        {
                            equation += $"{item}+";
                        }
                    }
                }
                binary = "";
                for (int l = Global.orderOfEquation - 1; l > 0; l--)
                {
                    binary += equation.Contains($"a^{l}") ? "1" : "0";
                    if (l == 1)
                        binary += equation.Contains($"+{l}") || equation.TrimEnd('+') == l.ToString() ? "1" : "0";
                }
                Console.WriteLine("a^{0}={1} ({2}-{3})", i, equation.TrimEnd('+').Replace("^1", ""), binary, Convert.ToInt32(binary, 2).ToString("X"));
                gmap[i] = Convert.ToInt32(binary, 2).ToString("X");
            }
            return gmap;
        }
        /// <summary>
        /// polinom algoritmanın çalışması için uygun hale getirilir. Derecesine göre büyükten küçüğe doğru sıralanır.
        /// </summary>
        private void setPolynomial()
        {
            polynomial = "";
            Global.orderOfEquation = 0;
            foreach (var item in tempPol)
            {
                if (item.Contains("x") && item.Length > 1)
                    Global.orderOfEquation = Global.orderOfEquation < Convert.ToInt32(item.Substring(item.IndexOf('x') + 1)) ? Convert.ToInt32(item.Substring(item.IndexOf('x') + 1)) : Global.orderOfEquation;

                polynomial += item == "x" ? "x1+" : $"{item}+";
            }

            polynomial = polynomial.TrimEnd(Global.separators);
            string[] tempExp = polynomial.Split(Global.separators);
            string temp = null;
            bool check = false;

            for (int i = 0; i < tempExp.Length; i++)
            {
                if ((i + 1) < (tempExp.Length))
                {
                    if (tempExp[i].All(char.IsNumber) && !check)
                    {
                        temp = tempExp[i];
                        tempExp[i] = tempExp[tempExp.Length - 1];
                        tempExp[tempExp.Length - 1] = temp;
                        check = true;
                        i = -1;
                    }
                    else if (!tempExp[i + 1].All(char.IsNumber))
                    {
                        int exp1 = Convert.ToInt32(tempExp[i].Substring(tempExp[i].IndexOf("x") + 1));
                        int exp2 = Convert.ToInt32(tempExp[i + 1].Substring(tempExp[i + 1].IndexOf("x") + 1));
                        if (exp2 > exp1)
                        {
                            temp = tempExp[i];
                            tempExp[i] = tempExp[i + 1];
                            tempExp[i + 1] = temp;
                            i = -1;
                        }
                    }
                }
            }
            polynomial = "";
            foreach (var item in tempExp)
            {
                polynomial += $"{item}+";
            }
            polynomial = polynomial.TrimEnd(Global.separators); 
        }
        private void writeMessage()
        {
            Console.WriteLine("Sonlu cismi oluşturan elemanlar listeleniyor...");
        }

    }
}
