using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_Encryption_.Classes
{
    interface ICryptographer
    {
        string Encrypt(string text);

        string Decrypt(string text);
    }
}
