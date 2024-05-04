using ASPNet_WPF_ChatApp.Core.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_WPF_ChatApp.Core.StyleConverters
{
    /// <summary>
    /// Helper functions for <see cref="IconTypes"/>
    /// </summary>
    /// <param name="type">The type to convert</param>
    public static class IconTypesExtensions
    {
        public static string ToFontAwesome(this IconTypes type)
        {
            // Return a FontAwesome string based on the icon type
            switch (type)
            {
                case IconTypes.File:
                    return "\uf0f6";
                case IconTypes.Picture:
                    return "\uf1c5";

                // If none found, return null
                default:
                    return null;
            }
        }
    }
}
