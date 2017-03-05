using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyousetsukaGetter.Model
{
    public class MainWindowModel
    {
        ViewModel.MainWindowViewModel vm;
        public MainWindowModel(ViewModel.MainWindowViewModel vm)
        {
            this.vm = vm;

            vm.OriginalText = "test";
        }

        IList<NovelInfo> novels = new List<NovelInfo>();

        public IList<NovelInfo> NovelListLoad()
        {
            DirectoryInfo di = new DirectoryInfo(SharedData.SavedNovelListDirPath);
            if (!di.Exists) return null;

            novels.Clear();

            FileInfo[] Files = di.GetFiles("*");
            foreach (FileInfo file in Files)
            {
                using (var fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var xmlReader = new KimamaLib.XMLWrapper.Reader(fs);
                    string ncode = xmlReader.GetValue("//novels/item[@name='ncode']").TrimEnd('\n').TrimEnd('\r');
                    string title = xmlReader.GetValue("//novels/item[@name='title']").TrimEnd('\n').TrimEnd('\r');
                    string userid = xmlReader.GetValue("//novels/item[@name='userid']").TrimEnd('\n').TrimEnd('\r');
                    string writer = xmlReader.GetValue("//novels/item[@name='writer']").TrimEnd('\n').TrimEnd('\r');
                    string story = xmlReader.GetValue("//novels/item[@name='story']").TrimEnd('\n').TrimEnd('\r');
                    string biggenre = xmlReader.GetValue("//novels/item[@name='biggenre']").TrimEnd('\n').TrimEnd('\r');
                    string genre = xmlReader.GetValue("//novels/item[@name='genre']").TrimEnd('\n').TrimEnd('\r');
                    string general_all_no = xmlReader.GetValue("//novels/item[@name='general_all_no']").TrimEnd('\n').TrimEnd('\r');
                    string novel_type = xmlReader.GetValue("//novels/item[@name='novel_type']").TrimEnd('\n').TrimEnd('\r');

                    var novelInfo = new NovelInfo()
                    {
                        NCode = ncode,
                        Title = title,
                        UseId = userid,
                        Writer = writer,
                        Story = story,
                        BigGenre = biggenre,
                        Genre = genre,
                        GeneralAllNo = int.Parse(general_all_no),
                        NType = (NovelType)int.Parse(novel_type),
                    };

                    novels.Add(novelInfo);
                }
            }

            if (novels.Count <= 0) return null;
            return novels;
        }

        public void LoadNovel(NovelInfo novelInfo, bool isForceDownload = false)
        {
            if (novelInfo == null) return;

            DirectoryInfo di = new DirectoryInfo(SharedData.SavelNovelDirPath);
            if (!di.Exists) di.Create();
            FileInfo fi = new FileInfo(di.FullName + @"\" + novelInfo.NCode);
            if (fi.Exists && !isForceDownload)
            {
                using (FileStream fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var xmlReader = new KimamaLib.XMLWrapper.Reader(fs);
                    int count = int.Parse(xmlReader.GetAttribute("count", "//novels/item"));
                    var titleList = xmlReader.GetAttributes("subtitle", "//novels/novel");
                    var textList = xmlReader.GetValues("//novels/novel");

                    novelInfo.Titles = titleList;
                    novelInfo.Texts = textList;
                }
            }
            else
            {
                string ncode = novelInfo.NCode;

                bool nType = false;
                if (novelInfo.NType == NovelType.Serialization)
                {
                    nType = true;
                }
                var novelDownloader = new SyousetukaGetterLib.NovelDownloader(ncode, novelInfo.GeneralAllNo, nType);
                novelDownloader.DownloadNovel();
                List<string> titleList;
                if (nType)
                {
                    titleList = novelDownloader.Title[ncode];
                }
                else
                {
                    titleList = new List<string>();
                    titleList.Add("");
                }
                var textList = novelDownloader.NovelText[ncode];

                novelInfo.Titles = titleList;
                novelInfo.Texts = textList;

                int count = titleList.Count < textList.Count ? titleList.Count : textList.Count;

                using (FileStream fs = new FileStream(fi.FullName, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    var xmlWriter = new KimamaLib.XMLWrapper.Writer();
                    xmlWriter.SetRoot("novels");

                    var attribute = new KimamaLib.XMLWrapper.AttributeInfo()
                    {
                        Name = "count",
                        Value = count.ToString()
                    };

                    xmlWriter.AddElement("item", attribute);
                    for (int i = 0; i < count; ++i)
                    {
                        var attributes = new KimamaLib.XMLWrapper.AttributeInfo[2]
                        {
                            new KimamaLib.XMLWrapper.AttributeInfo() { Name = "page", Value = (i + 1).ToString() },
                            new KimamaLib.XMLWrapper.AttributeInfo() { Name = "subtitle", Value = titleList[i] },
                        };
                        xmlWriter.AddElement("novel", attributes, textList[i]);
                    }
                    xmlWriter.Write(fs);
                }
            }
        }
        public void RemoveNovel(NovelInfo novelInfo)
        {
            string ncode = novelInfo.NCode;
            FileInfo novelFileInfo = new FileInfo(SharedData.SavelNovelDirPath + @"\" + ncode);
            FileInfo novelListFileInfo = new FileInfo(SharedData.SavedNovelListDirPath + @"\" + ncode);
            if (novelFileInfo.Exists)
            {
                novelFileInfo.Delete();
            }
            if (novelListFileInfo.Exists)
            {
                novelListFileInfo.Delete();
            }
        }
    }
}
