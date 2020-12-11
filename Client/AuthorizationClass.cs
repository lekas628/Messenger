using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    [Serializable]
    public class DataPerson
    {
        public string login { get; set; } = default;
        public string password { get; set; } = default;

        public DataPerson()
        {
            this.login = default;//hrhr
            this.login = default;
        }
        public DataPerson(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

    }
}
