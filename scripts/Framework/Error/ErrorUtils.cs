using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactive.Framework.Error
{
    public class ErrorUtils
    {

        /// <summary>
        /// Performs a stack trace to see where things went wrong
        /// for error reporting.
        /// </summary>
        public static string GetErrorLocation(int level = 1, bool showOnlyLast = false)
        {

            StackTrace stackTrace = new StackTrace();
            string result = "";
            string declaringType = "";

            for (int v = stackTrace.FrameCount - 1; v > level; v--)
            {
                if (v < stackTrace.FrameCount - 1)
                    result += " --> ";
                StackFrame stackFrame = stackTrace.GetFrame(v);
                if (stackFrame.GetMethod().DeclaringType.ToString() == declaringType)
                    result = "";    // only report the last called method within every class
                declaringType = stackFrame.GetMethod().DeclaringType.ToString();
                result += declaringType + ":" + stackFrame.GetMethod().Name;
            }

            if (showOnlyLast)
            {
                try
                {
                    result = result.Substring(result.LastIndexOf(" --> "));
                    result = result.Replace(" --> ", "");
                }
                catch
                {
                }
            }

            return result;

        }
    }
}
