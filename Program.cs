
using ConsoleApplication2.DZ1;
using ConsoleApplication2.DZ1.Letter.Interfaces;
using Ninject;
    using Ninject.Modules;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace ConsoleApplication2
    {
        class Program
        {
            static void Main(string[] args)
            {
            string addressFrom = @"C:\Users\Andrew\Desktop\Таня\TestC-\DZ1\Текстовые файлы\TextFile1.txt";
            string addressTo = @"C:\Users\Andrew\Desktop\Таня\TestC-\DZ1\Текстовые файлы\TextFile2.txt";
                WorkWithFile.copy(Path.GetFullPath(addressFrom), Path.GetFullPath(addressTo));
            }
        }
    }

