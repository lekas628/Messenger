using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Gui.Terminal
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
  
    [Serializable]
    public class MessagesClass
    {
        public List<Message> messages = new List<Message>();

        public void Add(Message message)
        {
            message.DateTime = DateTime.UtcNow;
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
    
    [Serializable]
    public class DataPerson
    {
        public string login { get; set; }
        public string password { get; set; }

        public DataPerson()
        {
            this.login = default;
            this.login = default;
        }
        public DataPerson(string login, string password)
        {
            this.login = login;
            this.password = password;
        }
    }

    [Serializable]
    class Interval
    {
        public int updateLoopInterval;
    }


}
