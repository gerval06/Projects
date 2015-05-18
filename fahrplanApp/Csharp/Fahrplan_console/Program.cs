/* Fahrplan Console Application
 * 
 * This is a application to realize access to to public transportation system of the city of Linz Austria.
 * 
 * Author: Gerhard Valcl,Bsc
 * 
 * v0.1: Implemented menu and xml output to console
 * v0.2: Implemented first HTTP request for the departure time at the WIFI Linz AG station
**/

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
        private static string sLinzAgURL = "http://www.linzag.at/static/";

        static void Main(string[] args)
        {
            /* Variable Declaration */
            string sInput = "empty";
            Int64 iSessionID = 0;

            XDocument xDoc = new XDocument();
            /* Create objects for xml output to Console */ 
            StringBuilder strBuilder = new StringBuilder();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.OmitXmlDeclaration = true;
            xmlSettings.Indent = true;

            XmlWriter xmlWriter = XmlWriter.Create(strBuilder, xmlSettings);

            //xDoc.WriteTo(xmlWriter);
            //Console.WriteLine(strBuilder.ToString());

            while (true)
            {
                Console.Clear();
                switch (sInput)
                {
                    case "1":
                        // WIFI%20Linz%20AG
                        xDoc = InitSession("XML_DM_REQUEST?", 0, "&locationServerActive=1&type_dm=any&name_dm=WIFI%20Linz%20AG&limit=5");
                        iSessionID = (Int64)xDoc.Root.Attribute("sessionID");
                        xDoc = InitSession("XML_DM_REQUEST?",iSessionID, "&requestID=1&dmLineSelectionAll=1");
                        xDoc.WriteTo(xmlWriter);
                        Console.WriteLine(strBuilder.ToString());
                        sInput = "empty";
                        Console.ReadLine();
                        break;

                    case "2":
                        sInput = "empty";
                        break;

                    case "3":
                        Console.Clear();
                        Console.Write("SessionID = " + iSessionID);
                        sInput = "empty";
                        Console.ReadLine();
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
            Console.ReadLine();
        }

        /* Initialize Session */
        public static XDocument InitSession(string sRequestTyp, Int64 SessionID, string sSessionArgs="")
        {
            Int64 iSessionID = -1;
            XDocument xDoc = new XDocument();
            xDoc = XDocument.Load(sLinzAgURL + sRequestTyp +"sessionID="+ SessionID + sSessionArgs);

            iSessionID = (Int64)xDoc.Root.Attribute("sessionID");

            return xDoc;
        }
    }
}
