using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    interface IObservable
    {
        void AddObserver(IObserver o);
        void RemoveObserver(IObserver o);
        void NotifyObserversAboutCame(IObserver cameObserver, EventArgsChild eventChild);
        void NotifyObserversAboutGone(IObserver goneObserver);
    }
}
