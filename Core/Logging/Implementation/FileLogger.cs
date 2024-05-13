using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Core.InversionOfControl.Base;
using Microsoft.VisualBasic;

namespace ASPNet_WPF_ChatApp.Core.Logging
{
    /// <summary>
    /// Logs to a specific file
    /// </summary>
    public class FileLogger : ILogger
    {
        #region Public Properties

        /// <summary>
        /// The path to write the log file to
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// If true, logs the current time with each message
        /// </summary>
        public bool LogTime { get; set; } = true;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="filePath"></param>
        public FileLogger(string filePath)
        {
            FilePath = filePath;
        }

        #endregion

        #region Logger Methods

        public void Log(string message, LogLevel level)
        {
            // Get current time
            var currentTime = DateTimeOffset.Now.ToString("yyyy-MM-dd hh:mm:ss");

            // Prepend the time to the log message if desired
            var timeStamp = LogTime ? $"[{currentTime}] " : "";

            // Write the message to the log file
            IoC.FileManager.WriteTextToFileAsync($"{timeStamp}{message}" + Environment.NewLine, FilePath, true);
        } 

        #endregion
    }
}
