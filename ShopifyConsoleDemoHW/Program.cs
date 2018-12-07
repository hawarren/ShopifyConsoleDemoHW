namespace ShopifyConsoleDemoHW
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

using Newtonsoft.Json;


    class Program
    {
        static void Main(string[] args)
        {


            //sample code to get open orders within the last 30 days. Do not have Shopify account to test against.
            using (var webClient = new WebClient())
            {

            // Get date 30 days ago
            DateTime today = DateTime.Today;
            DateTime thirtyDays = today.AddDays(-30);
                string apiTemplate = "http://admin.orders.json?status=open&created_at_min={0}";
                //build the request for the Shopify API using the template and today's date.
                string apiQuery = string.Format(apiTemplate, thirtyDays.ToString("o"));

            //Get the JSON from shopify
            String rawJSON = webClient.DownloadString(apiQuery);
                // convert the JSON string to series of objects
                ShopifyAPIORders shopifyAPIORders = JsonConvert.DeserializeObject<ShopifyAPIORders>(rawJSON);
                //print out number of orders received
            Console.WriteLine("You have {0} orders in the last 30 days", shopifyAPIORders.orders.Count);


            }


        }
    }
}
