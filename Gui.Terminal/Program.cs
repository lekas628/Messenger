using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace Gui.Terminal
{
    

    class Program
    {
        public static MessagesClass messagesClass = new MessagesClass();
        public static int currentMessagesPointer;
        public static int updateLoopInterval = 1000;

        static void Main(string[] args)
        {
            LoadConfigFromFile();
            

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
                Settings interval = JsonConvert.DeserializeObject<Settings>(json);
                updateLoopInterval = interval.updateLoopInterval;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }

        public static void LoadMessages()
        {
            bool serverOnline = API.GetServerStatus();
            while (!serverOnline)
            {
                Thread.Sleep(10_000);
                serverOnline = API.GetServerStatus();
            }


            currentMessagesPointer = 0;

            int serverMessagesPointer = API.GetMessagesCount() - 1;
            while (currentMessagesPointer != serverMessagesPointer)
            {
                (Message msg, bool status) = API.GetMessage(currentMessagesPointer);
                messagesClass.Add(msg);
                currentMessagesPointer += 1;
            }
        }
    }
}
