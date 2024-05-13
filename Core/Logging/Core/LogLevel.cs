using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_WPF_ChatApp.Core.Logging
{
    /// <summary>
    /// The severity of the log message
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Developer-specific information
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Verbose information
        /// </summary>
        Verbose = 2,

        /// <summary>
        /// General information
        /// </summary>
        Informative = 3,

        /// <summary>
        /// A warning
        /// </summary>
        Warning = 4,

        /// <summary>
        /// An error
        /// </summary>
        Error = 5,

        /// <summary>
        /// A success
        /// </summary>
        Success = 6,
    }
}
