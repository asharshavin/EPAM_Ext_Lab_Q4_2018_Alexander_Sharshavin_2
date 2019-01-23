using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    // ki. Хорошая реализация. 
    class Person : IObserver
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    _name = "";
                else
                    _name = value;
            }
        }

        IObservable office;

        public Person(string name, IObservable office)
        {
            Name = name;
            this.office = office;

        }

        public void Came(EventArgsChild cameTime)
        {
            Console.WriteLine();
            Console.WriteLine("[На работу пришел {0}]", Name, cameTime.dateTime.Hour);
            office.NotifyObserversAboutCame(this, cameTime);
            this.office.AddObserver(this);
        }

        public void Gone()
        {
            Console.WriteLine();
            Console.WriteLine("[{0} ушел домой]", Name);
            this.office.RemoveObserver(this);
            office.NotifyObserversAboutGone(this);
        }

        public void SayHello(Person employee, DateTime time)
        {
            if (time.Hour < 12)
                Console.WriteLine("'Доброе утро, {0}!', - сказал {1}.", employee.Name, Name);
            else if (time.Hour >= 12 && time.Hour < 17)
                Console.WriteLine("'Добрый день, {0}!', - сказал {1}.", employee.Name, Name);
            else
                Console.WriteLine("'Добрый вечер, {0}!', - сказал {1}.", employee.Name, Name);
        }

        public void SayBye(Person employee)
        {
            Console.WriteLine("'До свидания, {0}!', - сказал {1}.", employee.Name, Name);
        }
    }
}
