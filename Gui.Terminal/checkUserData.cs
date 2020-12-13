using System;
using System.Text.RegularExpressions;

namespace Gui.Terminal
{

    static class CheckUserData
    {
        static string patternUsername = "^[a-zA-Z0-9]+$";
        static string patternPassword = "^[a-zA-Z0-9_]+$";

        public static string checkUserWhenLogin(string usr, string pass)
        {
            if (Regex.IsMatch(usr, patternUsername))
            {
                if (Regex.IsMatch(pass, patternPassword))
                {
                    if (API.GetServerStatus())
                    {
                        if (API.LoginServer(usr, pass))
                        {
                            return "success";
                        }
                        else return "Login or password is wrong";
                    }
                    else return "Cannot connect to the server";
                }
                else return "Password contain wrong characters";
            }
            else return "Username contain wrong characters";
        }

        public static string checkUserWhenRegister(string usr, string pass, string confirmPass)
        {
            if (Regex.IsMatch(usr, patternUsername))
            {
                if (Regex.IsMatch(pass, patternPassword))
                {
                    if (pass == confirmPass)
                    {
                        if (API.GetServerStatus())
                        {
                            if (API.RegistrationServer(usr, pass))
                            {
                                return "success";
                            }
                            else return "Username already exist";
                        }
                        else return "Cannot connect to the server";
                    }
                    else return "Passwords do no match";
                }
                else return "Password contain wrong characters";
            }
            else return "Username contain wrong characters";
        }
    }
}
