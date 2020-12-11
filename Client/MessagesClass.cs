using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Client
{
    [Serializable]
    public class MessagesClass
    {
        public List<Message> messages = new List<Message>();

        public void Add(Message message)
        {
            message.dateTime = DateTime.UtcNow;
            messages.Add(message);
        }

        public Message Get(int id)
        {
            return messages.ElementAt(id);
        }

        public int GetCountMessages()
        {
            return messages.Count;
        }

        public MessagesClass()
        {
            messages.Clear();
        }
    }
}
