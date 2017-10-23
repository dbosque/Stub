using System;
using System.Windows.Forms;

namespace dBosque.Stub.Editor.Controls.Behaviours
{
    /// <summary>
    /// Timer event on a control
    /// </summary>
    public class PeriodicEventBehaviour 
    {
        /// <summary>
        /// The timer
        /// </summary>
        private readonly Timer _timer;
        /// <summary>
        /// The control to act upong
        /// </summary>
        protected readonly Control _child;
        /// <summary>
        /// The event to invoke
        /// </summary>
        private readonly Action _event;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="c"></param>
        /// <param name="emit"></param>
        /// <param name="i"></param>
        public PeriodicEventBehaviour(Control c, Action emit, int i = 1000)            
        {
            _child = c;
            _event = emit;
            _timer = new Timer() { Enabled = true, Interval = i };
            _timer.Tick += _timer_Tick;
           
        }

        /// <summary>
        /// Enabled/disabled
        /// </summary>
        public bool Enabled { get { return _timer.Enabled; } set { _timer.Enabled = value; } }

        /// <summary>
        /// Time elapsed...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timer_Tick(object sender, EventArgs e)
        {
            _event?.Invoke();
        }
    }
  
}
