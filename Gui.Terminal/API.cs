using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Text;

namespace Gui.Terminal
{
    public static class API
    {
        static string mainAPILink = "http://localhost:5000/api";
        
        static string chatLink = mainAPILink + "/chat";

        static string infoLink = mainAPILink + "/info";
        static string messageCountLink = infoLink + "/MessageCount";
        static string statusLink = infoLink + "/status";

        static string authorizationLink = mainAPILink + "/Authorization";
        static string registrationLink = mainAPILink + "/Registration";

        public static bool SendMessage(Message msg)
        {
            string postData = JsonConvert.SerializeObject(msg);
            byte[] bytes = Encoding.UTF8.GetBytes(postData);
            try
            {
                WebRequest req = WebRequest.Create(chatLink);
                req.Method = "POST";
                req.ContentType = "application/json";
                req.ContentLength = bytes.Length;

                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bytes);
                    req.GetResponse();
                    return true;
                }
            }
            catch (WebException)
            {
                return false;
            }
        }

        public static (Message, bool) GetMessage(int id)
        {
            string smsg;
            try
            {
                WebRequest req = WebRequest.Create($"{chatLink}/{id}");
                using (WebResponse resp = req.GetResponse())
                {
                    using (Stream stream = resp.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            smsg = reader.ReadToEnd();
                            if (smsg == "Not found") return (null, true);
                            
                            Message msg = JsonConvert.DeserializeObject<Message>(smsg);
                            return (msg, true);
                        }
                    }
                }
            }
            catch (WebException)
            {
                return (null, false);
            }
        }

        public static int GetMessagesCount()
        {
            int messageCount;

            try
            {
                WebRequest req = WebRequest.Create(messageCountLink);

                using (WebResponse resp = req.GetResponse())
                {
                    using (Stream stream = resp.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            messageCount = Int32.Parse(reader.ReadLine());
                        }
                    }
                }

                return messageCount;
            }
            catch (WebException)
            {
                return -1;
            }
        }

        public static bool GetServerStatus()
        {
            bool status = false;
            try
            {
                WebRequest req = WebRequest.Create(statusLink);

                using (WebResponse resp = req.GetResponse())
                {
                    using (Stream stream = resp.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            status = bool.Parse(reader.ReadLine());
                        }
                    }
                }

                return status;
            }
            catch (WebException)
            {
                return false;
            }
        }

        public static bool LoginServer(string username, string password)
        {
            DataPerson dataPerson = new DataPerson(username, password);
            string json = System.Text.Json.JsonSerializer.Serialize(dataPerson);

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(authorizationLink);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }
            
            HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                bool result = bool.Parse(streamReader.ReadToEnd());
                return result;
            }
        }

        public static bool RegistrationServer(string username, string password)
        {
            DataPerson dataPerson = new DataPerson(username, password);
            string json = System.Text.Json.JsonSerializer.Serialize(dataPerson);

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(registrationLink);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }
            HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                bool result = bool.Parse(streamReader.ReadToEnd());
                return result;
            }
        }
    }
}
