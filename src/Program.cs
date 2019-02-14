using System;
using System.IO;
// Microsoft.Extensions.Configuration.Json
using Microsoft.Extensions.Configuration;

namespace IRSACA
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            string option = Configuration["Project:GenerateOption"];
            Console.WriteLine(option);
            if (option == "Form")
            {
                GeneratePdf109495(Configuration);
            }
            else
            {
                GenerateXml109495(Configuration);
            }
        }
        private static void GenerateXml109495(IConfiguration Configuration)
        {
            Generatexml x = new Generatexml();
            x.Genxml109495C(Configuration);
        }
        private static void GeneratePdf109495(IConfiguration Configuration)
        {
            Generatepdf p = new Generatepdf();
            p.Generatebasepdf(Configuration);
        }
    }
}
