﻿using System;
using Database1.Banks;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Database1
{
    public static class Program
    {
        public static void Main()
        {
            //To Bank Program   
            Console.Beep();
            Bank b1 = new Bank();
            b1.MainAtm();
            Console.ReadKey();
        }
    }
}






