using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPF_Presentation
{
   
    public class DataReceivedEventArgs : EventArgs
    { }


    /// <summary>
    /// 1- Define a delegate
    /// 2- Define an event based on that delegate
    /// 3- Rais the event
    /// </summary>
    public class DataReceiver
    {

        public delegate void DataReceivedEventHandler(object source, EventArgs args);
        public event DataReceivedEventHandler DataReceived;


        //Reference https://learn.microsoft.com/en-us/dotnet/api/system.timers.timer?view=net-6.0
        private static System.Timers.Timer aTimer;

        public DataReceiver()
        {
            // Create a timer and set a two second interval.
            aTimer = new System.Timers.Timer();

            //Rais OnTimedEvent Every 2 seconds
            aTimer.Interval = 2000; 

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;

            // Have the timer fire repeated events (true is the default)
            aTimer.AutoReset = true;

            
        }

        // Start the timer
        public void Start() => aTimer.Enabled = true;

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Receive();
        }

        private void Receive()
        {
            OnDataReceived();
        }

        protected virtual void OnDataReceived()
        {
            if (DataReceived != null)
                DataReceived(this, EventArgs.Empty);
        }
    }
}
