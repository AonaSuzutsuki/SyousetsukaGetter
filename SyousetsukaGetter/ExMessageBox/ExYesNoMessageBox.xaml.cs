using System;
using System.Windows;
using System.Windows.Interop;

namespace SyousetsukaGetter.ExMessageBox
{
    /// <summary>
    /// ExYesNoMessageBox.xaml の相互作用ロジック
    /// </summary>
    public partial class ExYesNoMessageBox : Window
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
        }


        ViewModel vm;
        ExWindow.wInOut wIO;

        Window w;
        string text = string.Empty;
        string title = string.Empty;
        ExMessageBoxBase.MessageType mType;
        public ExYesNoMessageBox(Window _w, string _text, string _title, ExMessageBoxBase.MessageType _mType)
        {
            InitializeComponent();

            vm = new ViewModel();
            DataContext = vm;

            wIO = new ExWindow.wInOut(this, AroundBorder);

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HwndSource source = (HwndSource)HwndSource.FromVisual(this);
            ExMessageBoxBase.FlashWind(source.Handle);

            vm.MsgText = text;
            vm.MSGTitle = title;

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
            
        }

        private void NoBT_Click(object sender, RoutedEventArgs e)
        {
            ExMessageBoxBase.dr = ExMessageBoxBase.DialogResult.No;
            Close();
        }
        private void YesBT_Click(object sender, RoutedEventArgs e)
        {
            ExMessageBoxBase.dr = ExMessageBoxBase.DialogResult.Yes;
            Close();
        }
    }
}
