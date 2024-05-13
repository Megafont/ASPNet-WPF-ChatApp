
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace ASPNet_WPF_ChatApp.Core.Logging
{
    /// <summary>
    /// Logs the messages to the Debug log pane in Visual Studio
    /// </summary>
    public class DebugLogger : ILogger
    {
        /// <summary>
        /// Logs the given message to the Visual Studio debug pane
        /// </summary>
        /// <remarks>
        /// 
        /// NOTE: The native Debug output has no color.
        ///       However if you install the VS extension VSColorOutput64 via the
        ///       Extensions menu in Visual Studio, then this logger will color
        ///       the outputs nicely
        ///
        ///       https://github.com/mike-ward/VSColorOutput
        /// 
        ///       This is extension was called VSColorOutput in older
        ///       versions of Visual Studio.
        /// </remarks>
        /// <param name="message">The message to log</param>
        /// <param name="level">The level of the message</param>
        public void Log(string message, LogLevel level)
        {
            // The default category
            var category = default(string);

            // Color debug output based on level
            switch (level)
            {
                // Debug
                case LogLevel.Debug:
                    category = "information";
                    break;

                // Verbose
                case LogLevel.Verbose:
                    category = "verbose"; // I changed the color of this by adding a new entry in the RegEx list in Tools->Options->VSColorOutput64, which outputs the message using custom color 3.
                    break;

                // Informative (I added this case to make the normal debug messages outputed by our program white, which I set as custom color 2 in Tools->Options->VSColorOutput64).
                case LogLevel.Informative:
                    category = "informative";
                    break;

                // Warning
                case LogLevel.Warning:
                    category = "warning";
                    break;

                // Error
                case LogLevel.Error:
                    category = "error";
                    break;

                // Success
                case LogLevel.Success:
                    category = "-----";
                    break;
            }


            // Write message to console. If the VSColorOutput64 extension is installed, this will color the output for certain message levels like error.
            Debug.WriteLine(message, category);


        }
    }
}
