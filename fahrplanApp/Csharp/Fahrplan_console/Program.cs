using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Xml.Linq;
using System.Linq;
using System.Xml;

namespace Fahrplan_console
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Variable Declaration */
            string sInput = "empty";
            string sResponseFromServer;
            string sLinzAgURL = "http://www.linzag.at/static/";

            StringBuilder strBuilder = new StringBuilder();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.OmitXmlDeclaration = true;
            xmlSettings.Indent = true;

            XmlWriter xmlWriter = XmlWriter.Create(strBuilder, xmlSettings);
            //Console.Write("Fahrplan Console: \n");
            //Console.Write("1. Abfahrtszeiten\n");
            //Console.Write("2. Route\n");
            //Console.Write("Aufgabe wählen: ");
            //Input = Console.ReadLine();
            while (true)
            {
                Console.Clear();
                switch (sInput)
                {
                    case "1":
                        sInput = "empty";
                        break;

                    case "2":
                        sInput = "empty";
                        break;

                    case "3":
                        //WebRequest request = WebRequest.Create(sLinzAgURL + "XML_TRIP_REQUEST2?");
                        //WebResponse response = request.GetResponse();

                        //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                        //Stream DataStream = response.GetResponseStream();
                        //StreamReader Reader = new StreamReader(DataStream);
                        //sResponseFromServer = Reader.ReadToEnd();
                        //Console.WriteLine(sResponseFromServer);

                        //Reader.Close();
                        //response.Close();

                        XDocument xDoc = new XDocument();
                        xDoc = XDocument.Load(sLinzAgURL + "XML_TRIP_REQUEST2?");

                        xDoc.WriteTo(xmlWriter);

                        Console.WriteLine(strBuilder.ToString());
                        sInput = "empty";
                        Console.Read();
                        break;

                    default:
                        if(sInput == "")
                        {
                            break;
                        }
                        Console.Write("Fahrplan Console: \n");
                        Console.Write("1. Abfahrtszeiten\n");
                        Console.Write("2. Route\n");
                        Console.Write("Aufgabe wählen: ");
                        sInput = Console.ReadLine();

                        break;
                }

                if (sInput == "")
                    break;
            }

            Console.Write("Programm wird beendet. (Press Return !)");
            Console.Read();
        }
    }
}
