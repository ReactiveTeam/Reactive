using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactive.Framework.Events
{
    /// <summary>
    /// CEFEventHandler delegate.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    public delegate void CEFLoadPage(object source, CEFEventArgs e);

    /// <summary>
    /// CEFEventArgs is the base class for the CEFEvent Arguments
    /// </summary>
    public class CEFEventArgs : EventArgs
    {
        public string EventInfo;
    }

    public class CEFOnLoadPageArgs : CEFEventArgs
    {
        public string pageToLoad;
        public CEFOnLoadPageArgs(string text, string pageLoad)
        {
            EventInfo = text;
            pageToLoad = pageLoad;
        }
    }
}
