using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    [Serializable]
    public class Message
    {
        public string name { get; set; } = default;
        public string text { get; set; } = default;
        public DateTime dateTime { get; set; } = default;

        public Message()
        {
            this.name = default;
            this.text = default;
            this.dateTime = default;
        }
        public Message(string name, string text)
        {
            this.name = name;
            this.text = text;
            this.dateTime = DateTime.UtcNow;
        }
    }
}
