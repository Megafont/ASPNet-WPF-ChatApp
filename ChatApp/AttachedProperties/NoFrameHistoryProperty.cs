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
    /// The NoFrameHistory attached property for creating a <see cref="Frame"/> that never shows navigation
    /// and keeps the navigation history empty
    /// </summary>
    public class NoFrameHistoryProperty : BaseAttachedProperty<NoFrameHistoryProperty, bool> 
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get the frame
            var frame = (sender as Frame);

            if (frame == null)
            {
                Debug.WriteLine("NoFrameHistoryProperty.OnValueChanged():    ERROR!  The passed in sender is either not a frame or is null!");
                return;
            }

            // Hide the navigation bar
            frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;

            // Clear history on navigate
            frame.Navigated += (ss, ee) => ((Frame) ss).NavigationService.RemoveBackEntry();
        }
    }
}
