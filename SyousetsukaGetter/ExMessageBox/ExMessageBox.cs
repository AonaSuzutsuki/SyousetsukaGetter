using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace SyousetsukaGetter.ExMessageBox
{
    public static class ExMessageBoxBase
    {
        [DllImport("user32.dll")]
        static extern Int32 FlashWindowEx(ref FLASHWINFO pwfi);
        [StructLayout(LayoutKind.Sequential)]
        private struct FLASHWINFO
        {
            public UInt32 cbSize;    // FLASHWINFO構造体のサイズ
            public IntPtr hwnd;      // 点滅対象のウィンドウ・ハンドル
            public UInt32 dwFlags;   // 以下の「FLASHW_XXX」のいずれか
            public UInt32 uCount;    // 点滅する回数
            public UInt32 dwTimeout; // 点滅する間隔（ミリ秒単位）
        }
        // 点滅を止める
        private const UInt32 FLASHW_STOP = 0;
        // タイトルバーを点滅させる
        private const UInt32 FLASHW_CAPTION = 1;
        // タスクバー・ボタンを点滅させる
        private const UInt32 FLASHW_TRAY = 2;
        // タスクバー・ボタンとタイトルバーを点滅させる
        private const UInt32 FLASHW_ALL = 3;
        // FLASHW_STOPが指定されるまでずっと点滅させる
        private const UInt32 FLASHW_TIMER = 4;
        // ウィンドウが最前面に来るまでずっと点滅させる
        private const UInt32 FLASHW_TIMERNOFG = 12;
        public static void FlashWind(IntPtr handle)
        {
            FLASHWINFO fInfo = new FLASHWINFO();
            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = handle;
            fInfo.dwFlags = FLASHW_ALL;
            fInfo.uCount = 5;         // 点滅する回数
            fInfo.dwTimeout = 0;

            FlashWindowEx(ref fInfo);
        }



        public enum MessageType : int
        {
            /// <summary>
            /// メッセージ（情報）
            /// </summary>
            Asterisk = 0,
            /// <summary>
            /// 一般の警告音
            /// </summary>
            Beep = 1,
            /// <summary>
            /// メッセージ（警告）
            /// </summary>
            Exclamation = 2,
            /// <summary>
            /// システムエラー
            /// </summary>
            Hand = 3,
            /// <summary>
            /// メッセージ（問い合わせ）
            /// </summary>
            Question = 4,
        }
        public enum ButtonType : int
        {
            OK = 0,
            YesNo = 1,
        }
        public enum DialogResult : int
        {
            OK = 0,
            Yes = 1,
            No = 2,
        }

        public static DialogResult dr = 0;
        public static DialogResult Show(Window _w, string _text, string _title, MessageType _mType, ButtonType _bType = ButtonType.OK) {
            if (_bType == ButtonType.OK)
            {
                ExOKMassageBox okMsgBox = new ExOKMassageBox(_w, _text, _title, _mType);

                _w.IsEnabled = false;
                okMsgBox.ShowDialog();
                _w.IsEnabled = true;
            }

            if (_bType == ButtonType.YesNo)
            {
                ExYesNoMessageBox yesnoMsgBox = new ExYesNoMessageBox(_w, _text, _title, _mType);

                _w.IsEnabled = false;
                yesnoMsgBox.ShowDialog();
                _w.IsEnabled = true;
            }

            return dr;
        }
    }
}
