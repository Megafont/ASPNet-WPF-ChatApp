using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.ViewModels.Base;
using ASPNet_WPF_ChatApp.ViewModels.Input;

namespace ASPNet_WPF_ChatApp.ViewModels.Input.Design
{
    /// <summary>
    /// The design-time data for a <see cref="PasswordEntryViewModel"/>
    /// </summary>
    public class PasswordEntryDesignViewModel : PasswordEntryViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of this design model
        /// </summary>
        public static PasswordEntryDesignViewModel Instance => new PasswordEntryDesignViewModel();

        #endregion

        #region Constructors

        public PasswordEntryDesignViewModel()
        {
            Label = "Name";
            FakePassword = "********";
        }

        #endregion

    }
}
