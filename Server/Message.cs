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
        public string Name { get; set; } = "No data";
        public string Text { get; set; } = "No data";
        public DateTime DateTime { get; set; } = default;
 
        public Message()
        {
            this.Name = "No data";
            this.Text = "No data";
            this.DateTime = DateTime.UtcNow;
        }
        public Message(string name, string text)
        {
            this.Name = name;
            this.Text = text;
            this.DateTime = DateTime.UtcNow;
        }

        public void show()
        {
            Console.WriteLine($"{this.Name}: {this.Text}\n{this.DateTime}");
        }
    }

}
