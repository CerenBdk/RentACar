using System;
using DataAccess.Concrete;
using Business.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleManager consoleManager = new ConsoleManager();
            consoleManager.Dashboard();
        }
    }
}
