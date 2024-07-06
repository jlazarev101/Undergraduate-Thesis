using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
   //Declaration of flashcard objects
    public class Flashcard
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Stack { get; set; }
        public string Creator { get; set; }

        
        public Flashcard(string qn, string ar, string sk, string cr)
        {
            Question = qn;
            Answer = ar;
            Stack = sk;
            Creator = cr;  
        }

        public override string ToString()
        {
            return $"Q: {Question}\nA: {Answer}";
        }

    }

    public class PublicCard
    {
        public string Title { get; set; }
        public string Author { get; set; }
    }

}
