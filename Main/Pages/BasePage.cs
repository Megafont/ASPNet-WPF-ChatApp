using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using ASPNet_WPF_ChatApp.Animations;
using ASPNet_WPF_ChatApp.Core.ViewModels.Base;

namespace ASPNet_WPF_ChatApp.Pages
{
    /// <summary>
    /// The base page for all pages to gain base functionality
    /// </summary>
    public class BasePage : UserControl
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
        public float SlideSeconds { get; set; } = 0.4f;

        /// <summary>
        /// A flag to indicate whether this page should animate out on load
        /// Useful for when we are moving the page to another frame
        /// </summary>
        public bool ShouldAnimateOut { get; set; }

        #endregion

        #region Constructors

        public BasePage()
        {
            // Don't bother animating in design time
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            // If we are animating in, start out hidden
            if (PageLoadAnimation != PageAnimationTypes.None)
                Visibility = Visibility.Collapsed;

            // Listen for page loading
            Loaded += BasePage_LoadedAsync;
        }

        #endregion

        #region Animation Load / Unload

        /// <summary>
        /// Once the page is loaded, perform any required animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BasePage_LoadedAsync(object sender, RoutedEventArgs e)
        {
            // If we are set up to animate out on load...
            if (ShouldAnimateOut)
            {
                // Animate the page out of view
                await AnimateOutAsync();
            }
            else
            {
                // Animate the page into view
                await AnimateInAsync();
            }
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
                    await this.SlideAndFadeInAsync(AnimationSlideDirections.Left, false, SlideSeconds, size: (int) Application.Current.MainWindow.Width);
                    break;
                case PageAnimationTypes.SlideAndFadeInFromRight:
                    // Start the animation
                    await this.SlideAndFadeInAsync(AnimationSlideDirections.Right, false, SlideSeconds, size: (int)Application.Current.MainWindow.Width);
                    break;
                case PageAnimationTypes.SlideAndFadeInFromBottom:
                    // Start the animation
                    await this.SlideAndFadeInAsync(AnimationSlideDirections.Bottom, false, SlideSeconds, size: (int)Application.Current.MainWindow.Height);
                    break;
                case PageAnimationTypes.SlideAndFadeInFromTop:
                    // Start the animation
                    await this.SlideAndFadeInAsync(AnimationSlideDirections.Top, false, SlideSeconds, size: (int)Application.Current.MainWindow.Height);
                    break;
                case PageAnimationTypes.FadeIn:
                    // Start the animation
                    await this.FadeInAsync(false, SlideSeconds);
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
                    await this.SlideAndFadeOutAsync(AnimationSlideDirections.Left, SlideSeconds, size: (int)Application.Current.MainWindow.Width);
                    break;
                case PageAnimationTypes.SlideAndFadeOutToRight:
                    // Start the animation
                    await this.SlideAndFadeOutAsync(AnimationSlideDirections.Right, SlideSeconds, size: (int)Application.Current.MainWindow.Width);
                    break;
                case PageAnimationTypes.SlideAndFadeOutToBottom:
                    // Start the animation
                    await this.SlideAndFadeOutAsync(AnimationSlideDirections.Bottom, SlideSeconds, size: (int)Application.Current.MainWindow.Height);
                    break;
                case PageAnimationTypes.SlideAndFadeOutToTop:
                    // Start the animation
                    await this.SlideAndFadeOutAsync(AnimationSlideDirections.Top, SlideSeconds, size: (int)Application.Current.MainWindow.Height);
                    break;
                case PageAnimationTypes.FadeOut:
                    // Start the animation
                    await this.FadeOutAsync(SlideSeconds);
                    break;
            }
        }

        #endregion

    }


    /// <summary>
    /// A base page with added ViewModel support
    /// </summary>
    /// <typeparam name="VM"></typeparam>
    public class BasePage<VM> : BasePage
        where VM: BaseViewModel, new()
    {
        #region Private Members

        /// <summary>
        /// The View Model associated with this page
        /// </summary>
        private VM _ViewModel;

        #endregion

        #region Public Properties

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
            : base()
        {
            // Create a default view model
            ViewModel = new VM();
        }

        #endregion

    }
}
