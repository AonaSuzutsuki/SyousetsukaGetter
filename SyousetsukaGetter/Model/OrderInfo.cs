using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyousetsukaGetter.Model
{
    public class OrderInfo
    {
        public string ID { set; get; }
        public string Name { set; get; }
    }

    public class OrderInfos
    {
        public static readonly OrderInfo[] Orders =
        {
            new OrderInfo() { ID = "", Name = "" },
            new OrderInfo() { ID = "favnovelcnt", Name = "ブックマーク数の多い順" },
            new OrderInfo() { ID = "reviewcnt", Name = "レビュー数の多い順" },
            new OrderInfo() { ID = "hyoka", Name = "総合評価の高い順" },
            new OrderInfo() { ID = "hyokaasc", Name = "総合評価の低い順" },
            new OrderInfo() { ID = "impressioncnt", Name = "感想の多い順" },
            new OrderInfo() { ID = "hyokacnt", Name = "評価者数の多い順" },
            new OrderInfo() { ID = "hyokacntasc", Name = "評価者数の少ない順" },
            new OrderInfo() { ID = "weekly", Name = "週間ユニークユーザの多い順" },
            new OrderInfo() { ID = "lengthdesc", Name = "小説本文の文字数が多い順" },
            new OrderInfo() { ID = "lengthasc", Name = "小説本文の文字数が少ない順" },
            new OrderInfo() { ID = "ncodedesc", Name = "Nコードが新しい順" },
            new OrderInfo() { ID = "old", Name = "古い順" },
        };

        public static readonly Dictionary<string, string> OrderDictionary = new Dictionary<string, string>()
        {
            { "", "" },
            { "favnovelcnt", "ブックマーク数の多い順" },
            { "reviewcnt", "レビュー数の多い順" },
            { "hyoka", "総合評価の高い順" },
            { "hyokaasc", "総合評価の低い順" },
            { "impressioncnt", "感想の多い順" },
            { "hyokacnt", "評価者数の多い順" },
            { "hyokacntasc", "評価者数の少ない順" },
            { "weekly", "週間ユニークユーザの多い順" },
            { "lengthdesc", "小説本文の文字数が多い順" },
            { "lengthasc", "小説本文の文字数が少ない順" },
            { "ncodedesc", "Nコードが新しい順" },
            { "old", "古い順" },
        };
    }
}
