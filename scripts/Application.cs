using Humanizer;
using Reactive.Framework;
using Reactive.Framework.UI;
using Reactive.Framework.Error;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;
using Reactive.Utils;

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
        /// A reference to our assembly
        /// </summary>
        public static readonly Assembly MainAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();

        /// <summary>
        /// The Main Assembly Name
        /// </summary>
        public static readonly string MainAssemblyName = MainAssembly.GetName().Name;

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

        /// <summary>
        /// This function always run before main UI starts
        /// </summary>
        public static void InvokeBeforeRun()
        {
            Framework.Error.Debug.Log("Environment.ProcessorCount: " + MyEnvironment.ProcessorCount);
            Framework.Error.Debug.Log("Environment.OSVersion: " + Environment.OSVersion);
            Framework.Error.Debug.Log("Environment.CommandLine: " + Environment.CommandLine);
            Framework.Error.Debug.Log("Environment.Is64BitProcess: " + MyEnvironment.Is64BitProcess);
            Framework.Error.Debug.Log("Environment.Is64BitOperatingSystem: " + Environment.Is64BitOperatingSystem);
            Framework.Error.Debug.Log("Environment.CurrentDirectory: " + Environment.CurrentDirectory);
            Framework.Error.Debug.Log("MainAssembly.ProcessorArchitecture: " + Assembly.GetExecutingAssembly().GetArchitecture());
            Framework.Error.Debug.Log("IntPtr.Size: " + IntPtr.Size.ToString());
            Framework.Error.Debug.Log("Default Culture: " + CultureInfo.CurrentCulture.Name);
            Framework.Error.Debug.Log("Default UI Culture: " + CultureInfo.CurrentUICulture.Name);
            Framework.Error.Debug.Log("IsAdmin: " + new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator));

            Thread.CurrentThread.Name = "Main thread";
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            Application.ApplicationExit += new EventHandler(Reactive.Framework.Events.ApplicationEvents.onApplicationExit);
        }

        /// <summary>
        /// Checks if program is running in 64bit environment
        /// </summary>
        public bool Check64Bit()
        {
            if (!MyEnvironment.Is64BitProcess)
            {
                string text = "Reactive cannot be started in 32-bit mode, ";
                text = text + "because it uses 64bit libraries." + MyEnvironment.NewLine + MyEnvironment.NewLine;
                text = text + "Do you want to open website with more information about this particular issue?" + MyEnvironment.NewLine + MyEnvironment.NewLine;
                text = text + "Press Yes to open website with info" + MyEnvironment.NewLine;
                string text2 = text;
                text = string.Concat(new string[]
                {
            text2,
            MyEnvironment.NewLine
                });
                text += "Press Cancel to close this dialog";
                MessageBoxResult messageBoxResult = MyMessageBox.Show(IntPtr.Zero, text, ".NET Framework 64-bit error", Framework.UI.MessageBoxOptions.YesNoCancel);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Process.Start("http://github.com/ReactiveTeam/ReactiveMain/wiki");
                }
                else if (messageBoxResult == MessageBoxResult.No)
                {
                    Application.Exit();
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check for a single instance of the program
        /// </summary>
        public bool CheckSingleInstance()
        {
            SingleInstance mySingleProgramInstance = new SingleInstance(MainAssemblyName);
            if (!mySingleProgramInstance.IsSingleInstance)
            {
                Framework.Error.Debug.Log("Reactive is already running. Only one instance is allowed.");
                return false;
            }
            return true;
        }
    }
}