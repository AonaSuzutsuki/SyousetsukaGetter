using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace SyousetsukaGetter.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected Window view;
        public ViewModelBase(Window view)
        {
            this.view = view;

            imageMouseDown = new RelayCommand<MouseEventArgs>(Image_MouseDown);
            mainWindowMinimumBTClick = new RelayCommand(MainWindowMinimumBT_Click);
            mainWindowMaximumBTClick = new RelayCommand(MainWindowMaximumBT_Click);
            mainWindowCloseBTClick = new RelayCommand(MainWindowCloseBT_Click);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(object sender, [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }

        #region StaticMember
        private static readonly Thickness zeroMargin = new Thickness(0);
        private static readonly Thickness fiveMargin = new Thickness(5);
        #endregion

        #region EventProperties
        protected ICommand imageMouseDown = null;
        public ICommand ImageMouseDown
        {
            get { return imageMouseDown; }
        }
        protected ICommand mainWindowMinimumBTClick = null;
        public ICommand MainWindowMinimumBTClick
        {
            get { return mainWindowMinimumBTClick; }
        }
        protected ICommand mainWindowMaximumBTClick = null;
        public ICommand MainWindowMaximumBTClick
        {
            get { return mainWindowMaximumBTClick; }
        }
        protected ICommand mainWindowCloseBTClick = null;
        public ICommand MainWindowCloseBTClick
        {
            get { return mainWindowCloseBTClick; }
        }
        #endregion

        #region MainProperties
        protected string mainWindowMinimumBTContent = "0";
        public string MainWindowMinimumBTContent
        {
            set
            {
                mainWindowMinimumBTContent = value;
                OnPropertyChanged(this);
            }
            get { return mainWindowMinimumBTContent; }
        }
        protected string mainWindowMaximumBTContent = "1";
        public string MainWindowMaximumBTContent
        {
            set
            {
                mainWindowMaximumBTContent = value;
                OnPropertyChanged(this);
            }
            get { return mainWindowMaximumBTContent; }
        }
        protected string mainWindowCloseBTContent = "r";
        public string MainWindowCloseBTContent
        {
            set
            {
                mainWindowCloseBTContent = value;
                OnPropertyChanged(this);
            }
            get { return mainWindowCloseBTContent; }
        }

        protected Thickness mainWindowMargin = zeroMargin;
        public Thickness MainWindowMargin
        {
            set
            {
                mainWindowMargin = value;
                OnPropertyChanged(this);
            }
            get
            {
                return mainWindowMargin;
            }
        }
        #endregion

        #region EventMethods
        protected void Image_MouseDown(MouseEventArgs e)
        {
            Point p = view.PointToScreen(e.GetPosition(view));
            int x = (int)p.X;
            int y = (int)p.Y;
            ExWindow.WindowCommands.ShowSystemMenu(view, x, y);
        }
        protected void MainWindowMinimumBT_Click()
        {
            view.WindowState = WindowState.Minimized;
        }
        protected void MainWindowMaximumBT_Click()
        {
            if (view.WindowState != WindowState.Maximized)
            {
                view.WindowState = WindowState.Maximized;
                MainWindowMaximumBTContent = "2";
                MainWindowMargin = fiveMargin;
            }
            else
            {
                view.WindowState = WindowState.Normal;
                MainWindowMaximumBTContent = "1";
                MainWindowMargin = zeroMargin;
            }
        }
        protected void MainWindowCloseBT_Click()
        {
            view.Close();
        }
        #endregion
    }
}
