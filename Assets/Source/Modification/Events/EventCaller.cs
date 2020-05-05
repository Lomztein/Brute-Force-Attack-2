using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Events
{
    public class EventCaller
    {
        private Event _event;

        public EventCaller (Event @event) {
            _event = @event;
        }

        public void CallEvent ()
        {

        }

    }
}
