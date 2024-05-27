using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ASPNet_WPF_ChatApp.ViewModels.Input;

namespace ASPNet_WPF_ChatApp.Controls.Input
{
    /// <summary>
    /// Interaction logic for PasswordEntryControl.xaml
    /// </summary>
    public partial class PasswordEntryControl : UserControl
    {

        #region Dependency Properties

        /// <summary>
        /// The label width of the control
        /// </summary>
        public GridLength LabelWidth
        {
            get => (GridLength)GetValue(LabelWidthProperty);
            set => SetValue(LabelWidthProperty, value);
        }

        // We are using a DependencyProperty as the backing store for LabelWidth. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.Register("LabelWidth", typeof(GridLength), typeof(PasswordEntryControl), new PropertyMetadata(GridLength.Auto, LabelWidthChangedCallback));

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public PasswordEntryControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Dependency Callbacks

        /// <summary>
        /// Called when the label width has changed
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        public static void LabelWidthChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                // Set the column definition width to the new value
                (d as PasswordEntryControl).LabelColumnDefinition.Width = (GridLength) e.NewValue;
            }
            catch (Exception ex)
            {
                // Make developer aware of potential issue
                Debugger.Break();

                (d as PasswordEntryControl).LabelColumnDefinition.Width = GridLength.Auto;
            }
        }

        #endregion


        /// <summary>
        /// Update the view model value with the new password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is PasswordEntryViewModel viewModel)
            {
                // Update the view model
                viewModel.CurrentPassword = CurrentPassword.SecurePassword;
            }
        }

        /// <summary>
        /// Update the view model value with the new password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is PasswordEntryViewModel viewModel)
            {
                // Update the view model
                viewModel.NewPassword = NewPassword.SecurePassword;
            }
        }

        /// <summary>
        /// Update the view model value with the new password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is PasswordEntryViewModel viewModel)
            {
                // Update the view model
                viewModel.ConfirmPassword = ConfirmPassword.SecurePassword;
            }
        }

    }
}
