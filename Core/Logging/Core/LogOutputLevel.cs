using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_WPF_ChatApp.Core.Logging
{
    /// <summary>
    /// The level of detail to output for a logger
    /// Lower levels log more information, so Debug logs everything, while Nothing logs nothing.
    /// </summary>
    public enum LogOutputLevel
    {
        /// <summary>
        /// Logs everything
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Logs all information except debug information
        /// </summary>
        Verbose = 2,

        /// <summary>
        /// Logs all informative messages, ignoring any debug and verbose messages.
        /// </summary>
        Informative = 3,

        /// <summary>
        /// Logs only critical errors and warnings
        /// </summary>
        Critical = 4,

        /// <summary>
        /// The logger will never output anything
        /// </summary>
        Nothing = 7,
    }
}
