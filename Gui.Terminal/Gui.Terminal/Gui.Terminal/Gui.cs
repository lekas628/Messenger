using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Gui.Terminal
{
    static class Gui
    {
        public static void run()
        {
            ColorScheme color = Colors.TopLevel;

            Application.Init();
            var top = Application.Top;

            var menu = new MenuBar(
                new MenuBarItem[]
                {
                    new MenuBarItem("_File",
                        new MenuItem[]
                        {
                             new MenuItem("_Close", "", null),
                             new MenuItem("_Quit", "", () => { Application.RequestStop(); }),
                        }),
                    new MenuBarItem("_Edit",
                        new MenuItem[]
                        {
                            new MenuItem("_Copy", "", null),
                            new MenuItem("_Cut", "", null),
                            new MenuItem("_Paste", "", null),
                        })
                });
            top.Add(menu);

            var win = new Window("MainWindow")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                //ColorScheme = color,
            };
            top.Add(win);

            var left_win = new Window("chats")
            {
                X = 0,
                Y = 1,
                Width = 20,
                Height = Dim.Fill(),
                //ColorScheme = color,
            };
            win.Add(left_win);

            var right_win = new Window("chat")
            {
                X = 20,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                //ColorScheme = color,
            };
            win.Add(right_win);


            string server_status_string = "online";
            var server_status_label = new Label(server_status_string)
            {
                X = Pos.Right(win) - server_status_string.Length - 2 - 1,
                Y = 0,
                Height = 1,
            };
            win.Add(server_status_label);





            string username_string = "username";
            var username_label = new Label(username_string)
            {
                X = Pos.Right(win) - username_string.Length - 2 - server_status_string.Length - 3,
                Y = 0,
                Height = 1,
            };
            win.Add(username_label);


            var time_label = new TimeField()
            {
                X = Pos.Center(),
                Y = 0,
                Height = 1,
                CanFocus = false,

            };
            win.Add(time_label);




            string test_1 = $"hello";
            var test_label_1 = new Label(test_1)
            {
                X = Pos.Center(),
                Y = Pos.Center() + 1,
            };
            right_win.Add(test_label_1);


            string test = $"{right_win.Bounds.Y}";
            var test_label = new Label(test)
            {
                X = Pos.Center(),
                Y = Pos.Center(),
            };
            right_win.Add(test_label);


            Application.Run();
        }
    }
}
