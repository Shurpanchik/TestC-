using ConsoleApplication2.DZ1.Letter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DZ1.Letter.Classes
{
    class C : ILetter
    {
        public string addLetterToEnd(string text)
        {
            text += "C";
            return text;
        }
    }
}
