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
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public Message()
        {
            this.Name = "Server";
            this.Text = "Server is running...";
            this.DateTime = DateTime.UtcNow;
        }

        public Message(string name, string text)
        {
            this.Name = name;
            this.Text = text;
            this.DateTime = DateTime.UtcNow;
        }

        public void Show()
        {
            Console.WriteLine($"{this.Name}: {this.Text}\n{this.DateTime}");
        }
    }

}
