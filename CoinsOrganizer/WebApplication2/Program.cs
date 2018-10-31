using System;
using CoinsOrganizer.MarketService;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CoinsOrganizer
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Chilkat.Rest rest = new Chilkat.Rest();
            Console.WriteLine(rest.Version);

            BuildWebHost(args).Run();

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
