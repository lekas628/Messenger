using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace Gui.Terminal
{
    

    class Program
    {
        public static MessagesClass messagesClass = new MessagesClass();
        public static int currentMessagesPointer;

        //public static Dictionary<string, bool> usersOnline = new Dictionary<string, bool>(100);

        static void Main(string[] args)
        {
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
    }
}
