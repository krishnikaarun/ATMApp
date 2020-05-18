using System;
using Database1.Banks;
using System.Linq;
using System.Data.SqlClient;
using System.Data;

namespace Database1
{
    class Program
    {
        static void Main(string[] args)
        {
            //To Bank Program
            Bank b1 = new Bank();
            b1.MainAtm();
            Console.ReadKey();
        }

    }

}






