using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BIZ
{
    //hash class used to hash the password within the database for security
    public class HashPass
    {
        public string Passhash(string data)
        {
            SHA1 sha = SHA1.Create();

            byte[] hashdata = sha.ComputeHash(Encoding.Default.GetBytes(data));

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hashdata.Length; i++)
            {
                sb.Append(hashdata[i].ToString());
            }

            return sb.ToString();
        }
    }
}
