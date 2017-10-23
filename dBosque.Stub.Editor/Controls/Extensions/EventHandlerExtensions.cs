using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBosque.Stub.Editor.Controls.Extensions
{
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// Invoke the eventhandler without any arguments
        /// </summary>
        /// <param name="handler">The handler to invoke</param>
        /// <param name="sender">The sender</param>
        public static void Invoke(this EventHandler handler, object sender = null)
        {
            handler?.Invoke(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Invoke the eventhandler with the given arguments
        /// </summary>
        /// <param name="handler">The handler to invoke</param>
        /// <param name="arg">The argument to pass</param>
        public static void Invoke<T>(this EventHandler<T> handler, T arg)
        {
            handler?.Invoke(null, arg);
        }

        /// <summary>
        /// Invoke the eventhandler with the given arguments
        /// </summary>
        /// <param name="handler">The handler to invoke</param>
        /// <param name="arg">The argument to pass</param>
        /// <param name="sender">The sender</param>
        public static void SafeInvoke<T>(this EventHandler<T> handler, object sender, T arg)
        {
            handler?.Invoke(sender, arg);
        }
    }
}
