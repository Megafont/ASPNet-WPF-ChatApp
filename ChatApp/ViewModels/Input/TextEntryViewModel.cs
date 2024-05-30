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

        /// <summary>
        /// Indicates if the current control has an update in progress
        /// </summary>
        public bool Working { get; set; }

        /// <summary>
        /// The action to run when saving the text
        /// Returns true if the commit was successful, or false otherwise
        /// </summary>
        public Func<Task<bool>> CommitAction { get; set; }

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
            // Store result of a commit call
            bool result = default(bool);

            // Save currently saved value
            var currentSavedValue = OriginalText;

            RunCommandAsync(() => Working, async () =>
            {
                // While working, come out 0of edit mode
                Editing = false;

                // Commit the changed text
                // so we can see it while it is working
                OriginalText = EditedText;

                // Try and do the work
                result = CommitAction == null ? true : await CommitAction();

            }).ContinueWith(t =>
            {
                // If we failed...
                if (!result)
                {
                    // Restore original value
                    OriginalText = currentSavedValue;

                    // Go back into edit mode
                    Editing = true;
                }
            });
        }

        #endregion

    }
}
