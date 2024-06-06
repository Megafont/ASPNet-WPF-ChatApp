using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shell;

using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.ViewModels.Base;
using ASPNet_WPF_ChatApp.WindowUtils;


namespace ASPNet_WPF_ChatApp.WPFViewModels
{
    /// <summary>
    /// The View Model for a dialog window
    /// </summary>
    public class DialogWindowViewModel : WindowViewModel
    {
        #region Public Properties

        /// <summary>
        /// The title of this dialog window
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The content to host inside the dialog
        /// </summary>
        public Control Content { get; set; }

        #endregion

        #region Constructors

        public DialogWindowViewModel(Window window)
            : base(window)
        {
            // Make minimum size smaller than WindowViewModel's default
            WindowMinimumWidth = 250;
            WindowMinimumHeight = 100;

            // Make title bar smaller than WindowViewModel's default
            TitleHeight = 30;
        }

        #endregion    
    }

}
