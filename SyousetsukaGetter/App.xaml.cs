using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SyousetsukaGetter
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mw = new View.MainWindow();
            mw.Show();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string mes = string.Format(
                "予期せぬエラーが発生しました。\nお手数ですが、開発者に例外内容を報告してください。\n\n---\n\n{0}\n\n{1}",
                e.Exception.Message, e.Exception.StackTrace);
            MessageBox.Show(mes,
                "予期せぬエラー", MessageBoxButton.OK, MessageBoxImage.Error);

            DateTime dt = DateTime.Now;
            using (FileStream fs = new FileStream(KimamaLib.AppInfo.GetAppPath() + @"\error-" + dt.ToString("yyyy-MM-dd- HH-mm-ss") + ".log", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
            {
                using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
                {
                    sw.Write(mes);
                    sw.Close();
                }
                fs.Close();
            }

            // 例外を処理したことを通知
            e.Handled = true;
            // アプリケーションを終了
            Shutdown();
        }
    }
}
