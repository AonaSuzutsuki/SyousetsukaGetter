using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyousetsukaGetter.Model
{
    public class GenreInfo
    {
        public string ID { set; get; }
        public string Name { set; get; }
    }

    public static class GenreInfos
    {
        public static readonly GenreInfo[] BigGenres =
        {
            new GenreInfo() { ID = "0", Name = "" },
            new GenreInfo() { ID = "1", Name = "恋愛" },
            new GenreInfo() { ID = "2", Name = "ファンタジー" },
            new GenreInfo() { ID = "3", Name = "文芸" },
            new GenreInfo() { ID = "4", Name = "SF" },
            new GenreInfo() { ID = "99", Name = "その他" },
            new GenreInfo() { ID = "98", Name = "ノンジャンル" },
        };

        public static readonly Dictionary<string, string> BigGenresDictionary = new Dictionary<string, string>()
        {
            {"0", ""},
            {"1", "恋愛"},
            {"2", "ファンタジー"},
            {"3", "文芸"},
            {"4", "SF"},
            {"99", "その他"},
            {"98", "ノンジャンル"},
        };

        public static readonly GenreInfo[] Genres =
        {
            new GenreInfo() { ID = "0", Name = "" },
            new GenreInfo() { ID = "101", Name = "異世界〔恋愛〕" },
            new GenreInfo() { ID = "102", Name = "現実世界〔恋愛〕" },
            new GenreInfo() { ID = "201", Name = "ハイファンタジー〔ファンタジー〕" },
            new GenreInfo() { ID = "202", Name = "ローファンタジー〔ファンタジー〕" },
            new GenreInfo() { ID = "301", Name = "純文学〔文芸〕" },
            new GenreInfo() { ID = "302", Name = "ヒューマンドラマ〔文芸〕" },
            new GenreInfo() { ID = "303", Name = "歴史〔文芸〕" },
            new GenreInfo() { ID = "304", Name = "推理〔文芸〕" },
            new GenreInfo() { ID = "305", Name = "ホラー〔文芸〕" },
            new GenreInfo() { ID = "306", Name = "アクション〔文芸〕" },
            new GenreInfo() { ID = "307", Name = "コメディー〔文芸〕" },
            new GenreInfo() { ID = "401", Name = "VRゲーム〔SF〕" },
            new GenreInfo() { ID = "402", Name = "宇宙〔SF〕" },
            new GenreInfo() { ID = "403", Name = "空想科学〔SF〕" },
            new GenreInfo() { ID = "404", Name = "パニック〔SF〕" },
            new GenreInfo() { ID = "9901", Name = "童話〔その他〕" },
            new GenreInfo() { ID = "9902", Name = "詩〔その他〕" },
            new GenreInfo() { ID = "9903", Name = "エッセイ〔その他〕" },
            new GenreInfo() { ID = "9904", Name = "リプレイ〔その他〕" },
            new GenreInfo() { ID = "9999", Name = "その他〔その他〕" },
            new GenreInfo() { ID = "9801", Name = "ノンジャンル〔ノンジャンル〕" },
        };

        public static readonly Dictionary<string, string> GenresDictionary = new Dictionary<string, string>()
        {
            {"0", ""},
            {"101", "異世界〔恋愛〕" },
            {"102", "現実世界〔恋愛〕" },
            {"201", "ハイファンタジー〔ファンタジー〕" },
            {"202", "ローファンタジー〔ファンタジー〕" },
            {"301", "純文学〔文芸〕" },
            {"302", "ヒューマンドラマ〔文芸〕" },
            {"303", "歴史〔文芸〕" },
            {"304", "推理〔文芸〕" },
            {"305", "ホラー〔文芸〕" },
            {"306", "アクション〔文芸〕" },
            {"307", "コメディー〔文芸〕" },
            {"401", "VRゲーム〔SF〕" },
            {"402", "宇宙〔SF〕" },
            {"403", "空想科学〔SF〕" },
            {"404", "パニック〔SF〕" },
            {"9901", "童話〔その他〕" },
            {"9902", "詩〔その他〕" },
            {"9903", "エッセイ〔その他〕" },
            {"9904", "リプレイ〔その他〕" },
            {"9999", "その他〔その他〕" },
            {"9801", "ノンジャンル〔ノンジャンル〕" },
        };
    }
}
