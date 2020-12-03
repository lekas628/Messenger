using Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server
{
    [Serializable]
    public class Message
    {
        public string name { get; set; }
        public string text { get; set; }
        public DateTime dateTime { get; set; }
        public Message()
        {
            this.name = "Server";
            this.text = "Server is running...";
            this.dateTime = DateTime.UtcNow;
        }

        public Message(string name, string text)
        {
            this.name = name;
            this.text = text;
            this.dateTime = DateTime.UtcNow;
        }

        public void show()
        {
            Console.WriteLine($"{this.name}: {this.text}\n{this.dateTime}");
        }
    }

}
