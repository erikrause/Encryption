using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_Encryption_.Classes
{
    public abstract class Cryptographer
    {
        public Cryptographer()
        {

        }

        public abstract string encrypt(string originalText);


        public abstract string decrypt(string originalText);
    }
}
