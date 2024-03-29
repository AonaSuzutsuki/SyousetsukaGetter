﻿using System;
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
using SyousetsukaGetter.Model;

namespace SyousetsukaGetter.ViewModel
{
    public class SearchWindowViewModel : ViewModelBase
    {
        public class SearchListDataInfo : NotifyBase
        {
            public int ID { set; get; }
            public string NID { set; get; }
            public string Name { set; get; }
            public string BigGenre { set; get; }
            public string Genre { set; get; }
            public string GeneralAllNo { set; get; }
            public string Writer { set; get; }

            public bool IsPlus { set; get; } = false;

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
        }
        
        Model.SearchWindowModel model;

        #region Properties
        private ObservableCollection<SearchListDataInfo> searchListData = new ObservableCollection<SearchListDataInfo>();
        public ObservableCollection<SearchListDataInfo> SearchListData
        {
            set
            {
                searchListData = value;
                OnPropertyChanged(this);
            }
            get
            {
                return searchListData;
            }
        }
        /// <summary>
        /// ジャンル一覧
        /// </summary>
        public ObservableCollection<GenreInfo> GenreItems { set; get; }
        /// <summary>
        /// 第二ジャンル一覧
        /// </summary>
        public ObservableCollection<GenreInfo> SecondGenreItems { set; get; }
        /// <summary>
        /// 検索順序
        /// </summary>
        public ObservableCollection<OrderInfo> OrderItems { set; get; }

        private string searchWordText = string.Empty;
        /// <summary>
        /// 検索単語
        /// </summary>
        public string SearchWordText
        {
            set
            {
                searchWordText = value;
                OnPropertyChanged(this);
            }
            get
            {
                return searchWordText;
            }
        }

        private bool titleIsChecked = true;
        /// <summary>
        /// タイトルを検索対象にする
        /// </summary>
        public bool TitleIsChecked
        {
            set
            {
                titleIsChecked = value;
                OnPropertyChanged(this);
            }
            get
            {
                return titleIsChecked;
            }
        }
        private bool storyIsChecked = true;
        /// <summary>
        /// あらすじを検索対象にする
        /// </summary>
        public bool StoryIsChecked
        {
            set
            {
                storyIsChecked = value;
                OnPropertyChanged(this);
            }
            get
            {
                return storyIsChecked;
            }
        }
        public bool keywordIsChecked = true;
        /// <summary>
        /// キーワードを検索対象にする
        /// </summary>
        public bool KeywordIsChecked
        {
            set
            {
                keywordIsChecked = value;
                OnPropertyChanged(this);
            }
            get
            {
                return keywordIsChecked;
            }
        }
        private bool writerIsChecked = true;
        /// <summary>
        /// 作者を検索対象にする
        /// </summary>
        public bool WriterIsChecked
        {
            set
            {
                writerIsChecked = value;
                OnPropertyChanged(this);
            }
            get
            {
                return writerIsChecked;
            }
        }
        
        private int genreSelectedIndex = 0;
        /// <summary>
        /// ジャンルリストで指定されたインデックス
        /// </summary>
        public int GenreSelectedIndex
        {
            set
            {
                genreSelectedIndex = value;
                OnPropertyChanged(this);
            }
            get
            {
                return genreSelectedIndex;
            }
        }
        private int secondGenreSelectedIndex = 0;
        /// <summary>
        /// 第二ジャンルリストで指定されたインデックス
        /// </summary>
        public int SecondGenreSelectedIndex
        {
            set
            {
                secondGenreSelectedIndex = value;
                OnPropertyChanged(this);
            }
            get
            {
                return secondGenreSelectedIndex;
            }
        }
        private int orderSelectedIndex = 0;
        /// <summary>
        /// 検索順序で指定されたインデックス
        /// </summary>
        public int OrderSelectedIndex
        {
            set
            {
                orderSelectedIndex = value;
                OnPropertyChanged(this);
            }
            get
            {
                return orderSelectedIndex;
            }
        }

        private string userIDText = string.Empty;
        /// <summary>
        /// ユーザーIDの入力文字
        /// </summary>
        public string UserIDText
        {
            set
            {
                userIDText = value;
                OnPropertyChanged(this);
            }
            get
            {
                return userIDText;
            }
        }
        private string nCodeText = string.Empty;
        /// <summary>
        /// ncodeの入力文字
        /// </summary>
        public string NCodeText
        {
            set
            {
                nCodeText = value;
                OnPropertyChanged(this);
            }
            get
            {
                return nCodeText;
            }
        }

        private string searchNumText = string.Empty;
        /// <summary>
        /// 検索対象数の入力文字
        /// </summary>
        public string SearchNumText
        {
            set
            {
                searchNumText = value;
                OnPropertyChanged(this);
            }
            get
            {
                return searchNumText;
            }
        }
        #endregion

        #region EventProperties
        public ICommand PlusBTClick { private set; get; }
        public ICommand SearchBTClick { private set; get; }
        public ICommand ResetBTClick { private set; get; }
        public ICommand OKBTClick { private set; get; }
        #endregion

        public SearchWindowViewModel(Window view) : base(view)
        {
            model = new Model.SearchWindowModel(this);


            GenreItems = new ObservableCollection<GenreInfo>(GenreInfos.BigGenres);

            SecondGenreItems = new ObservableCollection<GenreInfo>(GenreInfos.Genres);

            OrderItems = new ObservableCollection<OrderInfo>(OrderInfos.Orders);

            SearchBTClick = new RelayCommand(SearchBT_Click);
            PlusBTClick = new RelayCommand<int>(PlusBT_Click);
            OKBTClick = new RelayCommand(OKBT_Click);
            ResetBTClick = new RelayCommand(ResetBT_Click);
        }

        public void SearchBT_Click()
        {
            SearchListData = new ObservableCollection<SearchListDataInfo>();
            model.Search();
        }
        public void ResetBT_Click()
        {
            SearchWordText = string.Empty;
            UserIDText = string.Empty;
            NCodeText = string.Empty;
            SearchNumText = string.Empty;
            GenreSelectedIndex = 0;
            OrderSelectedIndex = 0;
            SecondGenreSelectedIndex = 0;
            KeywordIsChecked = true;
            StoryIsChecked = true;
            TitleIsChecked = true;
            WriterIsChecked = true;
        }
        public void PlusBT_Click(int id)
        {
            Console.WriteLine("ID : " + SearchListData[id].ID);
            if (!SearchListData[id].IsPlus)
            {
                SearchListData[id].PlusBTVisibility = Visibility.Hidden;
                SearchListData[id].MinusBTVisibility = Visibility.Visible;
                SearchListData[id].IsPlus = true;
                SearchListData[id].ListBoxItemBackground = new SolidColorBrush(Color.FromArgb(200, 0, 0, 0));

                model.AddSaveNodel(id);
            }
            else
            {
                SearchListData[id].PlusBTVisibility = Visibility.Visible;
                SearchListData[id].MinusBTVisibility = Visibility.Hidden;
                SearchListData[id].IsPlus = false;
                SearchListData[id].ListBoxItemBackground = Brushes.Transparent;

                model.RemoveSaveNodel(id);
            }
        }
        
        public void OKBT_Click()
        {
            model.SaveTo();
            view.Close();
        }
    }
}
