using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace SyousetsukaGetter.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        Model.MainWindowModel model;
        public MainWindowViewModel(Window view) : base(view)
        {
            initialize();

            model = new Model.MainWindowModel(this);
            loadList();
        }

        private void initialize()
        {
            AddBTClick = new RelayCommand(AddBT_Click);
            NovelListSelectionChanged = new RelayCommand(NovelList_SelectionChanged);
            NextPageBTClick = new RelayCommand(NextPageBT_Click);
            PreviousPageBTClick = new RelayCommand(PreviousPageBT_Click);
            DownloadBTClick = new RelayCommand(DownloadBT_Click);
        }

        #region Properties
        public ObservableCollection<Model.NovelInfo> NovelListItem { set; get; } = new ObservableCollection<Model.NovelInfo>();

        public string originalText = string.Empty;
        public string OriginalText
        {
            set
            {
                originalText = value;
                OnPropertyChanged(this);
            }
            get
            {
                return originalText;
            }
        }

        private int novelListSelectedIndex;
        public int NovelListSelectedIndex
        {
            set
            {
                novelListSelectedIndex = value;
                OnPropertyChanged(this);
            }
            get
            {
                return novelListSelectedIndex;
            }
        }

        private bool previousBTIsEnabled = false;
        public bool PreviousBTIsEnabled
        {
            set
            {
                previousBTIsEnabled = value;
                OnPropertyChanged(this);
            }
            get
            {
                return previousBTIsEnabled;
            }
        }
        private bool nextBTIsEnabled = false;
        public bool NextBTIsEnabled
        {
            set
            {
                nextBTIsEnabled = value;
                OnPropertyChanged(this);
            }
            get
            {
                return nextBTIsEnabled;
            }
        }

        private int currentPage = 1;
        public int CurrentPage
        {
            set
            {
                currentPage = value;
                OnPropertyChanged(this);
            }
            get
            {
                return currentPage;
            }
        }
        private int maxPage = 1;
        public int MaxPage
        {
            set
            {
                maxPage = value;
                OnPropertyChanged(this);
            }
            get
            {
                return maxPage;
            }
        }

        private string subTitleText = string.Empty;
        public string SubTitleText
        {
            set
            {
                subTitleText = value;
                OnPropertyChanged(this);
            }
            get
            {
                return subTitleText;
            }
        }

        private List<string> titleList;
        private List<string> textList;
        #endregion

        #region EventProperties
        public ICommand AddBTClick { private set; get; }
        public ICommand NovelListSelectionChanged { private set; get; }
        public ICommand NextPageBTClick { private set; get; }
        public ICommand PreviousPageBTClick { private set; get; }
        public ICommand DownloadBTClick { private set; get; }
        #endregion

        public void AddBT_Click()
        {
            var searchWindow = new View.SearchWindow();
            searchWindow.ShowDialog();
            loadList();
        }

        public void NovelList_SelectionChanged()
        {
            if (NovelListSelectedIndex < 0) return;
            
            loadNovel(NovelListSelectedIndex);
        }

        public void NextPageBT_Click()
        {
            int index = ++CurrentPage;
            string text = textList[index - 1];
            string title = titleList[index - 1];
            OriginalText = text;
            SubTitleText = title;

            if (CurrentPage >= MaxPage)
            {
                PreviousBTIsEnabled = true;
                NextBTIsEnabled = false;
            }
            else
            {
                PreviousBTIsEnabled = true;
                NextBTIsEnabled = true;
            }
        }
        public void PreviousPageBT_Click()
        {
            int index = --CurrentPage;
            string text = textList[index - 1];
            string title = titleList[index - 1];
            OriginalText = text;
            SubTitleText = title;

            if (CurrentPage <= 1)
            {
                PreviousBTIsEnabled = false;
                NextBTIsEnabled = true;
            }
            else
            {
                PreviousBTIsEnabled = true;
                NextBTIsEnabled = true;
            }
        }

        public void DownloadBT_Click()
        {
            if (NovelListSelectedIndex < 0) return;
            loadNovel(NovelListSelectedIndex, true);
        }

        private void loadList()
        {
            NovelListItem.Clear();
            var novelList = model.NovelListLoad();
            if (novelList == null) return;

            foreach (var novelInfo in novelList)
            {
                NovelListItem.Add(novelInfo);
            }
            NovelListSelectedIndex = 0;
            loadNovel(0);
        }
        private void loadNovel(int index, bool isForceDownload = false)
        {
            var selectedNovel = NovelListItem[index];
            model.LoadNovel(selectedNovel, isForceDownload);
            titleList = selectedNovel.Titles;
            textList = selectedNovel.Texts;
            if (textList.Count > 0 && titleList.Count > 0)
            {
                SubTitleText = titleList[0];
                OriginalText = textList[0];
                MaxPage = textList.Count;
                CurrentPage = 1;
            }

            if (textList.Count <= 1 && titleList.Count <= 1)
            {
                PreviousBTIsEnabled = false;
                NextBTIsEnabled = false;
            }
            else
            {
                PreviousBTIsEnabled = false;
                NextBTIsEnabled = true;
            }
        }
    }
}
