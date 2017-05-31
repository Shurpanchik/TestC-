
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
            using (StreamReader fs = new StreamReader(@addressFrom))
            {
                using (StreamWriter writer = new StreamWriter(@addressTo))
                {
                    string s = fs.ReadLine();
                    while (s != null)
                    {
                        IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());
                        ILetter iletter;
                        switch (s.Substring(0, 0))
                        {
                            case "A":
                                iletter = ninjectKernel.Get<A>();
                                break;
                            case "B":
                                iletter = ninjectKernel.Get<B>();
                                break;
                            default:
                                iletter = ninjectKernel.Get<C>();
                                break;
                        }
                        writer.Write(iletter.addLetterToEnd(s));
                        //writer.Write(s);
                        writer.Write(Environment.NewLine);
                        s = fs.ReadLine();
                    }
                    writer.Close();
                }
            }
        }
    }

    // https://habrahabr.ru/post/131993/

    public interface ILetter
        {
            string addLetterToEnd(string text);
        }
        class A : ILetter
        {
            public string addLetterToEnd(string text)
            {
                text += "A";
                return text;
            }
        }
        class B : ILetter
        {
            public string addLetterToEnd(string text)
            {
                text += "B";
                return text;
            }
        }
        class C : ILetter
        {
            public string addLetterToEnd(string text)
            {
                text += "C";
                return text;
            }
        }

        class SimpleConfigModule : NinjectModule
        {
            public override void Load()
            {
                Bind<ILetter>().To<A>();
                Bind<ILetter>().To<B>();
                Bind<ILetter>().To<C>();
        }
        }
    }

