using System.Windows;

using ASPNet_WPF_ChatApp.Animations;

namespace ASPNet_WPF_ChatApp.AttachedProperties
{
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
    
}
