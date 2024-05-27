using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using ASPNet_WPF_ChatApp.ViewModels.Base;

namespace ASPNet_WPF_ChatApp.ViewModels.Input
{
    /// <summary>
    /// The view model for a text entry field to edit a string value
    /// </summary>
    public class TextEntryViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The label to identity what this value is for
        /// </summary>
        public string Label { get; set; }
        
        /// <summary>
        /// The current saved value
        /// </summary>
        public string OriginalText { get; set; }

        /// <summary>
        /// The currrent edited text that has not been submited yet.
        /// </summary>
        public string EditedText { get; set; }

        /// <summary>
        /// Indicates if the current text is in edit mode
        /// </summary>
        public bool Editing { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// Puts the control into edit mode
        /// </summary>
        public ICommand EditCommand { get; set; }

        /// <summary>
        /// Cancels out of edit mode
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Commits the changes and saves the value
        /// as well as goes back to non-edit mode
        /// </summary>
        public ICommand SaveCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public TextEntryViewModel()
        {
            // Create commands
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Puts the control into edit mode
        /// </summary>
        private void Edit()
        {
            // Set the edited text to the current value
            EditedText = OriginalText;

            // Go into edit mode
            Editing = true;
        }

        /// <summary>
        /// Cancels out of edit mode
        /// </summary>
        private void Cancel()
        {
            Editing = false;
        }

        /// <summary>
        /// Commits the changes and saves the value
        /// as well as goes back to non-edit mode
        /// </summary>
        private void Save()
        {
            // TODO: Save content
            OriginalText = EditedText;

            Editing = false;
        }

        #endregion

    }
}
