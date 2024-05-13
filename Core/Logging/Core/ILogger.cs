using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Core.InversionOfControl.Interfaces;


namespace ASPNet_WPF_ChatApp.Core.Logging
{
    /// <summary>
    /// A logger that will handle log messages from a <see cref="ILogFactory"/>
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Handles the log message being passed in
        /// </summary>
        /// <param name="message">The message being logged</param>
        /// <param name="level">The level of the log message</param>
        void Log(string message, LogLevel level);
    }
}
