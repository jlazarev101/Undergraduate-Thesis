using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BIZ
{
    //declaration of personal data object
    public class Person : Data
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
       
        public Person(string fn, string sn, string em, string ph)
        {
            FirstName = fn;
            SurName = sn;
            Email = em;
            Phone = ph;
        }

    }
}
