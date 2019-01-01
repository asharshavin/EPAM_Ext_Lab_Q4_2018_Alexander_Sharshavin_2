using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class EventArgsChild : EventArgs
    {
        public DateTime dateTime { get; set; }
        public EventArgsChild(DateTime dateTime)
        {
            this.dateTime = dateTime;
        }
    }
}
