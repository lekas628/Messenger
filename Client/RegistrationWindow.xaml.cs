using System.IO;
using System.Net;
using System.Text.Json;
using System.Windows;


namespace Client
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private string Ip;
        private int Host;
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.FirstPasswordText.Password == this.SecondPasswordText.Password)
            {
                this.Ip = this.IpTextBox.Text;
                this.Host = int.Parse(this.HostTextBox.Text);
                DataPerson dataPerson = new DataPerson(this.LoginText.Text.ToString(), this.FirstPasswordText.Password.ToString());
                string json = JsonSerializer.Serialize(dataPerson);

                var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://{this.Ip}:{this.Host}/api/Registration");
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
                else MessageBox.Show("Ops...");
            }
            else MessageBox.Show("Пароли не совпадают");
        }
    }
}
