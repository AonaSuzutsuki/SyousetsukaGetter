using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SyousetukaGetterLib
{
    class PageDownloader
    {
        private NovelUrlManager url = new NovelUrlManager();

        public void StartDownload(int index)
        {
            string[] urls = url.Novel(index);

            return;
        }





    }
}
