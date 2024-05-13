using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Core.Async;
using ASPNet_WPF_ChatApp.Core.InversionOfControl.Base;
using ASPNet_WPF_ChatApp.Core.InversionOfControl.Interfaces;

namespace ASPNet_WPF_ChatApp.Core.FileSystem
{
    /// <summary>
    /// Handles reading, writing, and querying the file system
    /// </summary>
    public class FileManager : IFileManager
    {
        /// <summary>
        /// Writes the text to the specified file
        /// </summary>
        /// <param name="text">The text to write</param>
        /// <param name="filePath">The path of the file to write to</param>
        /// <param name="append">If true, appends the text to the end of the file, otherwise overwrites the file if it already exists</param>
        /// <returns></returns>
        public async Task WriteTextToFileAsync(string text, string filePath, bool append = false)
        {
            // TODO: Add exception catching

            // Normalize path
            filePath = NormalizePath(filePath);

            // Resolve to absolute path
            filePath = ResolvePath(filePath);

            // Lock the task so only one thread can access this file at a time
            await AsyncAwaiter.AwaitAsync(nameof(FileManager) + filePath,
                                          async () =>
                                          {
                                              // TODO: Add IoC.Task.Log logger on failure

                                              // Run the synchronous file access as a new task
                                              await IoC.TaskManager.Run(() =>
                                              {
                                                  // Write the text to file
                                                  using (var fileStream = (TextWriter)new StreamWriter(File.Open(filePath,
                                                                                                       append ? FileMode.Append : FileMode.Create)))
                                                  {
                                                      fileStream.Write(text);
                                                  }
                                              });
                                          });
        }

        /// <summary>
        /// Normalizes a path based on the current operating system
        /// </summary>
        /// <param name="path">The path to normalize</param>
        /// <returns></returns>
        public string NormalizePath(string path)
        {
            // If on Windows...
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                // Replace any / with \
                return path?.Replace('/', '\\').Trim();
            // If on Linux/Mac
            else
                // Replace any \ with /
                return path?.Replace('\\', '/').Trim();
        }

        /// <summary>
        /// Resolves any relative elements of the path to absolute
        /// </summary>
        /// <param name="path">The path to resolve</param>
        /// <returns></returns>
        public string ResolvePath(string path)
        {
            // Resolve the path to absolute
            return Path.GetFullPath(path);
        }
    }
}
