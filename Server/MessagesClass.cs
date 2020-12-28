using System;
using System.Collections.Generic;
using System.Linq;

namespace Server
{
    [Serializable]
    public class MessagesClass
    {
        public List<Message> messages = new List<Message>();

        public void Add(Message message)
        {
            message.DateTime = DateTime.UtcNow;
            messages.Add(message);
        }

        public void RemoveAt(int index)
        {
            if(index <= this.messages.Count && index >= 0)
            {
                this.messages.RemoveAt(index);
            }
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
