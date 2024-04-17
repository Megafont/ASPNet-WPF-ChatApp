using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using WpfChatASPNet_WPF_ChatApp.ViewModels.Base;


namespace ASPNet_WPF_ChatApp.ViewModel
{
    /// <summary>
    /// The View Model for the custom flat window
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        private int _OuterMarginSize = 10;

        /// <summary>
        /// The radius of the corners of the window
        /// </summary>
        private int _WindowCornerSize = 10; 

        /// <summary>
        /// The window this view model controls
        /// </summary>
        private Window _Window;

        #endregion

        #region Public Properties

        /// <summary>
        /// The size of the margin around the window that allows for the drop shadow
        /// </summary>
        public int OuterMarginSize
        {
            get
            {
                return _Window.WindowState == WindowState.Maximized ? 0 : _OuterMarginSize;
            }
            set
            {
                _OuterMarginSize = value;
            }
        }

        /// <summary>
        /// The size (as a Thickness object) of the margin around the window that allows for the drop shadow
        /// </summary>
        public Thickness OuterMarginSizeThickness
        {
            get
            {
                return new Thickness(_OuterMarginSize);
            }
        }

        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorderSize { get; set; } = 6;

        
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorderSize + OuterMarginSize); } }

        /// <summary>
        /// The radius of the corners of the window
        /// </summary>
        public int WindowCornerSize
        {
            get
            {
                return _Window.WindowState == WindowState.Maximized ? 0 : _WindowCornerSize;
            }
            set
            {
                _WindowCornerSize = value;
            }
        }

        /// <summary>
        /// The radius (as a CornerRadius object) of the corners of the window
        /// </summary>
        public CornerRadius WindowCornerRadius
        {
            get
            {
                return new CornerRadius(_WindowCornerSize);
            }
        }

        #endregion

        #region Constructors        

        /// <summary>
        /// Default constructor
        /// </summary>
        public WindowViewModel(Window window)
        {
            _Window = window;

            // Listen for the window resizing
            _Window.StateChanged += OnWindowResized;

        }

        #endregion

        private void OnWindowResized(object sender, EventArgs e)
        {

        }
    }

}
