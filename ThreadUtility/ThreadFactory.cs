using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoraBot.ThreadUtility
{
    public class ThreadFactory
    {
        private static ThreadFactory _instance;
        public static ThreadFactory Instance => _instance ?? (_instance = new ThreadFactory());
        public Thread Start(ThreadStart threadStart, string Name="",bool IsBackground= false,bool DoNotStart=false)
        {
            var thread = new Thread(threadStart);
            thread.IsBackground = IsBackground;
            if(!string.IsNullOrEmpty(Name) ) 
                thread.Name = Name;
            if(!DoNotStart)
                thread.Start();
            return thread;
        }
    }
}
