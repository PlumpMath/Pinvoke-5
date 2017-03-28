using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PInvokeLib;

namespace TestCaller
{
    class Program : UpCall
    {
        static void Main(string[] args)
        {

            Caller.Initialize();

            DownCall theCaller = new Caller(new Program());

            theCaller.downCall1(49, "Hi");

        }

        public void upCall1(long i, string a)
        {
            System.Console.WriteLine(a + " : " + i);
        }
    }
}
