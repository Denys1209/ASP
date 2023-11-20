using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelCatalog.ConsoleView
{
    public class MyException : Exception
    {
        // Constructors
        public MyException() { }

        public MyException(string message) : base(message) { }
        
    }
}
