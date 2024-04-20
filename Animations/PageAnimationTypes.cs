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
        None = 0,                       // No animation

        SlideAndFadeInFromRight = 1,    // The page slides in and fades in from the right
        SlideAndFadeOutToLeft = 2,      // The page slides out and fades out to the left
    }
}
