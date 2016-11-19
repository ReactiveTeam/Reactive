using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactive.Framework.Events
{
    public static class ApplicationEvents
    {
        private static List<Action> exitEvents = new List<Action>();

        /// <summary>
        /// Runs on ApplicationExit
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        public static void onApplicationExit(object Sender, EventArgs e)
        {
            foreach(var events in exitEvents)
            {
                events.Invoke();
            }
        }

        /// <summary>
        /// Adds an event to the exitEvent param to be run at the end of Application life cycle
        /// </summary>
        /// <param name="exit"></param>
        public static void addToExitEvent(Action exit)
        {
            if(exit != null)
            {
                exitEvents.Add(exit);
            }
        }
    }
}
