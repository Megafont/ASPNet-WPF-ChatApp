using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ASPNet_WPF_ChatApp.AttachedProperties
{
    /// <summary>
    /// Focuses (keyboard focus) this element on load
    /// </summary>
    public class IsFocusedProperty : BaseAttachedProperty<IsFocusedProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // If we don't have a control, return.
            if (!(sender is Control control))
                return;

            // Focus this control once loaded
            control.Loaded += (ss, ee) => control.Focus();
        }
    }

    /// <summary>
    /// Focuses (keyboard focus) this element if true
    /// </summary>
    public class FocusProperty : BaseAttachedProperty<FocusProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if ((bool)e.NewValue)
                {
                    // Focus this control
                    textBox.Focus();
                }
            }

            if (sender is PasswordBox passwordBox)
            {
                if ((bool)e.NewValue)
                {
                    // Focus this control
                    passwordBox.Focus();
                }
            }
        }
    }

    /// <summary>
    /// Focuses (keyboard focus) and selects all text in this element if true
    /// </summary>
    public class FocusAndSelectProperty : BaseAttachedProperty<FocusAndSelectProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if ((bool)e.NewValue)
                {
                    // Focus this control
                    textBox.Focus();

                    // Select all text
                    textBox.SelectAll();
                }
            }

            if (sender is PasswordBox passwordBox)
            {
                if ((bool)e.NewValue)
                {
                    // Focus this control
                    passwordBox.Focus();

                    // Select all text
                    passwordBox.SelectAll();
                }
            }
        }
    }

}
