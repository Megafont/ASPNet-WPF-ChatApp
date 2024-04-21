using System.Windows.Input;

namespace ASPNet_WPF_ChatApp.ViewModels.Base
{
    /// <summary>
    /// A basic command that runs an action
    /// </summary>
    class RelayParameterizedCommand : ICommand
    {
        #region Private Members

        private Action<object> _Action;

        #endregion

        #region Public Events

        /// <summary>
        /// The event that's fired when the <see cref="CanExecuteChanged(object)"/> value has changed
        /// </summary>
        public event EventHandler? CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public RelayParameterizedCommand(Action<object> action)
        {
            _Action = action;
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// A relay command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        /// <summary>
        /// Excutes this command's action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object? parameter)
        {
            _Action?.Invoke(parameter);
        }

        #endregion

    }
}
