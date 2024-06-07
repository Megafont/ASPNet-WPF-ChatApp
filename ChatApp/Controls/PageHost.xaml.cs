using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.DependencyInjection;
using ASPNet_WPF_ChatApp.Pages;
using ASPNet_WPF_ChatApp.ViewModels;
using ASPNet_WPF_ChatApp.ViewModels.Application;
using ASPNet_WPF_ChatApp.ViewModels.Base;
using ASPNet_WPF_ChatApp.ValueConverters;

// This makes it so we can access members on this static class without needing to write "ChatAppDI." first.
using static ASPNet_WPF_ChatApp.DependencyInjection.ChatAppDI;


namespace ASPNet_WPF_ChatApp.Controls
{
    /// <summary>
    /// Interaction logic for PageHost.xaml
    /// </summary>
    public partial class PageHost : UserControl
    {
        #region Dependency Properties

        /// <summary>
        /// The current page to show in the page host
        /// </summary>
        public ApplicationPages CurrentPage
        {
            get { return (ApplicationPages) GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        /// <summary>
        /// Registers <see cref="CurrentPage"/> as a dependency property
        /// </summary>
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(nameof(CurrentPage),
                typeof(ApplicationPages),
                typeof(PageHost),
                new UIPropertyMetadata(default(ApplicationPages), null, new CoerceValueCallback(CurrentPagePropertyChanged)));


        /// <summary>
        /// The view model for the current page
        /// </summary>
        public BaseViewModel CurrentPageViewModel
        {
            get { return (BaseViewModel)GetValue(CurrentPageViewModelProperty); }
            set { SetValue(CurrentPageViewModelProperty, value); }
        }

        /// <summary>
        /// Registers <see cref="CurrentPageViewModel"/> as a dependency property
        /// </summary>
        public static readonly DependencyProperty CurrentPageViewModelProperty =
            DependencyProperty.Register(nameof(CurrentPageViewModel),
                typeof(BaseViewModel),
                typeof(PageHost),
                new UIPropertyMetadata());
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public PageHost()
        {
            InitializeComponent();

            // If we are in design time, show the current page
            // as the dependency property does not fire
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                NewPage.Content = new ApplicationViewModel().CurrentPage.ToBasePage();
            }
        }

        #endregion

        #region Property Changed Events

        /// <summary>
        /// Called when the <see cref="CurrentPage"/> value has changed
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static object CurrentPagePropertyChanged(DependencyObject d, object value)
        {
            // Get current values
            var currentPage = (ApplicationPages)d.GetValue(CurrentPageProperty);
            var currentPageViewModel = d.GetValue(CurrentPageViewModelProperty);

            // Get the frames
            var newPageFrame = (d as PageHost).NewPage;
            var oldPageFrame = (d as PageHost).OldPage;

            // If the current page hasn't changed, just update the view model
            if (newPageFrame.Content is BasePage page &&
                page.ToApplicationPage() == currentPage)
            {
                // Set the view model
                page.ViewModelObject = currentPageViewModel;

                // Just return since we don't need to do anything beyond setting the view model.
                return value;
            }


            // Store the current page content as the old page
            var oldPageContent = newPageFrame.Content;

            // Remove current page from new page frame
            newPageFrame.Content = null;

            // Move the previous page into the old page frame
            oldPageFrame.Content = oldPageContent;

            // Animate out previous page when the Loaded event fires
            // right after this call due to moving it to another frame
            if (oldPageContent is BasePage oldPage)
            {
                // Tell old page to animate out
                oldPage.ShouldAnimateOut = true;

                // Once it is done, remove it
                Task.Delay((int)(oldPage.SlideSeconds * 1000)).ContinueWith((t) =>
                {
                    // Remove old page
                    Application.Current.Dispatcher.Invoke(() => oldPageFrame.Content = null);
                });
            }


            // Set the new page content
            newPageFrame.Content = currentPage.ToBasePage(currentPageViewModel);


            return value;
        }

        #endregion
    }
}
