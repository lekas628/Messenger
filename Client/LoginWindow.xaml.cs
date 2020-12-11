using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataPerson dataPerson = new DataPerson(this.LoginText.Text.ToString(), this.PasswordText.Password.ToString());
            string json = JsonSerializer.Serialize(dataPerson);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:5000/api/Authorization");
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
                MainWindow mainWindow = new MainWindow(dataPerson);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ops...");
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
