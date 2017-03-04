using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyousetsukaGetter.Model
{
    public static class SharedData
    {
        public static string SavedNovelDirPath
        {
            get
            {
                return KimamaLib.AppInfo.GetAppPath() + @"\Novels\List";
            }
        }
    }
}
