using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
    [Serializable]
    public class Message
    {
        public string username { get; set; }
        public string text { get; set; }
        public DateTime timestamp { get; set; }
        public Message()
        {
            this.username = "Server";
            this.text = "Server is running...";
            this.timestamp = DateTime.UtcNow;
        }

        public Message(string username, string text)
        {
            this.username = username;
            this.text = text;
            this.timestamp = DateTime.UtcNow;
        }
    }

    [Serializable]
    public class MessagesClass
    {
        public List<Message> messages = new List<Message>();
        public MessagesClass()
        {
            messages.Clear();
            Message msg = new Message();
            messages.Add(msg);
        }
        public void Add(Message msg)
        {
            msg.timestamp = DateTime.UtcNow;
            messages.Add(msg);
            Console.WriteLine(messages.Count);
        }
        public void Add(string username, string text)
        {
            Message msg = new Message(username, text);
            messages.Add(msg);
            Console.WriteLine(messages.Count);
        }
        public Message Get(int id)
        {
            if (id < messages.Count)
                return messages.ElementAt(id);
            else
                return null;
        }
        public int GetCountMessages()
        {
            return messages.Count;
        }
    }
}
