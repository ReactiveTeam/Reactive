// Copyright (c) ReactiveTeam. All rights reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.

using Reactive.Framework.Error;
using System;
using System.Collections.Generic;
using System.Threading;

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
            Debug.Log("PluginManager: All plugins initialized.");
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
            Debug.Log("PluginManager: All plugins successfully stopped.");
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
