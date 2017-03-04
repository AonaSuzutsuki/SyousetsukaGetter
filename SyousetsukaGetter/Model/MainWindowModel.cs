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
            DirectoryInfo di = new DirectoryInfo(SharedData.SavedNovelDirPath);
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
    }
}
