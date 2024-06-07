using ASPNet_WPF_ChatApp.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        /// True if this is the very first time the value has been updated
        /// Used to make sure we run the logic at least once during first load
        /// </summary>
        protected Dictionary<WeakReference, bool> _AlreadyLoaded = new Dictionary<WeakReference, bool>();

        /// <summary>
        /// The most recent value used if we get a value changed before we do the first load
        /// </summary>
        protected Dictionary<WeakReference, bool> _FirstLoadValue = new Dictionary<WeakReference, bool>();

        #endregion

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            // Get the framework element (WPF UI element)
            if (!(sender is FrameworkElement element))
                return;

            // Try and get the already loaded reference
            var alreadyLoadedReference = _AlreadyLoaded.FirstOrDefault(f => f.Key.Target == sender);

            // Try and get the first load reference
            var firstLoadReference = _FirstLoadValue.FirstOrDefault(f => f.Key.Target == sender);

            // Don't fire if the value didn't change
            if ((bool) sender.GetValue(ValueProperty) == (bool) value && alreadyLoadedReference.Key != null)
                return;

            // On first load...
            if (alreadyLoadedReference.Key == null)
            {
                // Create weak reference
                var weakReference = new WeakReference(sender);

                // Flag that we are in first load, but have not finished it
                _AlreadyLoaded[weakReference] = false;

                // Start off hidden before we decide how to animate
                element.Visibility = Visibility.Hidden;

                // Create a single self-unhookable event
                // for the element's Loaded event
                RoutedEventHandler onLoaded = null;
                onLoaded = async (s, e) =>
                {
                    // Unhook the event
                    element.Loaded -= onLoaded;

                    // Slight delay after load is needed for some elements to get laid out
                    // and their width/heights correctly calculated
                    await Task.Delay(5);

                    // Refresh the first load value in case it changed
                    // since the 5ms delay
                    firstLoadReference = _FirstLoadValue.FirstOrDefault(f => f.Key.Target == sender);

                    // Do desired animation
                    DoAnimation(element,
                                firstLoadReference.Key != null ? firstLoadReference.Value : (bool)value,
                                true);

                    // Flag that we have finished the first load
                    _AlreadyLoaded[weakReference] = true;
                };

                // Hook into the Loaded event of the element
                element.Loaded += onLoaded;
            }

            // If we have started the first load but not fired the animation yet, update the property
            else if (alreadyLoadedReference.Value == false)
            {
                _FirstLoadValue[new WeakReference(sender)] = (bool)value;
            }
            else
            { 
                // Do desired animation
                DoAnimation(element, 
                            (bool)value, 
                            false);
            }
        }

        /// <summary>
        /// The animation method that is fired when the value changes
        /// </summary>
        /// <param name="element">The UI eleement</param>
        /// <param name="value">The new value</param>
        /// <param name="firstLoad">Indicates if this is happening when the app first loads</param>
        protected virtual void DoAnimation(FrameworkElement element, bool value, bool firstLoad) { }

    }


    /// <summary>
    /// Fades in an image when the source changes
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public class FadeInImageOnLoadProperty : AnimateBaseProperty<FadeInImageOnLoadProperty>
    {
        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            // Make sure we have an image
            if (!(sender is Image image))
                return;

            // If we want to animate in...
            if ((bool)value)
                // Listen for target change
                image.TargetUpdated += Image_TargetUpdatedAsync;
            // Otherwise
            else
                // Make sure we unhooked
                image.TargetUpdated -= Image_TargetUpdatedAsync;
        }

        private async void Image_TargetUpdatedAsync(object? sender, DataTransferEventArgs e)
        {
            // Fade in image
            await (sender as Image).FadeInAsync(false);
        }
    }

    /// <summary>
    /// Animates a framework element (WPF UI element) sliding it in from the left on show
    /// and sliding out to the left on hide
    /// </summary>
    public class AnimateSlideInFromLeftProperty : AnimateBaseProperty<AnimateSlideInFromLeftProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
            {
                // Animate in
                await element.SlideAndFadeInAsync(AnimationSlideDirections.Left,
                                                  firstLoad,
                                                  firstLoad ? 0 : 0.3f, 
                                                  keepMargin: false);
            }
            else
            {
                // Animate out
                await element.SlideAndFadeOutAsync(AnimationSlideDirections.Left,
                                                   firstLoad ? 0 : 0.3f, 
                                                   keepMargin: false);
            }
        }
    }

    /// <summary>
    /// Animates a framework element (WPF UI element) sliding it in from the left on show
    /// and sliding out to the left on hide
    /// </summary>
    public class AnimateSlideInFromLeft_KeepMargin_Property : AnimateBaseProperty<AnimateSlideInFromLeft_KeepMargin_Property>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
            {
                // Animate in
                await element.SlideAndFadeInAsync(AnimationSlideDirections.Left,
                                                  firstLoad,
                                                  firstLoad ? 0 : 0.3f,
                                                  keepMargin: true);
            }
            else
            {
                // Animate out
                await element.SlideAndFadeOutAsync(AnimationSlideDirections.Left,
                                                   firstLoad ? 0 : 0.3f,
                                                   keepMargin: true);
            }
        }
    }

    /// <summary>
    /// Animates a framework element (WPF UI element) sliding it in from the right on show
    /// and sliding out to the right on hide
    /// </summary>
    public class AnimateSlideInFromRightProperty : AnimateBaseProperty<AnimateSlideInFromRightProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
            {
                // Animate in
                await element.SlideAndFadeInAsync(AnimationSlideDirections.Right, 
                                                  firstLoad,
                                                  firstLoad ? 0 : 0.3f, 
                                                  keepMargin: false);
            }
            else
            {
                // Animate out
                await element.SlideAndFadeOutAsync(AnimationSlideDirections.Right, 
                                                   firstLoad ? 0 : 0.3f, 
                                                   keepMargin: false);
            }
        }
    }

    /// <summary>
    /// Animates a framework element (WPF UI element) sliding it in from the right on show
    /// and sliding out to the right on hide
    /// </summary>
    public class AnimateSlideInFromRight_KeepMargin_Property : AnimateBaseProperty<AnimateSlideInFromRight_KeepMargin_Property>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
            {
                // Animate in
                await element.SlideAndFadeInAsync(AnimationSlideDirections.Right,
                                                  firstLoad,
                                                  firstLoad ? 0 : 0.3f,
                                                  keepMargin: true);
            }
            else
            {
                // Animate out
                await element.SlideAndFadeOutAsync(AnimationSlideDirections.Right,
                                                   firstLoad ? 0 : 0.3f,
                                                   keepMargin: true);
            }
        }
    }

    /// <summary>
    /// Animates a framework element (WPF UI element) sliding up from the bottom on show
    /// and sliding out to the bottom on hide
    /// </summary>
    public class AnimateSlideInFromBottomProperty : AnimateBaseProperty<AnimateSlideInFromBottomProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
            {
                // Animate in
                await element.SlideAndFadeInAsync(AnimationSlideDirections.Bottom, 
                                                  firstLoad,
                                                  firstLoad ? 0 : 0.3f, 
                                                  keepMargin: false);
            }
            else
            {
                // Animate out
                await element.SlideAndFadeOutAsync(AnimationSlideDirections.Bottom, 
                                                   firstLoad ? 0 : 0.3f, 
                                                   keepMargin: false);
            }
        }
    }

    /// <summary>
    /// Animates a framework element (WPF UI element) sliding up from the bottom on show
    /// and sliding out to the bottom on hide, while keeping the margin
    /// </summary>
    public class AnimateSlideInFromBottom_KeepMargin_Property : AnimateBaseProperty<AnimateSlideInFromBottom_KeepMargin_Property>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
            {
                // Animate in
                await element.SlideAndFadeInAsync(AnimationSlideDirections.Bottom,
                                                  firstLoad,
                                                  firstLoad ? 0 : 0.3f,
                                                  keepMargin: true);
            }
            else
            {
                // Animate out
                await element.SlideAndFadeOutAsync(AnimationSlideDirections.Bottom,
                                                   firstLoad ? 0 : 0.3f,
                                                   keepMargin: true);
            }

        }
    }

    /// <summary>
    /// Animates a framework element sliding up from the bottom on load
    /// if the value is true
    /// </summary>
    public class AnimateSlideInFromBottomOnLoadProperty : AnimateBaseProperty<AnimateSlideInFromBottomOnLoadProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            // Animate in
            await element.SlideAndFadeInAsync(AnimationSlideDirections.Bottom, !value, !value ? 0 : 0.3f, keepMargin: false);
        }
    }

    /// <summary>
    /// Animates a framework element (WPF UI element) sliding down from the top on show
    /// and sliding out to the top on hide
    /// </summary>
    public class AnimateSlideInFromTopProperty : AnimateBaseProperty<AnimateSlideInFromTopProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
            {
                // Animate in
                await element.SlideAndFadeInAsync(AnimationSlideDirections.Top, 
                                                  firstLoad,
                                                  firstLoad ? 0 : 0.3f, 
                                                  keepMargin: false);
            }
            else
            {
                // Animate out
                await element.SlideAndFadeOutAsync(AnimationSlideDirections.Top, 
                                                   firstLoad ? 0 : 0.3f, 
                                                   keepMargin: false);
            }
        }
    }

    /// <summary>
    /// Animates a framework element (WPF UI element) sliding down from the top on show
    /// and sliding out to the top on hide
    /// </summary>
    public class AnimateSlideInFromTop_KeepMargin_Property : AnimateBaseProperty<AnimateSlideInFromTop_KeepMargin_Property>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
            {
                // Animate in
                await element.SlideAndFadeInAsync(AnimationSlideDirections.Top,
                                                  firstLoad,
                                                  firstLoad ? 0 : 0.3f,
                                                  keepMargin: true);
            }
            else
            {
                // Animate out
                await element.SlideAndFadeOutAsync(AnimationSlideDirections.Top,
                                                   firstLoad ? 0 : 0.3f,
                                                   keepMargin: true);
            }
        }
    }

    /// <summary>
    /// Animates a framework element (WPF UI element) fading in on show
    /// and fading out on hide
    /// </summary>
    public class AnimateFadeInProperty : AnimateBaseProperty<AnimateFadeInProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
            {
                // Animate in
                await element.FadeInAsync(firstLoad,
                                          firstLoad ? 0 : 0.3f);
            }
            else
            {
                // Animate out
                await element.FadeOutAsync(firstLoad ? 0 : 0.3f);
            }
        }

    }

    /// <summary>
    /// Animates a framework element sliding it from right to left and repeating forever
    /// </summary>
    public class AnimateMarqueeProperty : AnimateBaseProperty<AnimateMarqueeProperty>
    {
        protected override void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            // Animate in
            element.MarqueeAsync(firstLoad ? 0 : 3f);
        }
    }

}
