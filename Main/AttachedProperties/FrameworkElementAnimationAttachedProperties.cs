using ASPNet_WPF_ChatApp.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace ASPNet_WPF_ChatApp.AttachedProperties
{
    /// <summary>
    /// A base class to run any animation method when a boolean is set to true
    /// and a reverse animation when set to false.
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public abstract class AnimateBaseProperty<Parent> : BaseAttachedProperty<Parent, bool>
        where Parent : BaseAttachedProperty<Parent, bool>, new()
    {
        #region Protected Properties

        /// <summary>
        /// True if this is the very first time the value has been updated.
        /// Used to make sure we run the logic at least once during the first load.
        /// </summary>
        protected bool _FirstFire = true;

        #endregion

        #region Public Properties

        public bool FirstLoad { get; set; } = true;
        #endregion

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            // Get the framework element (WPF UI element)
            if (!(sender is FrameworkElement element))
                return;

            // Don't fire if the value didn't change
            if ((bool) sender.GetValue(ValueProperty) == (bool) value && !_FirstFire)
                return;

            // No longer first fire
            _FirstFire = false;

            // On first load...
            if (FirstLoad)
            {
                // Start off hidden before we decide how to animate
                // if we are to be animated out initially
                if (!(bool)value)
                    element.Visibility = Visibility.Hidden;

                // Create a single self-unhookable event
                // for the eleemnt's Loaded event
                RoutedEventHandler onLoaded = null;
                onLoaded = async (s, e) =>
                {
                    // Unhook the event
                    element.Loaded -= onLoaded;

                    // Slight delay after load is needed for some elements to get laid out
                    // and their width/heights correctly calculated
                    await Task.Delay(5);

                    // Do desired animation
                    DoAnimationAsync(element, (bool) value);

                    // No longer in first load
                    FirstLoad = false;
                };

                // Hook into the Loaded event of the element
                element.Loaded += onLoaded;
            }
            else
            {
                // Do desired animation
                DoAnimationAsync(element, (bool)value);
            }
        }

        /// <summary>
        /// The animation method that is fired when the value changes
        /// </summary>
        /// <param name="element">The UI eleement</param>
        /// <param name="value">The new value</param>
        protected virtual void DoAnimationAsync(FrameworkElement element, bool value) { }

    }

    /// <summary>
    /// Animates a framework element (WPF UI element) sliding it in from the left on show
    /// and sliding out to the left on hide
    /// </summary>
    public class AnimateSlideInFromLeftProperty : AnimateBaseProperty<AnimateSlideInFromLeftProperty>
    {
        protected override async void DoAnimationAsync(FrameworkElement element, bool value)
        {
            if (value)
            {
                // Animate in
                await element.SlideAndFadeInFromLeftAsync(FirstLoad ? 0 : 0.3f, false);
            }
            else
            {
                // Animate out
                await element.SlideAndFadeOutToLeftAsync(FirstLoad ? 0 : 0.3f, false);
            }
        }
    }

    /// <summary>
    /// Animates a framework element (WPF UI element) sliding it in from the right on show
    /// and sliding out to the right on hide
    /// </summary>
    public class AnimateSlideInFromRightProperty : AnimateBaseProperty<AnimateSlideInFromRightProperty>
    {
        protected override async void DoAnimationAsync(FrameworkElement element, bool value)
        {
            if (value)
            {
                // Animate in
                await element.SlideAndFadeInFromRightAsync(FirstLoad ? 0 : 0.3f, false);
            }
            else
            {
                // Animate out
                await element.SlideAndFadeOutToRightAsync(FirstLoad ? 0 : 0.3f, false);
            }
        }
    }

    /// <summary>
    /// Animates a framework element (WPF UI element) sliding up from the bottom on show
    /// and sliding out to the bottom on hide
    /// </summary>
    public class AnimateSlideInFromBottomProperty : AnimateBaseProperty<AnimateSlideInFromBottomProperty>
    {
        protected override async void DoAnimationAsync(FrameworkElement element, bool value)
        {
            if (value)
            {
                // Animate in
                await element.SlideAndFadeInFromBottomAsync(FirstLoad ? 0 : 0.3f, false);
            }
            else
            {
                // Animate out
                await element.SlideAndFadeOutToBottomAsync(FirstLoad ? 0 : 0.3f, false);
            }
        }
    }

    /// <summary>
    /// Animates a framework element (WPF UI element) sliding up from the bottom on show
    /// and sliding out to the bottom on hide, while keeping the margin
    /// </summary>
    public class AnimateSlideInFromBottom_KeepMargin_Property : AnimateBaseProperty<AnimateSlideInFromBottom_KeepMargin_Property>
    {
        protected override async void DoAnimationAsync(FrameworkElement element, bool value)
        {
            if (value)
            {
                // Animate in
                await element.SlideAndFadeInFromBottomAsync(FirstLoad ? 0 : 0.3f, true);
            }
            else
            {
                // Animate out
                await element.SlideAndFadeOutToBottomAsync(FirstLoad ? 0 : 0.3f, true);
            }
        }
    }

    /// <summary>
    /// Animates a framework element (WPF UI element) sliding down from the top on show
    /// and sliding out to the top on hide
    /// </summary>
    public class AnimateSlideInFromTopProperty : AnimateBaseProperty<AnimateSlideInFromTopProperty>
    {
        protected override async void DoAnimationAsync(FrameworkElement element, bool value)
        {
            if (value)
            {
                // Animate in
                await element.SlideAndFadeInFromTopAsync(FirstLoad ? 0 : 0.3f, false);
            }
            else
            {
                // Animate out
                await element.SlideAndFadeOutToTopAsync(FirstLoad ? 0 : 0.3f, false);
            }
        }
    }

    /// <summary>
    /// Animates a framework element (WPF UI element) fading in on show
    /// and fading out on hide
    /// </summary>
    public class AnimateFadeInProperty : AnimateBaseProperty<AnimateFadeInProperty>
    {
        protected override async void DoAnimationAsync(FrameworkElement element, bool value)
        {
            if (value)
            {
                // Animate in
                await element.FadeInAsync(FirstLoad ? 0 : 0.3f);
            }
            else
            {
                // Animate out
                await element.FadeOutAsync(FirstLoad ? 0 : 0.3f);
            }
        }
    }
}
