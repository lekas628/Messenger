using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string Ip;
        private int Host;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Ip = this.IpTextBox.Text;
                this.Host = int.Parse(this.HostTextBox.Text.ToString());
                DataPerson dataPerson = new DataPerson(this.LoginText.Text.ToString(), this.PasswordText.Password.ToString());
                string json = JsonSerializer.Serialize(dataPerson);

                var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://{this.Ip}:{this.Host}/api/Authorization");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                var streamReader = new StreamReader(httpResponse.GetResponseStream());
                bool result = bool.Parse(streamReader.ReadToEnd());
                if (result)
                {
                    MainWindow mainWindow = new MainWindow(dataPerson, Ip, Host);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ops...");
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void Regist_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }
    }
}
