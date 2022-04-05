using com.mirle.iibg3k0.ids.ohxc.App;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace IntelligentRealTimeDetectionSystem_OHxC_Core
{
    class Program
    {
        static CSApplication csApp = null;

        static void Main(string[] args)
        {
            string ohxc_id = args[0];
            string server_name = args[1];
            csApp = CSApplication.getInstance(ohxc_id, server_name);
            Console.WriteLine($"OHxC ID:{CSApplication.OhxC_ID},ServerName:{server_name}");
            csApp.ObserverChanged += CsApp_ObserverChanged;

            while (true)
            {
                Console.WriteLine("I'm IMS");
                Console.ReadLine();
                //string isSet = Console.ReadLine();
                //if (isSet == "Y")
                //{

                //    csApp.CheckService.EarthquakeHappendHandler(true);
                //}
                //else
                //{
                //    csApp.CheckService.EarthquakeHappendHandler(false);
                //}
            }
        }


        public static IConfigurationRoot BuildConfiguration(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        private static void CsApp_ObserverChanged(object sender, bool e)
        {
            if (e)
            {
                Console.WriteLine("As an observer!");
            }
            else
            {
                Console.WriteLine("Retired an observer!");
            }
        }
    }
}
