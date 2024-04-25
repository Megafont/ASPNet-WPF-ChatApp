using System.Windows;
using System.Windows.Controls;

using ASPNet_WPF_ChatApp.Animations;
using ASPNet_WPF_ChatApp.Core.ViewModels.Base;

namespace ASPNet_WPF_ChatApp.Pages
{
    public class BasePage<VM> : Page
        where VM: BaseViewModel, new()
    {
        #region Private Members

        /// <summary>
        /// The View Model associated with this page
        /// </summary>
        private VM _ViewModel;

        #endregion

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

        public VM ViewModel
        {
            get { return _ViewModel; }
            set
            {
                // If nothing has changed, return
                if (_ViewModel == value)
                    return;

                // Update the value
                _ViewModel = value;

                // Set the data context for this page
                DataContext = _ViewModel;
            }
        }

        #endregion

        #region Constructors

        public BasePage()
        {
            // If we are animating in, start out hidden
            if (PageLoadAnimation != PageAnimationTypes.None)
                Visibility = Visibility.Collapsed;

            // Listen for page loading
            Loaded += BasePage_LoadedAsync;

            // Create a default view model
            ViewModel = new VM();
        }

        #endregion

        #region Animation Load / Unload

        /// <summary>
        /// Once the page is loaded, perform any required animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BasePage_LoadedAsync(object sender, EventArgs e)
        {
            // Animate the page into view
            await AnimateInAsync();
        }

        /// <summary>
        /// Animates thhe page into view
        /// </summary>
        /// <returns></returns>
        public async Task AnimateInAsync()
        {
            // Make sure we have something to do
            if (PageLoadAnimation == PageAnimationTypes.None)
            {
                return;
            }

            switch (PageLoadAnimation)
            {
                case PageAnimationTypes.SlideAndFadeInFromLeft:
                    // Start the animation
                    await this.SlideAndFadeInFromLeftAsync(SlideSeconds);
                    break;
                case PageAnimationTypes.SlideAndFadeInFromRight:
                    // Start the animation
                    await this.SlideAndFadeInFromRightAsync(SlideSeconds);
                    break;
            }
        }

        /// <summary>
        /// Animates the page out of view
        /// </summary>
        /// <returns></returns>
        public async Task AnimateOutAsync() 
        {
            // Make sure we have something to do
            if (PageUnloadAnimation == PageAnimationTypes.None)
            {
                return;
            }

            switch (PageUnloadAnimation)
            {
                case PageAnimationTypes.SlideAndFadeOutToLeft:
                    // Start the animation
                    await this.SlideAndFadeOutToTheLeftAsync(SlideSeconds);
                    break;
                case PageAnimationTypes.SlideAndFadeOutToRight:
                    // Start the animation
                    await this.SlideAndFadeOutToTheRightAsync(SlideSeconds);
                    break;
            }
        }

        #endregion

    }
}
