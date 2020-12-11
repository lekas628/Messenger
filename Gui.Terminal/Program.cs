using System;
using System.Collections.Generic;
using System.Net;

namespace Gui.Terminal
{
    

    class Program
    {
        public static MessagesClass messagesClass = new MessagesClass();
        public static int currentMessagesPointer;

        static void Main(string[] args)
        {
            currentMessagesPointer = 0;

            int serverMessagesPointer = API.GetMessagesCount() - 1;
            while(currentMessagesPointer != serverMessagesPointer)
            {
                (Message msg, bool status) = API.GetMessage(currentMessagesPointer);
                messagesClass.Add(msg);
                currentMessagesPointer += 1;
            }

            ChatApp.run();
        }
    }
}
