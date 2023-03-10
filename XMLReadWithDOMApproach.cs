using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IS_Lab1_XML
{
    internal class XMLReadWithDOMApproach
    {
        internal static void Read(string filepath)
        {
            // odczyt zawartości dokumentu
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);
            string postac;
            string sc;
            string podmiot;
            int count = 0;
            int count_diff = 0;
            var drugs = doc.GetElementsByTagName("produktLeczniczy");
            var nazwa_postac = new Dictionary<string, HashSet<string>>();
            var podmiot_kremy = new Dictionary<string, int>();
            var podmiot_tab = new Dictionary<string, int>();
            foreach (XmlNode d in drugs)
            {
                postac = d.Attributes.GetNamedItem("postac").Value;
                podmiot = d.Attributes.GetNamedItem("podmiotOdpowiedzialny").Value;
                sc = d.Attributes.GetNamedItem("nazwaPowszechnieStosowana").Value;
                if (postac == "Krem" && sc == "Mometasoni furoas") count++;
                if (!nazwa_postac.ContainsKey(sc)) nazwa_postac.Add(sc, new HashSet<string>());
                nazwa_postac[sc].Add(postac);
                if (postac.Contains("Krem"))
                {
                    if (podmiot_kremy.ContainsKey(podmiot)) podmiot_kremy[podmiot]++;
                    else podmiot_kremy[podmiot] = 1;
                }
                if (postac.Contains("Tabletki"))
                {
                    if (podmiot_tab.ContainsKey(podmiot)) podmiot_tab[podmiot]++;
                    else podmiot_tab[podmiot] = 1;
                }
            }
            Console.WriteLine("Liczba produktów leczniczych w postaci kremu, których jedyną substancją czynną jest Mometasoni furoas {0}", count);
            foreach (KeyValuePair<string, HashSet<string>> i in nazwa_postac)
            {
                if (i.Value.Count > 1) count_diff++;
            }
            Console.WriteLine("Liczba preparatów leczniczych o takiej samie nazwie powszechnej, pod różnymi postaciami: {0}", count_diff);
            var max_kremy = podmiot_kremy.Values.Max();
            var max_tab = podmiot_tab.Values.Max();
            foreach (KeyValuePair<string, int> i in podmiot_kremy)
            {
                if (i.Value == max_kremy) Console.WriteLine("Podmiot produkujacy najwiecej kremow: {0}", i.Key);
            }
            foreach (KeyValuePair<string, int> i in podmiot_tab)
            {
                if (i.Value == max_tab) Console.WriteLine("Podmiot produkujacy najwiecej tabletek: {0}", i.Key);
            }
            Console.WriteLine("3 podmioty produkujace najwiecej kremow");
            var cnt = 0;
            foreach (var i in podmiot_kremy.OrderByDescending(key => key.Value))
            {
                Console.WriteLine("{0} => {1}", i.Key, i.Value);
                cnt++;
                if (cnt >= 3) break;
            }
        }
    }
}
