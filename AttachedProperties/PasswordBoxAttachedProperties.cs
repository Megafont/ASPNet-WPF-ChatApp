using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ASPNet_WPF_ChatApp.AttachedProperties
{
    /// <summary>
    /// The MonitorPassword property for a <see cref="PasswordBox"/>
    /// </summary>
    public class MonitorPasswordProperty : BaseAttachedProperty<MonitorPasswordProperty, bool> 
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get the caller
            var passwordBox = sender as PasswordBox;

            // Make sure it is a password box
            if (passwordBox == null)
                return;


            // Remove any previous event handlers
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

            // If the caller set MonitorPassword to true...
            if ((bool)e.NewValue)
            {
                // Set default value
                HasTextProperty.SetValue(passwordBox);

                // Start listening for password changes
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        /// <summary>
        /// Fired when the password box password value changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Set the attached HasText value
            HasTextProperty.SetValue((PasswordBox) sender);
        }
    }


    /// <summary>
    /// The HasText property for a <see cref="PasswordBox"/>
    /// </summary>
    public class HasTextProperty : BaseAttachedProperty<HasTextProperty, bool> 
    { 
        /// <summary>
        /// Sets the HasText property based on if the caller <cref="PasswordBox"/> has any text
        /// </summary>
        /// <param name="sender"></param>
        public static void SetValue(DependencyObject sender)
        {
            HasTextProperty.SetValue(sender, ((PasswordBox) sender).SecurePassword.Length > 0);
        }
    }
}
