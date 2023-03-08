using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.XPath;

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
            var nazwy = new List<string>(); 
            var podmiot_kremy = new Dictionary<string, int>();
            var podmiot_tab = new Dictionary<string, int>();
            foreach (XmlNode d in drugs)
            {
                postac = d.Attributes.GetNamedItem("postac").Value;
                podmiot = d.Attributes.GetNamedItem("podmiotOdpowiedzialny").Value;
                sc = d.Attributes.GetNamedItem("nazwaPowszechnieStosowana").Value;
                if (postac == "Krem" && sc == "Mometasoni furoas") count++;
                if (!nazwy.Contains(sc)) nazwy.Add(sc);
                else count_diff++;
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
            Console.WriteLine("Liczba preparatów leczniczych o takiej samie nazwie powszechnej, pod różnymi postaciami: {0}", count_diff);
            var max_kremy = podmiot_kremy.Values.Max();
            var max_tab = podmiot_tab.Values.Max();
            foreach (KeyValuePair<string, int> i in podmiot_kremy)
            {
                if (i.Value == max_kremy) Console.WriteLine("{0}", i.Key);
            }
            foreach (KeyValuePair<string, int> i in podmiot_tab)
            {
                if (i.Value == max_tab) Console.WriteLine("{0}", i.Key);
            }
        }
    }

    internal class XMLReadWithSAXApproach
    {
        internal static void Read(string filepath)
        {
            // konfiguracja początkowa dla XmlReadera
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreProcessingInstructions = true;
            settings.IgnoreWhitespace = true;
            // odczyt zawartości dokumentu
            XmlReader reader = XmlReader.Create(filepath, settings);
            // zmienne pomocnicze
            int count = 0;
            int count_diff = 0;
            string postac = "";
            string sc = "";
            string podmiot = "";
            var nazwy = new List<string>();
            var podmiot_kremy = new Dictionary<string, int>();
            var podmiot_tab = new Dictionary<string, int>();
            reader.MoveToContent();
            // analiza każdego z węzłów dokumentu
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "produktLeczniczy")
                {
                    postac = reader.GetAttribute("postac");
                    podmiot = reader.GetAttribute("podmiotOdpowiedzialny");
                    sc = reader.GetAttribute("nazwaPowszechnieStosowana");
                    if (postac == "Krem" && sc == "Mometasoni furoas") count++;
                    if (!nazwy.Contains(sc)) nazwy.Add(sc);
                    else count_diff++;
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
            }
            Console.WriteLine("Liczba produktów leczniczych w postaci kremu, których jedyną substancją czynną jest Mometasoni furoas {0}", count);
            Console.WriteLine("Liczba preparatów leczniczych o takiej samie nazwie powszechnej, pod różnymi postaciami: {0}", count_diff);
            var max_kremy = podmiot_kremy.Values.Max();
            var max_tab = podmiot_tab.Values.Max();
            foreach (KeyValuePair<string, int> i in podmiot_kremy)
            {
                if (i.Value == max_kremy) Console.WriteLine("{0}", i.Key);
            }
            foreach (KeyValuePair<string, int> i in podmiot_tab)
            {
                if (i.Value == max_tab) Console.WriteLine("{0}", i.Key);
            }
        }
    }

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
            var nazwy = new List<string>();
            var podmiot_kremy = new Dictionary<string, int>();
            var podmiot_tab = new Dictionary<string, int>();
            Console.WriteLine("Liczba produktów leczniczych w postaci kremu, których jedyną substancją czynną jest Mometasoni furoas {0}", count);
            XPathNodeIterator x = navigator.Select("//@nazwaPowszechnieStosowana");
            while (x.MoveNext()) {
                if (!nazwy.Contains(x.Current.Value)) nazwy.Add(x.Current.Value);
                else count_diff++;
            }
            Console.WriteLine("Liczba preparatów leczniczych o takiej samie nazwie powszechnej, pod różnymi postaciami: {0}", count_diff);
            XPathNodeIterator y = navigator.Select("//produktLeczniczy");
        }
    }

    internal class Program
    {
        internal static void Main(string[] args)
        {
            string xmlpath = Path.Combine("Assets", "data.xml");
            // odczyt danych z wykorzystaniem DOM
            /*Console.WriteLine("XML loaded by DOM Approach");
            XMLReadWithDOMApproach.Read(xmlpath);*/
            
            // odczyt danych z wykorzystaniem SAX
            /*Console.WriteLine("XML loaded by SAX Approach");
            XMLReadWithSAXApproach.Read(xmlpath);*/
            // odczyt danych z wykorzystaniem XPath i DOM
            Console.WriteLine("XML loaded with XPath");
            XMLReadWithXLSTDOM.Read(xmlpath);
            Console.ReadLine();
        }
    }
}

