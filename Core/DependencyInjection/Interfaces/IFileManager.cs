using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_WPF_ChatApp.Core.DependencyInjection.Interfaces
{
    /// <summary>
    /// Handles reading, writing, and querying the file system
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Writes the text to the specified file
        /// </summary>
        /// <param name="text">The text to write</param>
        /// <param name="filePath">The path of the file to write to</param>
        /// <param name="append">If true, appends the text to the end of the file, otherwise overwrites the file if it already exists</param>
        /// <returns></returns>
        Task WriteTextToFileAsync(string text, string filePath, bool append = false);

        /// <summary>
        /// Normalizes a path based on the current operating system
        /// </summary>
        /// <param name="path">The path to normalize</param>
        /// <returns></returns>
        string NormalizePath(string path);

        /// <summary>
        /// Resolves any relative elements of the path to absolute
        /// </summary>
        /// <param name="path">The path to resolve</param>
        /// <returns></returns>
        string ResolvePath(string path);
    }
}
