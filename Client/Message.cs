using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    [Serializable]
    public class Message
    {
        public string Name { get; set; } = default;
        public string Text { get; set; } = default;
        public DateTime DateTime { get; set; } = default;

        public Message()
        {
            this.Name = default;
            this.Text = default;
            this.DateTime = default;
        }
        public Message(string name, string text)
        {
            this.Name = name;
            this.Text = text;
            this.DateTime = DateTime.UtcNow;
        }
    }
}
