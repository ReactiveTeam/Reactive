using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactive.Framework.Events
{
    public class ApplicationEvents
    {

        /// <summary>
        /// Runs on ApplicationExit
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        public static void onApplicationExit(object Sender, EventArgs e)
        {
            Plugin.PluginManager.StopHost();
        }
    }
}
