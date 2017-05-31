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
            WorkWithFile.copy(@"~\ConsoleApplication2\ConsoleApplication2\Текстовые файлы\TextFile1.txt",
                @"~\ConsoleApplication2\ConsoleApplication2\Текстовые файлы\TextFile2.txt");
        }
    }

    class WorkWithFile
    {
        public static void copy(string addressFrom, string addressTo)
        {
            // источник https://ru.stackoverflow.com/questions/356355/
            //  и статья http://mycsharp.ru/post/21/2013_06_12_rabota_s_fajlami_v_si-sharp_klassy_streamreader_i_streamwriter.html
            using (StreamReader fs = new StreamReader(@addressFrom) ) {
                using (StreamWriter writer = new StreamWriter(@addressTo)) {
                    string s = "";
                    while (s != null)
                    {
                        s = fs.ReadLine();
                        writer.Write(s);
                        writer.Write(Environment.NewLine);
                    }
                    writer.Close();
                }
            } 
        }
    }
}
