using SyousetukaGetterLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyousetsukaGetter.Model
{
    public class NovelAuthorInfo
    {
        public string UseId { set; get; }
        public string Writer { set; get; }
    }
    public class NovelInfo
    {
        public string NCode { set; get; }
        public string Title { set; get; }
        public NovelAuthorInfo Author { set; get; }
        public string Story { set; get; }
        public string BigGenre { set; get; }
        public string Genre { set; get; }
        public int GeneralAllNo { set; get; }
    }

    public class SearchWindowModel
    {
        ViewModel.SearchWindowViewModel vm;

        public SearchWindowModel(ViewModel.SearchWindowViewModel vm)
        {
            this.vm = vm;
        }

        // 検索済み全書籍データ
        IList<NovelInfo> novels = new List<NovelInfo>();
        // 保存用書籍データ
        IList<NovelInfo> saveNovels = new List<NovelInfo>();

        
        public void Search()
        {
            var jsonLoader = new JsonLoader("test.txt", true);
            for (int i = 1; i < jsonLoader.Nodes.Count; ++i)
            {
                var node = jsonLoader.Nodes[i];
                var novel = new NovelInfo()
                {
                    NCode = node["ncode"],
                    Title = node["title"],
                    BigGenre = GenreInfos.BigGenresDictionary[node["biggenre"]],
                    Genre = GenreInfos.GenresDictionary[node["genre"]],
                    GeneralAllNo = int.Parse(node["general_all_no"]),
                };
                novels.Add(novel);
            }

            foreach (var item in novels.Select((v, i) => new { v, i }))
            {
                var searchListDataInfo = new ViewModel.SearchWindowViewModel.SearchListDataInfo()
                {
                    ID = item.i,
                    NID = item.v.NCode,
                    Name = item.v.Title,
                    BigGenre = item.v.BigGenre,
                    Genre = item.v.Genre,
                };
                vm.SearchListData.Add(searchListDataInfo);
            }
        }

        public void AddSaveNodel(int id)
        {
            saveNovels.Add(novels[id]);
        }
        public void RemoveSaveNodel(int id)
        {
            foreach (var item in saveNovels.Select((v, i) => new { v, i }))
            {
                var ncode = novels[id].NCode;
                if (item.v.NCode.Equals(ncode))
                {
                    saveNovels.RemoveAt(item.i);
                    return;
                }
            }
        }
    }
}
