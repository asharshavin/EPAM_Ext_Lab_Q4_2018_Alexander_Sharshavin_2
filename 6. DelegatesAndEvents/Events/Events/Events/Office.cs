using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class Office : IObservable
    {
        private List<IObserver> Employees { get; set; }

        public Office()
        {
            Employees = new List<IObserver>();
        }

        public void AddObserver(IObserver o)
        {
            Employees.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            Employees.Remove(o);
        }

        public void NotifyObserversAboutCame(IObserver cameObserver, EventArgsChild eventChild)
        {
            foreach (IObserver observer in Employees) 
                observer.SayHello((Person)cameObserver, eventChild.dateTime);
        }

        public void NotifyObserversAboutGone(IObserver goneObserver)
        {
            foreach (IObserver observer in Employees)
                observer.SayBye((Person)goneObserver);
        }
    }
}
