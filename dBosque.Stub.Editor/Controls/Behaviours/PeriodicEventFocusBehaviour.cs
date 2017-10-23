using System;
using System.Windows.Forms;

namespace dBosque.Stub.Editor.Controls.Behaviours
{
    /// <summary>
    /// Timer event on a control that is enabled/disabled depending on the focus of the control
    /// </summary>
    public class PeriodicEventFocusBehaviour : PeriodicEventBehaviour
    {

        /// <summary>
        /// Changed event, invoked once the timer is enabled/disabled.
        /// </summary>
        private readonly Action _changed;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="c">The control to act upon</param>
        /// <param name="emit">The event to invoke on a periodic basis</param>
        /// <param name="changed">The event to invoke when the focus is lost/received</param>
        /// <param name="i">The time period</param>
        public PeriodicEventFocusBehaviour(Control c, Action emit, Action changed, int i = 1000)
            : base(c, emit, i)
        {
            _changed = changed;
            _child.Leave += _child_Leave;
            _child.Enter += _child_Enter;

        }

        /// <summary>
        /// Enter the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _child_Enter(object sender, EventArgs e)
        {
            Enabled = false;
            _changed?.Invoke();
        }

        /// <summary>
        /// Leave the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _child_Leave(object sender, EventArgs e)
        {
            Enabled = true;
            _changed?.Invoke();
        }


    }
}
