using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

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
            authorizationClass.LoadFromFile();
            authorizationClass.Show();
            LoadMessageFromFile();
            SaveMessageToFile();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void LoadMessageFromFile()
        {
            string json;
            try
            {
                using (StreamReader sr = new StreamReader("message_history.json", System.Text.Encoding.Default))
                {
                    json = sr.ReadToEnd();
                }
                message = JsonConvert.DeserializeObject<MessagesClass>(json);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }

        public static async void SaveMessageToFile()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        string data = JsonConvert.SerializeObject(message);
                        using (StreamWriter stream = new StreamWriter("message_history.json", false, System.Text.Encoding.Default))
                        {
                            stream.WriteLine(data);
                        }
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                    }
                    Thread.Sleep(5000);
                }
            });
        }
    }
}
