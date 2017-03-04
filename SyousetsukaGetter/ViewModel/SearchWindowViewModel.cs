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
        public ObservableCollection<SearchListDataInfo> SearchListData { set; get; } = new ObservableCollection<SearchListDataInfo>();
        public ObservableCollection<GenreInfo> GenreItems { set; get; }
        public ObservableCollection<GenreInfo> SecondGenreItems { set; get; }
        #endregion

        #region EventProperties
        public ICommand PlusBTClick { private set; get; }
        public ICommand SearchBTClick { private set; get; }
        public ICommand OKBT_Click { private set; get; }
        #endregion

        public SearchWindowViewModel(Window view) : base(view)
        {
            model = new Model.SearchWindowModel(this);


            GenreItems = new ObservableCollection<GenreInfo>(GenreInfos.BigGenres);

            SecondGenreItems = new ObservableCollection<GenreInfo>(GenreInfos.Genres);

            SearchBTClick = new RelayCommand(SearchBT_Click);
            PlusBTClick = new RelayCommand<int>(PlusBT_Click);
            OKBT_Click = new RelayCommand(OKBTClick);
        }

        public void SearchBT_Click()
        {
            SearchListData.Clear();
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

        public void OKBTClick()
        {
            model.SaveTo();
            view.Close();
        }
    }
}
