using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Core.Expressions;

using PropertyChanged;

namespace ASPNet_WPF_ChatApp.Core.ViewModels.Base
{
    /// <summary>
    /// A base view model for any dialogs
    /// </summary>
    public class BaseDialogViewModel : BaseViewModel
    {
        /// <summary>
        /// The title of the dialog box
        /// </summary>
        public string Title { get; set; }
    }
}