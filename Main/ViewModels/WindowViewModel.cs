using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shell;

using ASPNet_WPF_ChatApp.Core.DataModels;
using ASPNet_WPF_ChatApp.Core.ViewModels.Base;
using ASPNet_WPF_ChatApp.Window;

using static ASPNet_WPF_ChatApp.Window.WindowResizer; // This is static because the last name here (WindowResizer) is a type, not a namespace.



namespace ASPNet_WPF_ChatApp.ViewModels
{
    /// <summary>
    /// The View Model for the custom flat window
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The window this view model controls
        /// </summary>
        private System.Windows.Window _Window;

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        private int _OuterMarginSize = 10;

        /// <summary>
        /// The radius of the corners of the window
        /// </summary>
        private int _WindowCornerSize = 10; 

        private WindowDockPosition _DockPosition = WindowDockPosition.Undocked;

        #endregion

        #region Public Properties

        /// <summary>
        /// The smallest width the window can go to
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 800;
        
        /// <summary>
        /// The smallest height the window can go to
        /// </summary>
        public double WindowMinimumHeight { get; set; } = 500;

        public bool Borderless { get => (_Window.WindowState == WindowState.Maximized || _DockPosition != WindowDockPosition.Undocked); }

        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorderSize { get => Borderless ? 0 : 6; }

        /// <summary>
        /// The size of the resize border around the window (as a Thickness object).
        /// </summary>
        public Thickness ResizeBorderThickness { get => new Thickness(ResizeBorderSize + OuterMarginSize); }

        /// <summary>
        /// The padding of the inner content of the main window
        /// </summary>
        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        /// <summary>
        /// The size of the margin around the window that allows for the drop shadow
        /// </summary>
        public int OuterMarginSize
        {
            get => Borderless ? 0 : _OuterMarginSize;
            set => _OuterMarginSize = value;            
        }

        /// <summary>
        /// The size (as a Thickness object) of the margin around the window that allows for the drop shadow
        /// </summary>
        public Thickness OuterMarginThickness
        { 
            get => new Thickness(_OuterMarginSize);            
        }

        /// <summary>
        /// The radius of the corners of the window
        /// </summary>
        public int WindowCornerSize
        {
            get => Borderless ? 0 : _WindowCornerSize;
            set => _WindowCornerSize = value;            
        }

        /// <summary>
        /// The radius (as a CornerRadius object) of the corners of the window
        /// </summary>
        public CornerRadius WindowCornerRadius
        {
            get => new CornerRadius(_WindowCornerSize);
        }

        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public int TitleHeight { get; set; } = 42;

        /// <summary>
        /// The height of the title bar / caption of the window as a GridLength object
        /// </summary>
        public GridLength TitleHeightGridLength { get => new GridLength(TitleHeight + ResizeBorderSize); }

        #endregion

        #region Commands

        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// The command to maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to open the system menu
        /// </summary>
        public ICommand SysMenuCommand { get; set; }

        #endregion

        #region Constructors        

        /// <summary>
        /// Default constructor
        /// </summary>
        public WindowViewModel(System.Windows.Window window)
        {
            _Window = window;

            // Listen for the window resizing
            _Window.StateChanged += OnWindowResized;


            // Create commands
            MinimizeCommand = new RelayCommand(() => _Window.WindowState =(_Window.WindowState != WindowState.Minimized ? _Window.WindowState = WindowState.Minimized : _Window.WindowState = WindowState.Normal));
            MaximizeCommand = new RelayCommand(() => _Window.WindowState = (_Window.WindowState != WindowState.Maximized) ? _Window.WindowState = WindowState.Maximized : _Window.WindowState = WindowState.Normal);
            //MaximizeCommand = new RelayCommand(() => Maximize());
            CloseCommand = new RelayCommand(() => _Window.Close());
            SysMenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(_Window, GetMousePosition()));


            // Fix window resize issue
            var resizer = new WindowResizer(_Window);

            // Listen for dock changes
            resizer.WindowDockChanged += OnDockingChanged;
        }

        #endregion

        private void Maximize()
        {
            if (_Window.WindowState != WindowState.Maximized)
            {
                _Window.WindowState = WindowState.Maximized;

                WindowChrome.SetWindowChrome(_Window, null);
            }
            else
            {
                _Window.WindowState = WindowState.Normal;

                WindowChrome.SetWindowChrome(_Window, new WindowChrome
                {
                    CaptionHeight = TitleHeight,
                    CornerRadius = WindowCornerRadius,
                    GlassFrameThickness = new Thickness(0),
                });
            }
        }

        #region Private Helpers

        /// <summary>
        /// Gets the current mouse position in screen space.
        /// </summary>
        /// <returns></returns>
        private Point GetMousePosition()
        {
            // Gets the position of the mouse relative to the window
            var position = Mouse.GetPosition(_Window);

            // Add the window position to convert it to screen space.
            return new Point(position.X + _Window.Left,
                             position.Y + _Window.Top);
        }

        #endregion

        #region Event Handlers

        private void OnWindowResized(object? sender, EventArgs e)
        {            
            // Fire off events for all properties that are affected by a resize
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));

            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginThickness));

            OnPropertyChanged(nameof(WindowCornerSize));
            OnPropertyChanged(nameof(WindowCornerRadius));
        }

        private void OnDockingChanged(WindowDockPosition dockingPosition)
        {
            // Store the last docking position
            _DockPosition = dockingPosition;

            // Fire off resize events
            OnWindowResized(this, EventArgs.Empty);
        }

        #endregion
    }

}
