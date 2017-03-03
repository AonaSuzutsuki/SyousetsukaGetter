using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SyousetsukaGetter.ViewModel
{
    public class SearchWindowViewModel : ViewModelBase
    {
        public class SearchListDataInfo : NotifyBase
        {
            public int ID { set; get; }
            public string NID { set; get; }
            public string Name { set; get; }
            private Visibility plusBTVisibility = Visibility.Visible;
            public Visibility PlusBTVisibility
            {
                set
                {
                    plusBTVisibility = value;
                    OnPropertyChanged(this);
                }
                get
                {
                    return plusBTVisibility;
                }
            }
            private Visibility minusBTVisibility = Visibility.Hidden;
            public Visibility MinusBTVisibility
            {
                set
                {
                    minusBTVisibility = value;
                    OnPropertyChanged(this);
                }
                get
                {
                    return minusBTVisibility;
                }
            }
            public bool IsPlus { set; get; } = false;
        }

        public class GenreInfo
        {
            public string ID { set; get; }
            public string Name { set; get; }
        }

        Model.SearchWindowModel model;

        #region Properties
        public ObservableCollection<SearchListDataInfo> SearchListData { set; get; } = new ObservableCollection<SearchListDataInfo>();
        public ObservableCollection<GenreInfo> GenreItems { set; get; }
        public ObservableCollection<GenreInfo> SecondGenreItems { set; get; }
        #endregion

        #region EventProperties
        public ICommand PlusBTClick { private set; get; }
        public ICommand SearchBTClick { private set; get; }
        private Brush listBoxItemBackground = Brushes.Transparent;
        public Brush ListBoxItemBackground
        {
            set
            {
                listBoxItemBackground = value;
                OnPropertyChanged(this);
            }
            get
            {
                return listBoxItemBackground;
            }
        }
        #endregion

        public SearchWindowViewModel(Window view) : base(view)
        {
            model = new Model.SearchWindowModel(this);


            GenreItems = new ObservableCollection<GenreInfo>()
            {
                new GenreInfo() { ID = "1", Name = "恋愛" },
                new GenreInfo() { ID = "2", Name = "ファンタジー" },
                new GenreInfo() { ID = "3", Name = "文芸" },
                new GenreInfo() { ID = "4", Name = "SF" },
                new GenreInfo() { ID = "98", Name = "その他" },
                new GenreInfo() { ID = "99", Name = "ノンジャンル" },
            };

            SecondGenreItems = new ObservableCollection<GenreInfo>()
            {
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

            SearchBTClick = new RelayCommand(SearchBT_Click);
            PlusBTClick = new RelayCommand<int>(PlusBT_Click);
        }

        public void SearchBT_Click()
        {
            ListBoxItemBackground = Brushes.White;
            model.Search();
        }

        public void PlusBT_Click(int id)
        {
            Console.WriteLine("ID : " + SearchListData[id].ID);
            if (!SearchListData[id].IsPlus)
            {
                SearchListData[id].PlusBTVisibility = Visibility.Hidden;
                SearchListData[id].MinusBTVisibility = Visibility.Visible;
                SearchListData[id].IsPlus = true;

                model.AddSaveNodel(id);
            }
            else
            {
                SearchListData[id].PlusBTVisibility = Visibility.Visible;
                SearchListData[id].MinusBTVisibility = Visibility.Hidden;
                SearchListData[id].IsPlus = false;
                
                model.RemoveSaveNodel(id);
            }
        }
    }
}
