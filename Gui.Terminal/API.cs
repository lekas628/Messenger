using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Text;

namespace Gui.Terminal
{
    public static class API
    {
        public static bool SendMessage(Message msg)
        {
            try
            {
                WebRequest req = WebRequest.Create("http://localhost:5000/api/chat");
                req.Method = "POST";
                string postData = JsonConvert.SerializeObject(msg);
                byte[] bytes = Encoding.UTF8.GetBytes(postData);
                req.ContentType = "application/json";
                req.ContentLength = bytes.Length;
                Stream reqStream = req.GetRequestStream();
                reqStream.Write(bytes);
                reqStream.Close();

                req.GetResponse();
                return true;
            }
            catch (System.Net.WebException)
            {
                return false;
            }
        }

        public static (bool, Message) GetMessage(int id)
        {
            try
            {
                WebRequest req = WebRequest.Create($"http://localhost:5000/api/chat/{id}");

                WebResponse resp = req.GetResponse();
                string smsg = new StreamReader(resp.GetResponseStream()).ReadToEnd();

                if (smsg == "Not found") return (true, null);
                Message msg = JsonConvert.DeserializeObject<Message>(smsg);
                return (true, msg);
            }
            catch (WebException wbEx)
            {
                if(wbEx.Response is null)
                    return (false, null);
                //if(wbEx.re)
                return (false, null);
            }
        }
    }
}
