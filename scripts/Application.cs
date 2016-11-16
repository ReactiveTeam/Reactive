using Humanizer;
using Reactive.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reactive
{
    /// <summary>
    ///   <para>Access to application run-time data.</para>
    /// </summary>
    public class App
    {
        static App()
        {
            App.Version = new Framework.Version
            {
                Major = 0,
                Minor = 1,
                Revision = 0,
                Serial = 0,
                Accessibility = Accessibility.Internal
            };
            //System.Windows.Forms.MessageBox.Show(App.Version.ToString());
        }

        /// <summary>
        /// A reference to the plugin manager.
        /// </summary>
        public static Plugin.PluginManager pluginManager = new Plugin.PluginManager();

        /// <summary>
        /// Returns the version of the app
        /// </summary>
        public static Framework.Version Version
        {
            get; internal set;
        }

        /// <summary>
        /// Returns the startup path of the app
        /// </summary>
        public static string StartupPath
        {
            get { return Application.StartupPath; }
        }

        /// <summary>
        /// Returns the current representation of time
        /// </summary>
        public static string CurrentTime
        {
            get { return DateTime.Now.ToString(); }
        }
    }
}