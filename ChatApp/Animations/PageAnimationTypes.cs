using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_WPF_ChatApp.Animations
{
    /// <summary>
    /// Styles of page animation for appearing/disappearing
    /// </summary>
    public enum PageAnimationTypes
    {
        None = 0,                   // No animation

        SlideAndFadeInFromLeft,     // The page slides in and fades in from the left
        SlideAndFadeOutToLeft,      // The page slides out and fades out to the left

        SlideAndFadeInFromRight,    // The page slides in and fades in from the right
        SlideAndFadeOutToRight,     // The page slides out and fades out to the right

        SlideAndFadeInFromBottom,   // The page slides in and fades in from the bottom
        SlideAndFadeOutToBottom,    // The page slides out and fades out to the bottom

        SlideAndFadeInFromTop,      // The page slides in and fades in from the top
        SlideAndFadeOutToTop,       // The page slides out and fades out to the top

        FadeIn,                     // The page fades in
        FadeOut,                    // The page fades out
    }
}
