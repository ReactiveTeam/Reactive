using Reactive.Framework.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        private static Timer funcTimer;

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
            foreach (Action func in start)
            {
                func.Invoke();
            }

        }

        public static void Update()
        {
            funcTimer = new System.Threading.Timer(new TimerCallback(LoopCallback), null, 0, 1000);
        }

        public static void Stop()
        {
            funcTimer.Dispose();

            foreach(Action func in stop)
            {
                func.Invoke();
            }
        }

        private static void LoopCallback(object state)
        {
            foreach (Action func in loop)
            {
                func.Invoke();
            }
        }
    }
}
