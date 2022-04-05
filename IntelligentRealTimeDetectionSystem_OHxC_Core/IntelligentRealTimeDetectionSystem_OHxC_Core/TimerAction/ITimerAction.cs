using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.mirle.iibg3k0.ids.ohxc.TimerAction
{
    public abstract class ITimerAction
    {
        public string Name { get; }
        public long IntervalMilliSec { get; set; }

        protected Timer timer;
        protected bool isStarted = false;
        public bool IsStarted { get { return isStarted; } }

        public ITimerAction(string name, long intervalMilliSec)
        {
            this.Name = name;
            this.IntervalMilliSec = intervalMilliSec;
        }

        public async void start()
        {
            await Task.Delay(10);
            initStart();
            if (timer != null)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
                timer.Dispose();
                timer = null;
            }
            isStarted = true;
            timer = new Timer(new TimerCallback(doProcess), null, 0, IntervalMilliSec);
        }

        public async void stop()
        {
            await Task.Delay(10);
            if (timer != null)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
                timer.Dispose();
                timer = null;
            }
            isStarted = false;
        }

        public abstract void doProcess(object obj);

        public abstract void initStart();
    }
}
