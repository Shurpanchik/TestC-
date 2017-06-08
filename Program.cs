
using ConsoleApplication2.DZ1;
using System.IO;

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

