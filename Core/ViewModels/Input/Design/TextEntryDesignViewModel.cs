using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASPNet_WPF_ChatApp.Core.ViewModels.Base;
using ASPNet_WPF_ChatApp.Core.ViewModels.Input;

namespace ASPNet_WPF_ChatApp.Core.ViewModels.Input.Design
{
    /// <summary>
    /// The design-time data for a <see cref="TextEntryViewModel"/>
    /// </summary>
    public class TextEntryDesignViewModel : TextEntryViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of this design model
        /// </summary>
        public static TextEntryDesignViewModel Instance => new TextEntryDesignViewModel();

        #endregion

        #region Constructors

        public TextEntryDesignViewModel()
        {
            Label = "Name";
            OriginalText = "Michael Fontanini";
            EditedText = "Editing :)";
        }

        #endregion

    }
}
