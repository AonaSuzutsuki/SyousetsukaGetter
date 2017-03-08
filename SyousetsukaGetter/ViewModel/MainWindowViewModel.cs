using SyousetsukaGetter.ExMessageBox;
using SyousetsukaGetter.Localize.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
            Initialize();

            model = new Model.MainWindowModel(this);
            LoadList();
        }

        private void Initialize()
        {
            AddBTClick = new RelayCommand(AddBT_Click);
            MinusBTClick = new RelayCommand(MinusBT_Click);
            NovelListSelectionChanged = new RelayCommand(NovelList_SelectionChanged);
            NextPageBTClick = new RelayCommand(NextPageBT_Click);
            PreviousPageBTClick = new RelayCommand(PreviousPageBT_Click);
            DownloadBTClick = new RelayCommand(DownloadBT_Click);
            CurrentPageKeyDown = new RelayCommand<KeyEventArgs>(CurrentPage_KeyDown);
            MainWindowVersionInfoBTClick = new RelayCommand(MainWindowVersionInfoBT_Click);
        }

        #region Properties
        private ObservableCollection<Model.NovelInfo> novelListItem = new ObservableCollection<Model.NovelInfo>();
        public ObservableCollection<Model.NovelInfo> NovelListItem
        {
            set
            {
                novelListItem = value;
                OnPropertyChanged(this);
            }
            get
            {
                return novelListItem;
            }
        }

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

        string currentPageText = string.Empty;
        public string CurrentPageText
        {
            set
            {

                currentPageText = value;

                int.TryParse(value, out int page);
                currentPage = page;

                OnPropertyChanged(this);
            }
            get
            {
                return currentPageText;
            }
        }

        private int currentPage = 1;
        public int CurrentPage
        {
            set
            {
                currentPage = value;
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

        private Model.NovelInfo currentNodelInfo;
        #endregion

        #region EventProperties
        public ICommand MainWindowVersionInfoBTClick { private set; get; }

        public ICommand AddBTClick { private set; get; }
        public ICommand MinusBTClick { private set; get; }
        public ICommand NovelListSelectionChanged { private set; get; }
        public ICommand NextPageBTClick { private set; get; }
        public ICommand PreviousPageBTClick { private set; get; }
        public ICommand DownloadBTClick { private set; get; }

        public ICommand CurrentPageKeyDown { private set; get; }
        #endregion


        #region EventMethods
        private void MainWindowVersionInfoBT_Click()
        {
            view.IsEnabled = false;
            View.VersionInfo vInfo = new View.VersionInfo();
            vInfo.ShowDialog();
            view.IsEnabled = true;
        }

        private void AddBT_Click()
        {
            view.IsEnabled = false;
            var searchWindow = new View.SearchWindow();
            searchWindow.ShowDialog();
            view.IsEnabled = true;
            LoadList();
        }
        private void MinusBT_Click()
        {
            int index = NovelListSelectedIndex;
            if (index < 0) return;

            var novelInfo = NovelListItem[index];

            string alertTitle = Resources.DeleteNovelTitle;
            string alertMessage = string.Format(Resources.DeleteAlert, novelInfo.Title);
            ExMessageBoxBase.DialogResult dr = ExMessageBoxBase.Show(view, alertMessage, alertTitle, ExMessageBoxBase.MessageType.Asterisk, ExMessageBoxBase.ButtonType.YesNo);
            if (dr == ExMessageBoxBase.DialogResult.No)
            {
                return;
            }

            NovelListItem.RemoveAt(index);
            model.RemoveNovel(novelInfo);

            SubTitleText = "";
            OriginalText = "";
            CurrentPageText = "";
            MaxPage = 0;
            NextBTIsEnabled = false;
            PreviousBTIsEnabled = false;
        }

        private void NovelList_SelectionChanged()
        {
            if (NovelListSelectedIndex < 0) return;
            
            LoadNovel(NovelListSelectedIndex);
        }

        private void NextPageBT_Click()
        {
            int index = ++CurrentPage;
            CurrentPageText = index.ToString();
            PageChange(index);
        }
        private void PreviousPageBT_Click()
        {
            int index = --CurrentPage;
            CurrentPageText = index.ToString();
            PageChange(index);
        }

        private void DownloadBT_Click()
        {
            if (NovelListSelectedIndex < 0) return;
            model.DownloadNovel(NovelListItem[NovelListSelectedIndex]);
            LoadNovel(NovelListSelectedIndex);
        }

        private void CurrentPage_KeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PageChange(CurrentPage);
            }
        }
        #endregion

        private void PageChange(int index)
        {
            if (index > MaxPage || index <= 0) return;

            model.SavePage(currentNodelInfo, CurrentPage);

            string text = textList[index - 1];
            string title = titleList[index - 1];
            OriginalText = text;
            SubTitleText = title;
            
            if (MaxPage <= 1)
            {
                PreviousBTIsEnabled = false;
                NextBTIsEnabled = false;
            }
            else if (CurrentPage >= MaxPage)
            {
                PreviousBTIsEnabled = true;
                NextBTIsEnabled = false;
            }
            else if (CurrentPage <= 1)
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
        private void LoadList()
        {
            NovelListItem = new ObservableCollection<Model.NovelInfo>();
            var novelList = model.NovelListLoad();
            if (novelList == null) return;

            foreach (var novelInfo in novelList)
            {
                NovelListItem.Add(novelInfo);
            }
            NovelListSelectedIndex = 0;
            LoadNovel(0);
        }
        private void LoadNovel(int index)
        {
            var selectedNovel = NovelListItem[index];

            int page = 1;
            page = model.GetPage(selectedNovel);

            currentNodelInfo = selectedNovel;
            bool isLoaded = model.LoadNovel(selectedNovel);

            if (!isLoaded)
            {
                SubTitleText = "";
                OriginalText = Resources.NeedDownloadText;
                CurrentPageText = (0).ToString();
                MaxPage = 0;
                NextBTIsEnabled = false;
                PreviousBTIsEnabled = false;
            }

            titleList = selectedNovel.Titles;
            textList = selectedNovel.Texts;
            if (textList.Count > 0 && titleList.Count > 0)
            {
                SubTitleText = titleList[page - 1];
                OriginalText = textList[page - 1];
                MaxPage = textList.Count;
                CurrentPageText = page.ToString();
            }

            if (MaxPage <= 1)
            {
                PreviousBTIsEnabled = false;
                NextBTIsEnabled = false;
            }
            else if (CurrentPage >= MaxPage)
            {
                PreviousBTIsEnabled = true;
                NextBTIsEnabled = false;
            }
            else if (CurrentPage <= 1)
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
    }
}
