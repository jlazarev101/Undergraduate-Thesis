using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BIZ
{   
    //declaration of login tables and of current user which is used to recall flashcards based on who is logged in
    public class LoginDetails : Data
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }


        public LoginDetails(string username, string password, string firstname)
        {
            Username = username;
            Password = password;
            Firstname = firstname;

        }

    }

    public static class SessionManager
    {
        public static string CurrentUser { get; private set; }

        public static void Login(string user)
        {
            CurrentUser = user;
        }

    }


}
