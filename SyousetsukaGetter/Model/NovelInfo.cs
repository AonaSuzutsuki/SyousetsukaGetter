using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyousetsukaGetter.Model
{
    public enum NovelType
    {
        Serialization = 1,
        Short = 2,
    }
    public class NovelInfo
    {
        public string NCode { set; get; }
        public string Title { set; get; }
        public string UseId { set; get; }
        public string Writer { set; get; }
        public string Story { set; get; }
        public string BigGenre { set; get; }
        public string Genre { set; get; }
        public int GeneralAllNo { set; get; }
        public NovelType NType { set; get; }

        public List<string> Titles { set; get; } = new List<string>();
        public List<string> Texts { set; get; } = new List<string>();

        public Dictionary<string, string> GetValues()
        {
            var dic = new Dictionary<string, string>();

            dic.Add("ncode", NCode);
            dic.Add("title", Title);
            dic.Add("userid", UseId);
            dic.Add("writer", Writer);
            dic.Add("story", Story);
            dic.Add("biggenre", BigGenre);
            dic.Add("genre", Genre);
            dic.Add("general_all_no", GeneralAllNo.ToString());
            dic.Add("novel_type", ((int)NType).ToString());

            return dic;
        }
    }
}
