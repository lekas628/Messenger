using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    [Serializable]
    public class DataPerson
    {
        public string Login { get; set; } = default;
        public string Password { get; set; } = default;

        public DataPerson()
        {
            this.Login = default;
            this.Login = default;
        }
        public DataPerson(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }

    }
}
