using ConsoleApplication2.DZ1.Letter.Classes;
using ConsoleApplication2.DZ1.Letter.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.DZ1
{
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
