using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ASPNet_WPF_ChatApp.AttachedProperties
{
    /// <summary>
    /// The PanelChildMarginProperty attached property for creating a <see cref="Panel"/> (typically a grid) that has the specified margin
    /// value on all of its children.
    /// </summary>
    public class PanelChildMarginProperty : BaseAttachedProperty<PanelChildMarginProperty, string> 
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get the panel (grid typically)
            var panel = (sender as Panel);

            // Wait for panel to load
            panel.Loaded += (ss, ee) =>
            {
                // Loop each child
                foreach (var child in panel.Children)
                {
                    // Set its margin to the given value
                    (child as FrameworkElement).Margin = (Thickness)(new ThicknessConverter().ConvertFromString(e.NewValue as string));
                }
            };
        }
    }
}
