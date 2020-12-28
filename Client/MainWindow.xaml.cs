using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;


namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Ip;
        public int Host;
        public DataPerson dataPerson;
        public MessagesClass messagesClass;
        public DataUpdatePeriod dataUpdatePeriod;
        Thread UpdateMessgethread;
        public MainWindow(DataPerson dataPerson, string Ip, int Host)
        {
            try
            {
                string json;
                string Puth = AppDomain.CurrentDomain.BaseDirectory;
                int start = Puth.IndexOf("bin");
                Puth = Puth.Remove(start, Puth.Length - start);
                using (StreamReader sr = new StreamReader(Puth + @"SizeMainForm.json", System.Text.Encoding.Default))
                {
                    json = sr.ReadToEnd();
                }
                string json1;
                using (StreamReader sr = new StreamReader(Puth + @"DataUpdatePeriod.json", System.Text.Encoding.Default))
                {
                    json1 = sr.ReadToEnd();
                }
                this.Ip = Ip;
                this.Host = Host;
                dataUpdatePeriod = JsonConvert.DeserializeObject<DataUpdatePeriod>(json1);
                SizeMainForm sizeMainForm = JsonConvert.DeserializeObject<SizeMainForm>(json);
                this.Width = sizeMainForm.Width;
                this.Height = sizeMainForm.Height;
                this.dataPerson = dataPerson;
                this.messagesClass = new MessagesClass();
                GetAllMessageFromServer();
                InitializeComponent();
                ShowMessageToPanel();
                SendMessage(new Message('#' + dataPerson.Login, " is online"));
                UpdateMessgethread = new Thread(UpdateMessage);
                UpdateMessgethread.Start();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

         public int GetCountMessages()
        {
            try
            {
                WebRequest req = WebRequest.Create($"http://{this.Ip}:{this.Host}/api/Info/MessageCount");
                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                int count = int.Parse(sr.ReadToEnd());
                sr.Close();
                return count;
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
                return 0;
            }
        }
        private void GetAllMessageFromServer()
        {
            try
            {
                for (int i = 0; i < GetCountMessages(); i++)
                {
                    WebRequest request = WebRequest.Create($"http://{this.Ip}:{this.Host}/api/chat/" + i.ToString());
                    WebResponse response = request.GetResponse();
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            this.messagesClass.Add(System.Text.Json.JsonSerializer.Deserialize<Message>(reader.ReadToEnd()));
                        }
                    }
                    response.Close();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private void ShowMessageToPanel()
        {
            try
            {
                int count = GetCountMessages();
                for (int i = 0; i < count; i++)
                {
                    PrintNewMessageToPanel(this.messagesClass.Get(i));
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private void PrintNewMessageToPanel(Message message)
        {
            try
            {
                Label label = new Label();
                label.Background = Brushes.Gray;
                if (message.Name[0] == '#')
                {
                    label.HorizontalAlignment = HorizontalAlignment.Center;
                    label.Background = new SolidColorBrush(Color.FromRgb(225, 242, 251));
                }
                else 
                {
                    if (message.Name == this.dataPerson.Login)
                    {
                        label.HorizontalAlignment = HorizontalAlignment.Right;
                        label.Background = new SolidColorBrush(Color.FromRgb(220, 248, 198));
                    }
                    else
                    {
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.Background = new SolidColorBrush(Color.FromRgb(255, 225, 255));
                    }
                    label.Height = 45;
                }
                label.Margin = new Thickness(10,10,10,10);
                label.Content = message.Name + ": " + message.Text; 
                if(label.Content.ToString().Length < 17)
                {
                    int size = 17 - label.Content.ToString().Length; 
                    label.Width = (label.Content.ToString().Length + size) * 7;
                }
                else label.Width = label.Content.ToString().Length *7;
                label.Content += "\n" + message.DateTime.ToUniversalTime(); 
                this.ChatPanel.Children.Add(label);
                this.scrollViewer.ScrollToEnd();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private void GetMessageFromServer(int count)
        {
            try
            {
                WebRequest request = WebRequest.Create($"http://{this.Ip}:{this.Host}/api/chat/" + count.ToString());
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        Message message = System.Text.Json.JsonSerializer.Deserialize<Message>(reader.ReadToEnd());
                        this.messagesClass.Add(message);
                        PrintNewMessageToPanel(message);
                    }
                }
                response.Close();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private  void UpdateMessage()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(dataUpdatePeriod.dataUpdate);
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        if (this.messagesClass.GetCountMessages() < GetCountMessages())
                        {
                            GetMessageFromServer(this.messagesClass.GetCountMessages());
                        }
                    });
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        void SendMessage(Message message)
        {
            try
            {
                WebRequest webRequest = WebRequest.Create($"http://{this.Ip}:{this.Host}/api/chat");
                webRequest.Method = "POST";
                string postData = JsonConvert.SerializeObject(message);
                webRequest.ContentType = "application/json";
                StreamWriter reqStream = new StreamWriter(webRequest.GetRequestStream());
                reqStream.Write(postData);
                reqStream.Close();
                webRequest.GetResponse();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.textBox.Text != null)
            {
                string name = this.dataPerson.Login;
                string text = this.textBox.Text;
                SendMessage(new Message(name, text));
            }
            this.textBox.Text = null;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            SendMessage(new Message('#' + dataPerson.Login, " is not online"));
            this.UpdateMessgethread.Abort();
        }
    }
}
