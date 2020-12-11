using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
    [Serializable]
    public class DataPerson
    {
        public string login { get; set; } = "No data";
        public string password { get; set; } = "No data";

        public DataPerson()
        {
            this.login = "No data";
            this.password = "No data";
        }

        public DataPerson(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

    }
    [Serializable]
    public class AuthorizationClass
    {
        private readonly string filename = "data_sessions.json";
        public List<DataPerson> dataPeople;

        public AuthorizationClass()
        {
            this.dataPeople = new List<DataPerson>();
        }
        public void Add(DataPerson dataPerson)
        {
            this.dataPeople.Add(dataPerson);
        }

        public void Show()
        {
            foreach (var item in dataPeople)
            {
                Console.WriteLine($"{item.login}: {item.password}");
            }
        }

        public void SaveToFile()
        {
            try
            {
                string data = JsonConvert.SerializeObject(Program.authorizationClass);
                using (StreamWriter stream = new StreamWriter(filename, false, System.Text.Encoding.Default))
                {
                    stream.WriteLine(data);
                }
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
        public void LoadFromFile()
        {
            string json;
            try
            {
                using (StreamReader sr = new StreamReader(filename, System.Text.Encoding.Default))
                {
                    json = sr.ReadToEnd();
                }
                Program.authorizationClass = JsonConvert.DeserializeObject<AuthorizationClass>(json);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }

        public bool CheckLoginAndPassword(DataPerson dataPerson)
        {
            foreach(DataPerson item in dataPeople)
            {
                if (item.login == dataPerson.login)
                {
                    if (item.password == dataPerson.password)
                    {
                        Console.WriteLine($"{dataPerson.login} is online");
                        return true;
                    }
                }
            }
            return false;
        }

        public bool Registration(DataPerson dataPerson)
        {
            foreach (DataPerson item in dataPeople)
            {
                if (item.login == dataPerson.login)
                {
                    return false;
                }
            }
            Program.authorizationClass.Add(dataPerson);
            return true;
        }
    }
}
