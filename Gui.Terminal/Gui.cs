using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Terminal.Gui;

namespace Gui.Terminal
{
    public class ChatApp
    {

        static Action running = loginScreen;
        static List<Func<bool>> idleHandlers = new List<Func<bool>>();
        static IMainLoopDriver Driver { get; }


        static string username;
        
        public static void run()
        {
            while (running != null)
            {
                running.Invoke();
            }
            Application.Shutdown();
        }


        static void MainApp()
        {
            Application.Init();

            MenuBar menu = new MenuBar(
                new MenuBarItem[]
                {
                    new MenuBarItem("_File",
                        new MenuItem[]
                        {    new MenuItem("_Change user","", () => { running = loginScreen; Application.RequestStop(); }),
                             new MenuItem("_Close", "", null),
                             new MenuItem("_Quit", "", () => { running = null; Application.RequestStop(); }),
                        }),
                    //new MenuBarItem("_Edit",
                    //    new MenuItem[]
                    //    {
                    //        new MenuItem("_Copy", "", null),
                    //        new MenuItem("_Cut", "", null),
                    //        new MenuItem("_Paste", "", null),
                    //    }),
                     //new MenuBarItem("Settings",
                     //   new MenuItem[]
                     //   {
                     //       new MenuItem("_Username", "", null),
                     //   }),
                });
            Application.Top.Add(menu);

            Window win = new Window("MainWindow")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };
            Application.Top.Add(win);

            Window leftWin = new Window("chats")
            {
                X = 0,
                Y = 1,
                Width = 20,
                Height = Dim.Fill(),
            };
            win.Add(leftWin);

            Window rightWin = new Window("chat")
            {
                X = Pos.Right(leftWin),
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };
            win.Add(rightWin);

            View leftChatView = new View()
            {
                X = 0,
                Y = 0,
                Width = leftWin.Width,
                Height = 1,
                Text = "Main Chat",
            };
            leftWin.Add(leftChatView);

            string server_status_string = "------";
            Label serverStatusLabel = new Label(server_status_string)
            {
                X = Pos.Right(win) - 10,
                Y = 0,
                Height = 1,
            };
            win.Add(serverStatusLabel);


            string username_string;
            if (username == null)
                username_string = "name is empty";
            else
                username_string = username;
            Label usernameLabel = new Label(username_string)
            {
                X = Pos.Right(serverStatusLabel) - 25,
                Y = 0,
                Height = 1,
            };
            win.Add(usernameLabel);

            TimeField timeField = new TimeField()
            {
                X = Pos.Center(),
                Y = 0,
                Height = 1,
                CanFocus = false,

            };
            win.Add(timeField);

            Window winMessages = new Window()
            {
                X = 0,
                Y = 0,
                Width = rightWin.Width,
                Height = rightWin.Height - 1,
            };
            rightWin.Add(winMessages);

            Label labelMessage = new Label()
            {
                X = 0,
                Y = Pos.Bottom(rightWin) - 4,
                Width = 10,
                Height = 1,
                Text = "Message:",
                TextAlignment = TextAlignment.Right,
            };
            rightWin.Add(labelMessage);

            TextField fieldMessage = new TextField()
            {
                X = Pos.Right(labelMessage) + 1,
                Y = Pos.Bottom(rightWin) - 4,
                Width = rightWin.Width - 15,
                Height = 1,
            };
            rightWin.Add(fieldMessage);

            Button buttonSend = new Button()
            {
                X = Pos.Right(fieldMessage) + 1,
                Y = Pos.Bottom(rightWin) - 4,
                Width = 15,
                Height = 2,
                Text = "  SEND  ",
            };
            buttonSend.Clicked += () => { OnBtnSendClick(fieldMessage); };
            rightWin.Add(buttonSend);


            int lastMsgID = Program.currentMessagesPointer;
            Timer updateLoop = new Timer();
            updateLoop.Interval = 1000;
            updateLoop.Elapsed += (object sender, ElapsedEventArgs e) => {

                (Message msg, bool serverStatus) = API.GetMessage(lastMsgID);

                if (msg != null)
                {
                    Program.messagesClass.Add(msg);
                    MessagesUpdate(winMessages);
                    lastMsgID++;
                }

                StatusUpdate(serverStatusLabel, serverStatus);

            };
            updateLoop.Start();

            Application.Run();
        }

        static void MessagesUpdate(Window winMessages)
        {
            winMessages.RemoveAll();
            int offset = 0;
            for (var i = Program.messagesClass.GetCountMessages() - 1; i >= 0; i--)
            {
                View msg = PrepairMessage(Program.messagesClass.Get(i), winMessages, offset);
                winMessages.Add(msg);
                offset++;
            }
            Application.Refresh();
        }

        static View PrepairMessage(Message message, Window winMessages, int offset)
        {
            View msg = new View()
            {
                X = 0,
                Y = offset,
                Width = winMessages.Width,
                Height = 1,
                Text = $"[{message.Name}] {message.Text}",
            };
            return msg;
        }


        static void StatusUpdate(Label serverStatusLabel, bool serverStatus)
        {
            serverStatusLabel.RemoveAll();
            if (serverStatus)
                serverStatusLabel.Text = "Online";
            else
                serverStatusLabel.Text = "#Offline#";
        }

        static void OnBtnSendClick(TextField fieldMessage)
        {
            if (username.Length != 0 && fieldMessage.Text.Length != 0)
            {
                Message msg = new Message()
                {
                    Name = username,
                    Text = fieldMessage.Text.ToString(),
                };
                API.SendMessage(msg);
                fieldMessage.Text = "";
            }
        }

        static void loginScreen()
        {
            Application.Init();

            var win = new Window("login_screen")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };
            Application.Top.Add(win);

            var login = new Label("Login: ") { X = 3, Y = 6 };
            var password = new Label("Password: ")
            {
                X = Pos.Left(login),
                Y = Pos.Bottom(login) + 1
            };
            var loginText = new TextField("")
            {
                X = Pos.Right(password),
                Y = Pos.Top(login),
                Width = 40
            };
            var passText = new TextField("")
            {
                Secret = true,
                X = Pos.Left(loginText),
                Y = Pos.Top(password),
                Width = Dim.Width(loginText)
            };

            var button = new Button("Login")
            {
                X = Pos.Right(passText) - 8,
                Y = Pos.Bottom(passText) + 1,
                Width = 10,
                Height = 1,
            };
            button.Clicked += () =>
            {
                username = loginText.Text.ToString();
                running = MainApp; 
                Application.RequestStop();
            };


            win.Add(login,
                    password,
                    loginText,
                    passText,
                    button);

            Application.Run();
        }

        Func<bool> AddIdle(Func<bool> idleHandler)
        {
            lock (idleHandlers)
            {
                idleHandlers.Add(idleHandler);
            }

            Driver.Wakeup();
            return idleHandler;
        }

        public void Invoke(Action action)
        {
            AddIdle(() => {
                action();
                return false;
            });
        }
    }
}
