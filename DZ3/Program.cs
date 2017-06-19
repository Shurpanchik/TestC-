using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static List<Person> Persons;
        static List<WeddingType> WeddingTypes;
        static void Main(string[] args)
        {
            Persons = GetListPersons();
            WeddingTypes = GetListWeddingType();
            Join();
            Console.WriteLine("Средневзвешанное равно: "+ AVGSum());
            Console.Read();
        }

        public static void Join()
        {
            var PersonPlusWedding =
                Persons.Join(WeddingTypes, p => p.TimeMarriages, w => w.years, (p, w) => new { Person = p, WeddingType = w });
            foreach (var item in PersonPlusWedding)
            {
                Console.WriteLine(item.Person.Name + " празднует " + item.WeddingType.type);
            }
        }
        // среднее значение полных прожитых лет в браке
        public static double AVGSum() {
            var PersonPlusWedding =
              Persons.Join(WeddingTypes, p => p.TimeMarriages, w => w.years, (p, w) => new { Person = p, WeddingType = w });
            var count =
                from person in PersonPlusWedding
                group person by person.Person.TimeMarriages into weddingGroup
                select new
                {
                    TimeMarriage = weddingGroup.Key,
                    CountPersons = weddingGroup.Count()
                };
            int sum = count.Sum(x => x.TimeMarriage * x.CountPersons);
            return sum*1.0/PersonPlusWedding.Count() ;
        }
        public static List<Person> GetListPersons()
        {
            List<Person> Persons = new List<Person>();
            for (int i = 0; i < 10; i++)
            {
                Persons.Add(new Person { Name = "Vasya", SecondName = "Petrov", Age = 20 + i, TimeMarriages = i });
            }
                Persons.Add(new Person { Name = "Kostya", SecondName = "Petrov", Age = 20 , TimeMarriages = 2 });
            return Persons;
        }
        public static List<WeddingType> GetListWeddingType()
        {
            List<WeddingType> WeddingTypes = new List<WeddingType>();
            WeddingTypes.Add(new WeddingType { years = 0 });
            WeddingTypes.Add(new WeddingType { years = 1, type = "Ситцевая" });
            WeddingTypes.Add(new WeddingType { years = 2, type = "Бумажная" });
            return WeddingTypes;
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public int Age { get; set; }
        public int TimeMarriages { get; set; }
    }

    public class WeddingType
    {
        public int years { get; set; }
        public string type { get; set; }
    }
}
