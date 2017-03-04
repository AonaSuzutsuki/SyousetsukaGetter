﻿using SyousetukaGetterLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyousetsukaGetter.Model
{
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
            var url = SetJsonUrl();

            var jsonLoader = new JsonLoader(url);
            for (int i = 1; i < jsonLoader.Nodes.Count; ++i)
            {
                var node = jsonLoader.Nodes[i];
                var novel = new NovelInfo()
                {
                    NCode = node["ncode"],
                    Title = node["title"],
                    UseId = node["userid"],
                    Writer = node["writer"],
                    Story = node["story"],
                    BigGenre = GenreInfos.BigGenresDictionary[node["biggenre"]],
                    Genre = GenreInfos.GenresDictionary[node["genre"]],
                    GeneralAllNo = int.Parse(node["general_all_no"]),
                    NType = (NovelType)int.Parse(node["novel_type"]),
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

        private string SetJsonUrl()
        {
            var url = new JsonUrlManager();
            string id = vm.GenreItems[vm.GenreSelectedIndex].ID;
            url.SetGenreItems(id);
            id = vm.SecondGenreItems[vm.GenreSelectedIndex].ID;
            url.SetSecondGenreItems(id);
            string word = vm.SearchWordText;
            url.SetSearchWordText(word);
            bool check = vm.TitleIsChecked;
            url.SetTitleIsChecked(check);
            check = vm.StoryIsChecked;
            url.SetStoryIsChecked(check);
            check = vm.KeywordIsChecked;
            url.SetKeywordIsChecked(check);
            check = vm.WriterIsChecked;
            url.SetWriterIsChecked(check);
            string userID = vm.UserIDText;
            url.SetUserIDText(userID);
            string nCode = vm.NCodeText;
            url.SetNCodeText(nCode);

           vm.SearchNumText = "3"; //テスト用

            int lim = int.Parse(vm.SearchNumText);
            url.SetLim(lim);
            return url.JsonUrl;
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

        public void SaveTo()
        {
            DirectoryInfo di = new DirectoryInfo(SharedData.SavedNovelDirPath);
            if (!di.Exists) di.Create();

            foreach (NovelInfo novelInfo in saveNovels)
            {
                var novelDic = novelInfo.GetValues();
                
                using (FileStream fs = new FileStream(di.FullName + @"\" + novelInfo.NCode, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                {
                    var writer = new KimamaLib.XMLWrapper.Writer();
                    writer.SetRoot("novels");
                    foreach (KeyValuePair<string, string> pair in novelDic)
                    {
                        var attribute = new KimamaLib.XMLWrapper.AttributeInfo()
                        {
                            Name = "name",
                            Value = pair.Key
                        };
                        writer.AddElement("item", attribute, pair.Value);
                    }

                    writer.Write(fs);
                }
            }
        }
    }
}
