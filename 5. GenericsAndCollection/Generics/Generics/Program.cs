using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            var MyRepository= new Repository();
            MyRepository.Save(new User() { name = "alpha" });
            MyRepository.Save(new User() { name = "beta" });

            var MyList = MyRepository.GetAll();

            Console.WriteLine();
            foreach (User Element in MyList)
            {
                Console.WriteLine(Element.name);
            }
            Console.ReadLine();

            MyRepository.Delete(1);

            MyList = MyRepository.GetAll();

            Console.WriteLine();
            foreach (User Element in MyList)
            {
                Console.WriteLine(Element.name);
            }
            Console.ReadLine();
        }
    }
}
