using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DAL
{
    //this is where most of the stored procedures are called with the different data types inserted for manipulation
    public class Data : DAO
    {
        //this taks all registration data and adds it to different tables
        public void RegistrationALL(string FirstName, string SurName, string Email, string Phone, string username, string password)
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspUploadData";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fn", FirstName);
            cmd.Parameters.AddWithValue("@sn", SurName);
            cmd.Parameters.AddWithValue("@em", Email);
            cmd.Parameters.AddWithValue("@ph", Phone);
            cmd.Parameters.AddWithValue("@user", username);
            cmd.Parameters.AddWithValue("@pass", password);
            cmd.ExecuteNonQuery();
            CloseCon();
        }

        //this is used to check whether the person trying to login has an account or not;
        public bool GetUser(string user, string pass)
        {
            bool loginSuccessful;

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspLogin";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@pass", pass);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            CloseCon();
            if (count > 0)
            {
                loginSuccessful = true;
                return loginSuccessful;
            }
            else
            {
                loginSuccessful = false;
                return loginSuccessful;
            }


        }

        //this adds the flashcards to the table
        public void FlashcardCreate(string question, string answer, string stack, string user)
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspUploadFlash";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@qn", question);
            cmd.Parameters.AddWithValue("@ar", answer);
            cmd.Parameters.AddWithValue("@sk", stack);
            cmd.Parameters.AddWithValue("@user", user);

            cmd.ExecuteNonQuery();
            CloseCon();
        }

        //this fetches the stack based on the user who is logged in
        public void GetStackByUsername (string user)
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspGetStackByUser";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user", user);

            cmd.ExecuteNonQuery();
            CloseCon();

        }
        //this deletes stack based on the logged in user and the stack selected
        public void DeleteStack (string user, string stack)
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspDeleteStack";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@sk", stack);
            cmd.ExecuteNonQuery();
            CloseCon();

        }

        //this checks whether the user that they want to share to exists
        public bool CheckUser(string sharee)
        {
            bool userExists = false;
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspCheckUser";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user", sharee);
            object result = cmd.ExecuteScalar();
            userExists = Convert.ToBoolean(result);

            CloseCon();

            return userExists;
        }

        //this shares the selected stack with a user
        public void ShareStack(string user, string stack, string sharee)
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspShareFlashcardToUser";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@sk", stack);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@nuser", sharee);
            cmd.ExecuteNonQuery();
            CloseCon();

        }

        //adds stack to the public table which then is able to be viewed by all users
        public void PublishStack(string user, string stack)
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspAddPublicStack";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@sk", stack);
            cmd.ExecuteNonQuery();
            CloseCon();
        }

        //grabs the stack selected and duplicates it with the current users username
        public void ImportStack(string user, string stack, string author)
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspImportStack";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nusr", user);
            cmd.Parameters.AddWithValue("@psk", stack);
            cmd.Parameters.AddWithValue("@usr", author);
            cmd.ExecuteNonQuery();
            CloseCon();
        }

        public void AddScore(string user, string stack, int rightans, int total, DateTime date)
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspAddScore";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@usr", user);
            cmd.Parameters.AddWithValue("@sk", stack);
            cmd.Parameters.AddWithValue("@rans", rightans);
            cmd.Parameters.AddWithValue("@total", total);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.ExecuteNonQuery();
            CloseCon();
        }


    }
}
