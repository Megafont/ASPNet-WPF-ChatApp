using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

using ASPNet_WPF_ChatApp.Animations;

namespace ASPNet_WPF_ChatApp.Pages
{
    public class BasePage : Page
    {
        #region Public Properties

        /// <summary>
        /// The animation to play when the page is first loaded
        /// </summary>
        public PageAnimationTypes PageLoadAnimation { get; set; } = PageAnimationTypes.SlideAndFadeInFromRight;

        /// <summary>
        /// The animation to play when the page is unloaded
        /// </summary>
        public PageAnimationTypes PageUnloadAnimation { get; set; } = PageAnimationTypes.SlideAndFadeOutToLeft;

        /// <summary>
        /// The time any slide animation takes to complete
        /// </summary>
        public float SlideSeconds { get; set; } = 0.8f;

        #endregion

        #region Constructors

        public BasePage()
        {
            // If we are animating in, start out hidden
            if (this.PageLoadAnimation != PageAnimationTypes.None)
                this.Visibility = Visibility.Collapsed;

            // Listen for page loading
            this.Loaded += BasePage_Loaded;
        }

        #endregion

        #region Animation Load / Unload

        /// <summary>
        /// Once the page is loaded, perform any required animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BasePage_Loaded(object sender, EventArgs e)
        {
            // Animate the page into view
            await AnimateIn();
        }

        /// <summary>
        /// Animates thhe page into view
        /// </summary>
        /// <returns></returns>
        public async Task AnimateIn()
        {
            // Make sure we have something to do
            if (this.PageLoadAnimation == PageAnimationTypes.None)
            {
                return;
            }

            switch (this.PageLoadAnimation)
            {
                case PageAnimationTypes.SlideAndFadeInFromRight:
                    // Start the animation
                    await this.SlideAndFadeInFromRight(this.SlideSeconds);
                    break;
            }
        }

        /// <summary>
        /// Animates the page out of view
        /// </summary>
        /// <returns></returns>
        public async Task AnimateOut() 
        {
            // Make sure we have something to do
            if (this.PageUnloadAnimation == PageAnimationTypes.None)
            {
                return;
            }

            switch (this.PageUnloadAnimation)
            {
                case PageAnimationTypes.SlideAndFadeOutToLeft:
                    // Start the animation
                    await this.SlideAndFadeOutToTheLeft(this.SlideSeconds);
                    break;
            }
        }

        #endregion

    }
}
