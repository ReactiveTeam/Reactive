using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reactive.Utils
{
    public class SingleInstance
    {
        private Mutex m_mutex;

        private bool m_weOwn;

        public bool IsSingleInstance
        {
            get
            {
                return this.m_weOwn;
            }
        }

        public SingleInstance()
        {
            this.m_mutex = new Mutex(true, Assembly.GetExecutingAssembly().GetName().Name, out this.m_weOwn);
        }

        public SingleInstance(string identifier)
        {
            this.m_mutex = new Mutex(true, identifier, out this.m_weOwn);
        }

        public void Close()
        {
            if (this.m_weOwn)
            {
                this.m_mutex.ReleaseMutex();
                this.m_mutex.Close();
                this.m_weOwn = false;
            }
        }
    }
}
