using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;

namespace SyousetsukaGetter.ExMessageBox
{
    /// <summary>
    /// ExMassageBox.xaml の相互作用ロジック
    /// </summary>
    public partial class ExOKMassageBox : Window
    {
        public class ViewModel : System.ComponentModel.INotifyPropertyChanged
        {
            public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
                }
            }

            string _MsgText = string.Empty;
            public string MsgText
            {
                set
                {
                    _MsgText = value;
                    OnPropertyChanged("MsgText");
                }
                get
                {
                    return _MsgText;
                }
            }
            string _MSGTitle = string.Empty;
            public string MSGTitle
            {
                set
                {
                    _MSGTitle = value;
                    OnPropertyChanged("MSGTitle");
                }
                get
                {
                    return _MSGTitle;
                }
            }

            public void BorderBrushChangeColor(Border control, SolidColorBrush color)
            {
                control.BorderBrush = color;
            }
            public void BackgroundChangeColor(Grid control, SolidColorBrush color)
            {
                control.Background = color;
            }
        }

        ViewModel _vm;
        public ExOKMassageBox(Window _w, string _text, string _title, ExMessageBoxBase.MessageType _mType)
        {
            InitializeComponent();

            _vm = new ViewModel();
            DataContext = _vm;

            wIO = new ExWindow.WindowInOut(this, AroundBorder);

            w = _w;
            text = _text;
            title = _title;
            mType = _mType;

            this.Owner = _w;
        }

        private void MainWindowCloseBT_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        Window w;
        ExWindow.WindowInOut wIO;
        string text = string.Empty;
        string title = string.Empty;
        ExMessageBoxBase.MessageType mType;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HwndSource source = (HwndSource)HwndSource.FromVisual(this);
            ExMessageBoxBase.FlashWind(source.Handle);

            _vm.MsgText = text;
            _vm.MSGTitle = title;

            if (mType == ExMessageBoxBase.MessageType.Asterisk)
            {
                System.Media.SystemSounds.Asterisk.Play();
            }
            if (mType == ExMessageBoxBase.MessageType.Beep)
            {
                System.Media.SystemSounds.Beep.Play();
            }
            if (mType == ExMessageBoxBase.MessageType.Exclamation)
            {
                System.Media.SystemSounds.Exclamation.Play();
            }
            if (mType == ExMessageBoxBase.MessageType.Hand)
            {
                System.Media.SystemSounds.Hand.Play();
            }
            if (mType == ExMessageBoxBase.MessageType.Question)
            {
                System.Media.SystemSounds.Question.Play();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ExMessageBoxBase.dr = ExMessageBoxBase.DialogResult.OK;
        }
    }
}
