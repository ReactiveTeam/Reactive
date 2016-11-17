using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactive.Framework.Jobs
{
    /// <summary>
    /// Handles the loop, start, and stop functions of the PluginHandler
    /// </summary>
    public class JobHandler
    {
        private static List<Action> start = new List<Action>();
        private static List<Action> loop = new List<Action>();
        private static List<Action> stop = new List<Action>();

        public static void AddToStart(Action action)
        {
            start.Add(action);
        }

        public static void AddToLoop(Action action)
        {
            loop.Add(action);
        }

        public static void AddToStop(Action action)
        {
            stop.Add(action);
        }

        public static void Start()
        {
            foreach(Action func in start)
            {
                func.Invoke();
            }
        }
    }
}
