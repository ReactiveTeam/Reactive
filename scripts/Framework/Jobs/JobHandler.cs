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
    /// todo: Improve overall performance (Multithreading)
    public class JobHandler
    {
        private static List<Action> start = new List<Action>();
        private static List<Action> loop = new List<Action>();
        private static List<Action> stop = new List<Action>();

        private static Timer funcTimer;

        /// <summary>
        /// Adds an action to the JobHandler Start function.
        /// </summary>
        /// <param name="action"></param>
        public static void AddToStart(Action action)
        {
            start.Add(action);
        }

        /// <summary>
        /// Adds an action to the JobHandler Loop function.
        /// </summary>
        /// <param name="action"></param>
        public static void AddToLoop(Action action)
        {
            loop.Add(action);
        }

        /// <summary>
        /// Adds an action to the JobHandler Stop function.
        /// </summary>
        /// <param name="action"></param>
        public static void AddToStop(Action action)
        {
            stop.Add(action);
        }

        /// <summary>
        /// Unregisters all of a modules actions
        /// </summary>
        /// <param name="action"></param>
        public static void UnregisterAction(Action action)
        {
            start.Remove(action);
            loop.Remove(action);
            stop.Remove(action);
        }

        /// <summary>
        /// This function runs automatically the registered actions at the start of the program.
        /// </summary>
        public static void Start()
        {
            foreach (Action func in start)
            {
                func.Invoke();
            }
        }

        /// <summary>
        /// This function runs every second the registered actions.
        /// </summary>
        public static void Update()
        {
            funcTimer = new System.Threading.Timer(new TimerCallback(LoopCallback), null, 0, 1000);
        }

        /// <summary>
        /// This function runs all the registered actions at the end of the program. (Before the exit)
        /// </summary>
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
