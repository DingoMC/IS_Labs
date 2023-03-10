using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using System.Xml;

namespace IS_Lab1_XML
{
    internal class XMLReadWithXLSTDOM
    {
        internal static void Read(string filepath)
        {
            XPathDocument document = new XPathDocument(filepath);
            XPathNavigator navigator = document.CreateNavigator();
            XmlNamespaceManager manager = new XmlNamespaceManager(navigator.NameTable);
            manager.AddNamespace("x", "http://rejestrymedyczne.ezdrowie.gov.pl/rpl/eksport-danych-v1.0");
            XPathExpression query = navigator.Compile("/x:produktyLecznicze/x:produktLeczniczy[@postac='Krem' and @nazwaPowszechnieStosowana='Mometasoni furoas']");
            query.SetContext(manager);
            int count = navigator.Select(query).Count;
            int count_diff = 0;
            var nazwa_postac = new Dictionary<string, HashSet<string>>();
            var podmiot_kremy = new Dictionary<string, int>();
            var podmiot_tab = new Dictionary<string, int>();
            Console.WriteLine("Liczba produktów leczniczych w postaci kremu, których jedyną substancją czynną jest Mometasoni furoas {0}", count);
            XPathNodeIterator x = navigator.Select("//@nazwaPowszechnieStosowana");
            while (x.MoveNext())
            {
                var nazwa = x.Current.Value;
                if (!nazwa_postac.ContainsKey(nazwa)) nazwa_postac.Add(nazwa, new HashSet<string>());
                //nazwa_postac[nazwa].Add(postac);
            }
            Console.WriteLine("Liczba preparatów leczniczych o takiej samie nazwie powszechnej, pod różnymi postaciami: {0}", count_diff);
            navigator.MoveToRoot();
            navigator.MoveToFirstChild();   // produktyLecznicze
            navigator.MoveToFirstChild();   // produktLeczniczy
            do
            {
                if (navigator.NodeType == XPathNodeType.Element)
                {
                    navigator.MoveToFirstAttribute();
                    string postac = "";
                    string podmiot = "";
                    do
                    {
                        navigator.MoveToNextAttribute();
                        if (navigator.Name == "postac") postac = navigator.Value;
                        if (navigator.Name == "podmiotOdpowiedzialny") podmiot = navigator.Value;
                    } while (navigator.Name != "podmiotOdpowiedzialny");
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
                    navigator.MoveToParent();
                }

            } while (navigator.MoveToNext());
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
