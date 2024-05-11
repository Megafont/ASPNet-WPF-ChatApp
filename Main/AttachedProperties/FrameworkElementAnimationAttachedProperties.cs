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
        /// True if this is the very first time the value has been updated
        /// Used to make sure we run the logic at least once during first load
        /// </summary>
        protected Dictionary<DependencyObject, bool> _AlreadyLoaded = new Dictionary<DependencyObject, bool>();

        /// <summary>
        /// The most recent value used if we get a value changed before we do the first load
        /// </summary>
        protected Dictionary<DependencyObject, bool> _FirstLoadValue = new Dictionary<DependencyObject, bool>();

        #endregion

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            // Get the framework element (WPF UI element)
            if (!(sender is FrameworkElement element))
                return;

            // Don't fire if the value didn't change
            if ((bool) sender.GetValue(ValueProperty) == (bool) value && _AlreadyLoaded.ContainsKey(sender))
                return;

            // On first load...
            if (!_AlreadyLoaded.ContainsKey(sender))
            {
                // Flag that we are in first load, but have not finished it
                _AlreadyLoaded[sender] = false;

                // Start off hidden before we decide how to animate
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
                    DoAnimation(element,
                                     _FirstLoadValue.ContainsKey(sender) ? _FirstLoadValue[sender] : (bool)value,
                                     true);

                    // Flag that we have finished the first load
                    _AlreadyLoaded[sender] = true;
                };

                // Hook into the Loaded event of the element
                element.Loaded += onLoaded;
            }

            // If we have started the first load but not fired the animation yet, update the property
            else if (_AlreadyLoaded[sender] == false)
            {
                _FirstLoadValue[sender] = (bool)value;
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
    /// Animates a framework element (WPF UI element) sliding up from the bottom on load
    /// if the value is true
    /// </summary>
    public class AnimateSlideInFromBottomOnLoadProperty : AnimateBaseProperty<AnimateSlideInFromBottomOnLoadProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            // Animate in
            await element.SlideAndFadeInAsync(AnimationSlideDirections.Bottom,
                                                !value,
                                                !value ? 0 : 0.3f,
                                                keepMargin: false);
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
