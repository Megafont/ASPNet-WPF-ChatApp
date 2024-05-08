using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Core.ViewModels.Application;
using ASPNet_WPF_ChatApp.Core.ViewModels.Input;

namespace ASPNet_WPF_ChatApp.Core.ViewModels.Application.Design
{
    /// <summary>
    /// The design-time data for a <see cref="SettingsDesignViewModel"/>
    /// </summary>
    public class SettingsDesignViewModel : SettingsViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of this design model
        /// </summary>
        public static SettingsDesignViewModel Instance => new SettingsDesignViewModel();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsDesignViewModel()
        {
            Name = new TextEntryViewModel { Label = "Name", OriginalText = "Michael Fontanini" };
            Username = new TextEntryViewModel { Label = "Username", OriginalText = "Megafont" };
            Password = new TextEntryViewModel { Label = "Password", OriginalText = "**********" };
            Email = new TextEntryViewModel { Label = "Email", OriginalText = "megafont@gmail.com" };
        }

        #endregion
    }
}
