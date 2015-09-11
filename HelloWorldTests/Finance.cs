using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml.Linq;


namespace HelloWorldTests
{
    public class Finance
    {
        //class Quote
        //{
        //    float AverageDailyVolume;
        //    float Change;
        //    float DaysLow;
        //    float DaysHigh;
        //    float YearLow;
        //    float YearHigh;
        //    string MarketCapitalization;
        //    float LastTradePriceOnly;
        //    float DaysRange;
        //    string Name;
        //    string Symbol;
        //    float Volume;
        //    string StockExchange;
        //}


        public static string GetQuote(string symbol)
        {
            string res = null;
            string url = "https://query.yahooapis.com/v1/public/yql";
            url += "?q=" + Uri.EscapeDataString(string.Format("select LastTradePriceOnly from yahoo.finance.quote where symbol in (\"{0}\")", symbol));
            url += "&env=" + Uri.EscapeDataString("store://datatables.org/alltableswithkeys");
            //var request = HttpWebRequest.Create(string.Format(@"http://download.finance.yahoo.com/d/quotes.csv?s=%40%5EDJI,GOOG", symbol));
            var request = HttpWebRequest.Create(url);
            request.ContentType = "application/xml";
            request.Method = "GET";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    XDocument doc = XDocument.Parse(content);
                    var price = (from result in doc.Root.Elements("results")
                                    //where quote.Element("firstName").Value.Contains("er")
                                    select result.Elements("quote").FirstOrDefault().Element("LastTradePriceOnly")).FirstOrDefault().Value;
                    //content = Xml
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        Console.Out.WriteLine("Response contained empty body...");
                    }
                    else
                    {
                        Console.Out.WriteLine("Response Body: \r\n {0}", content);
                        res = price.ToString();
                    }
                }
            }
            return res;
        }
    }
}