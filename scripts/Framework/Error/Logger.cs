using System;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Windows.Forms;
using System.IO;

namespace Reactive.Framework.Error
{
    /// <summary>
    ///   <para>Class containing methods to ease debugging.</para>
    /// </summary>
    public class Debug
    {
        private static string FileName = null;

        static Debug()
        {
            FileName = string.Format(@"{0}\error.log", Application.StartupPath);

            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
        }

        /// <summary>
        ///   <para>Logs message to the Logger.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void Log(object message,bool showPopup = false)
        {
            if (showPopup)
            {
                MessageBox.Show(message.ToString(),"Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            WriteLog(message);
        }

        /// <summary>
        ///   <para>A variant of Debug.Log that logs an error message to the Logger.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void LogFatal(object message,bool showPopup = false,string popupmessage = null)
        {
            if (showPopup)
            {
                if (!string.IsNullOrEmpty(popupmessage))
                {
                    MessageBox.Show(popupmessage, "Fatal Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(message.ToString(), "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            WriteLog(message);
            WriteLog("Renderer: Fatal Exception! Exiting application");
            Application.Exit();
        }

        /// <summary>
        ///   <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="exception">Runtime Exception.</param>
        public static void LogException(Exception exception)
        {

        }

        /// <summary>
        ///   <para>Occurs when the application exits.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void Exit(object sender, EventArgs e)
        {
            WriteLog("Application: Exiting application");
        }

        internal static void WriteLog(object log)
        {
            using (StreamWriter writer = new StreamWriter(FileName, true))
            {
                string s = string.Format("[{0}] - {1}", DateTime.Now, log);
                writer.WriteLine(s);
            }
        }
    }
}
