namespace KimamaLib
{
    public static class AppInfo
    {
        /// <summary>
        /// 実行ファイルの現在の場所を取得します。
        /// </summary>
        /// <returns>実行ファイルのディレクトリ絶対パス</returns>
        public static string GetAppPath()
        {
            return System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// アプリケーションの現在のバージョンを取得します。
        /// </summary>
        /// <returns>アプリケーションの現在のバージョン</returns>
        public static string GetVer()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            System.Version ver = asm.GetName().Version;

            return ver.ToString();
        }
    }
}
