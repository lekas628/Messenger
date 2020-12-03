using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Server
{
    public class Program
    {
        public static MessagesClass message;
        public static AuthorizationClass authorizationClass;
        public static void Main(string[] args)
        {
            message = new MessagesClass();
            authorizationClass = new AuthorizationClass();
            //authorizationClass.Add(new DataPerson("tbgrf", "yhtrgef"));
            //authorizationClass.Add(new DataPerson("gui", "rxrrr"));
            //authorizationClass.LoadToFile();
            authorizationClass.LoadFromFile();
            authorizationClass.Show();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
