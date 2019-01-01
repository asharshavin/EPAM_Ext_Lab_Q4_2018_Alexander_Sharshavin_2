using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            var office = new Office();

            var john = new Person("Джон", office);
            var bill = new Person("Билл", office);
            var hugo = new Person("Хьюго", office);

            john.Came(new EventArgsChild(new DateTime(2019, 1, 1, 7, 0, 0)));
            bill.Came(new EventArgsChild(new DateTime(2019, 1, 1, 8, 0, 0)));
            hugo.Came(new EventArgsChild(new DateTime(2019, 1, 1, 13, 0, 0)));

            john.Gone();
            bill.Gone();
            hugo.Gone();

            Console.ReadLine();
        }
    }
}
