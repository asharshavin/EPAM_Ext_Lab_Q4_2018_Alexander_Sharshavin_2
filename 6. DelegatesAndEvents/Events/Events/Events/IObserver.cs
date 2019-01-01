using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    interface IObserver
    {
        void Came(EventArgsChild cameTime);

        void Gone();

        void SayHello(Person employee, DateTime time);

        void SayBye(Person employee);
    }
}
