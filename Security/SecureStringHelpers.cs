using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Security;

namespace ASPNet_WPF_ChatApp.Security
{
    /// <summary>
    /// Helpers for the <see cref="SecureString"/> class
    /// </summary>
    public static class SecureStringHelpers
    {
        /// <summary>
        /// Unsecures a <see cref="SecureString"/> to plain text
        /// </summary>
        /// <param name="secureString">The secure string</param>
        /// <returns></returns>
        public static string Unsecure(this SecureString secureString)
        {
            // Make sure we have a secure string
            if (secureString == null)
                return string.Empty;

            // Get a pointer for an unsecure string in memory
            var unmanagedString = IntPtr.Zero;

            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            catch
            {
                // Clean up any memory allocation
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);

                return string.Empty;
            }
        }
    }
}
