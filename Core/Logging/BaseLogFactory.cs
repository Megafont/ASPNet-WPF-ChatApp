using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Core.InversionOfControl.Interfaces;

namespace ASPNet_WPF_ChatApp.Core.Logging
{
    /// <summary>
    /// The standard log factory for the ASPNet_WPF_ChatApp
    /// Logs details to the console by default
    /// </summary>
    public class BaseLogFactory : ILogFactory
    {
        #region Protected Methods

        /// <summary>
        /// The list of loggers in this factory
        /// </summary>
        protected List<ILogger> _Loggers = new List<ILogger>();

        /// <summary>
        /// A lock for the logger list to keep it thread-safe
        /// </summary>
        protected object _LoggersLock = new object();

        #endregion

        #region Public Properties

        /// <summary>
        /// Fires whenever a new log arrives
        /// </summary>
        public LogOutputLevel LogOutputLevel { get; set; }

        /// <summary>
        /// If true, includes the origin of where the log message was logged from
        /// such as the class name, line number, and file name
        /// </summary>
        public bool IncludeLogOriginDetails { get; set; } = true;

        #endregion


        #region Events

        /// <summary>
        /// The level of logging to output.
        /// </summary>
        /// <remarks>
        /// The parentheses are added inside the <> of the Action to allow us to give the parameters names.
        /// This effectively is creating a tuple, which is then used as the type for the action.
        /// </remarks>
        public event Action<(string Message, LogLevel Level)> NewLog = (details) => { };

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseLogFactory()
        {
            // Add the console logger by default
            AddLogger(new DebugLogger());
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a specific logger to this factory
        /// </summary>
        /// <param name="logger">The logger</param>
        public void AddLogger(ILogger logger)
        {
            // Lock the list so it is thread-safe
            lock (_LoggersLock)
            {
                /// If the logger is not already in the list...
                if (!_Loggers.Contains(logger))
                {
                    // Add the logger to the list
                    _Loggers.Add(logger);
                }
            }
        }

        /// <summary>
        /// Removes the specified logger from this factory
        /// </summary>
        /// <param name="logger">The logger</param>
        public void RemoveLogger(ILogger logger)
        {
            // Lock the list so it is thread-safe
            lock (_LoggersLock)
            {
                /// If the logger is already in the list...
                if (_Loggers.Contains(logger))
                {
                    // Remove the logger from the list
                    _Loggers.Remove(logger);
                }
            }
        }

        /// <summary>
        /// Logs a specific message to all loggers in this factory
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="level">The level of the message being logged</param>
        /// <param name="origin">The method/function this message was logged in</param>
        /// <param name="filePath">The code filename that this message logged from</param>
        /// <param name="lineNumber">The line of code in the file this message was logged from</param>
        public void Log(string message, 
                        LogLevel level = LogLevel.Informative, 
                        [CallerMemberName] string origin = "", 
                        [CallerFilePath] string filePath = "", 
                        [CallerLineNumber] int lineNumber = 0)
        {
            // If we should not log the message because the level is too low...
            if ((int)level < (int)LogOutputLevel)
                    return;

            // If the user wants to know where the log originated from...
            if (IncludeLogOriginDetails)
                message = $"{message} [{Path.GetFileName(filePath)} > {origin}() > Line {lineNumber}]";

            // Log to all loggers
            _Loggers.ForEach(logger => logger.Log(message, level));

            // Fire off the NewLog event, passing in an inline tuple.
            NewLog.Invoke((message, level));
        }

        #endregion    

    }
}
