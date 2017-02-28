using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ExWindow
{
    public class wInOut
    {
        Border AroundBorder;
        Window w;
        public wInOut(Window _w, Border _brd)
        {
            w = _w;
            AroundBorder = _brd;

            w.Activated += W_Activated;
            w.Deactivated += W_Deactivated;
        }

        private void W_Activated(object sender, EventArgs e)
        {
            Deactivated = false;
        }
        private void W_Deactivated(object sender, EventArgs e)
        {
            Deactivated = true;
        }

        private bool _Deactivated = false;
        public bool Deactivated
        {
            set
            {
                _Deactivated = value;

                if (value)
                {
                    BorderBrushChangeColor(AroundBorder, (SolidColorBrush)w.Resources["DeAroundBorderColor"]);
                }
                else
                {
                    BorderBrushChangeColor(AroundBorder, (SolidColorBrush)w.Resources["AroundBorderColor"]);
                }
            }
            get
            {
                return _Deactivated;
            }
        }

        public void BorderBrushChangeColor(Border Control, SolidColorBrush Color)
        {
            Control.BorderBrush = Color;
        }
        public void BackgroundChangeColor(Grid Control, SolidColorBrush Color)
        {
            Control.Background = Color;
        }
    }

    public class WindowCommands
    {
        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        public static void ShowSystemMenu(Window w, int x, int y)
        {
            IntPtr lParam = new IntPtr(x | y << 16);

            //Windowハンドルを取得し、0x313メッセージを送信
            IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(w).Handle;
            PostMessage(hwnd, 0x313, IntPtr.Zero, lParam);
        }
    }
}
