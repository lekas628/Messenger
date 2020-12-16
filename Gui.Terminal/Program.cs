using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using Newtonsoft.Json;

namespace Gui.Terminal
{
    

    class Program
    {
        public static MessagesClass messagesClass = new MessagesClass();
        public static int currentMessagesPointer;
        public static int updateLoopInterval = 1000;

        //public static Dictionary<string, bool> usersOnline = new Dictionary<string, bool>(100);

        static void Main(string[] args)
        {
            LoadConfigFromFile();


            bool serverOnline =  API.GetServerStatus();
            while (!serverOnline)
            {
                Console.WriteLine("Server offline");
                Thread.Sleep(10_000);
                serverOnline = API.GetServerStatus();
            }


            currentMessagesPointer = 0;

            int serverMessagesPointer = API.GetMessagesCount() - 1;
            while (currentMessagesPointer != serverMessagesPointer)
            {
                (Message msg, bool status) = API.GetMessage(currentMessagesPointer);
                //if(msg != null && msg.Text[0] == '#')
                //{

                //}
                messagesClass.Add(msg);
                currentMessagesPointer += 1;
            }
            

            ChatApp.run();
        }

        public static void LoadConfigFromFile()
        {
            string json;
            try
            {
                using (StreamReader sr = new StreamReader("updateLoopInterval.json", System.Text.Encoding.Default))
                {
                    json = sr.ReadToEnd();
                }
                Interval interval = JsonConvert.DeserializeObject<Interval>(json);
                updateLoopInterval = interval.updateLoopInterval;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
    }
}
