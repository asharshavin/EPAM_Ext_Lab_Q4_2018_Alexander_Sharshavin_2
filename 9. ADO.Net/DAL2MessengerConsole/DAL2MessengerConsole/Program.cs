using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;
using System.Data;

namespace DAL2MessengerConsole
{
    class Program
    {
        //private const string MessengerConnectionString = "MessengerConection";

        static void Main(string[] args)
        {
            var repository = new UserRepository();

            repository.Save(new User() { id = 1, name = "beta" });
            repository.Save(new User() { id = 3, name = "alpha" });

            var user = repository.Get(1);
            Console.WriteLine("{0} - {1}", user.id, user.name);

            Console.WriteLine();

            var listUser = repository.GetAll();
            foreach (User element in listUser)
            {
                Console.WriteLine("{0} - {1}", element.id, element.name);
            }

            Console.WriteLine();

            repository.Delete(3);

            foreach (User element in repository.GetAll())
            {
                Console.WriteLine("{0} - {1}", element.id, element.name);
            }

            Console.ReadKey();
        }
    }
}
