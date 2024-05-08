using ASPNet_WPF_ChatApp.Controls.Input;
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
    /// The TextEntryWidthMatcherProperty attached property matches the label width of all <see cref="TextEntryControl"/> controls inside this panel
    /// </summary>
    public class TextEntryWidthMatcherProperty : BaseAttachedProperty<TextEntryWidthMatcherProperty, bool> 
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get the panel (grid typically)
            var panel = (sender as Panel);

            // Call SetWidths() initially. This also helps design time to show the right UI width.
            SetWidths(panel);

            // Wait for panel to load
            RoutedEventHandler onLoaded = null;
            onLoaded = (ss, ee) =>
            {
                // Unhook from the Loaded event
                panel.Loaded -= onLoaded;

                // Set widths
                SetWidths(panel);


                // Loop each child
                foreach (var child in panel.Children)
                {
                    // Ignore any non-TextEntry controls
                    if (!(child is TextEntryControl control))
                        continue;


                    // Update the widths if the size of the label has changed
                    control.Label.SizeChanged += (sss, eee) =>
                    {
                        // Update widths
                        SetWidths(panel);
                    };

                }
            };

            // Hook into the Loaded event
            panel.Loaded += onLoaded;
        }

        /// <summary>
        /// Update all child TextEntry controls so their widths match the largest width of the group
        /// </summary>
        /// <param name="panel">The panel containing the TextEntry controls</param>
        private void SetWidths(Panel panel)
        {
            // Keep track of the maximum width
            var maxSize = 0d;


            // Loop each child
            foreach (var child in panel.Children)
            {
                // Ignore any non-TextEntry controls
                if (!(child is TextEntryControl control))
                    continue;


                // Find if this value is larger than the other controls
                maxSize = Math.Max(maxSize, control.Label.RenderSize.Width + control.Label.Margin.Left + control.Label.Margin.Right);
            }


            // Create a grid length converter
            var gridLength = (GridLength) (new GridLengthConverter().ConvertFromString(maxSize.ToString()));


            // Loop each child
            foreach (var child in panel.Children)
            {
                // Ignore any non-TextEntry controls
                if (!(child is TextEntryControl control))
                    continue;


                // Set each control's LabelWidth value to the max size
                control.LabelWidth = gridLength;
            }
        }
    }
}
