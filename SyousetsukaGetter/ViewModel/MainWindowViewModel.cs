using SyousetsukaGetter.ExMessageBox;
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
            MinusBTClick = new RelayCommand(MinusBT_Click);
            NovelListSelectionChanged = new RelayCommand(NovelList_SelectionChanged);
            NextPageBTClick = new RelayCommand(NextPageBT_Click);
            PreviousPageBTClick = new RelayCommand(PreviousPageBT_Click);
            DownloadBTClick = new RelayCommand(DownloadBT_Click);
            CurrentPageKeyDown = new RelayCommand<KeyEventArgs>(CurrentPage_KeyDown);
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

        string currentPageText = string.Empty;
        public string CurrentPageText
        {
            set
            {

                currentPageText = value;
                
                int page = 1;
                int.TryParse(value, out page);
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
        #endregion

        #region EventProperties
        public ICommand AddBTClick { private set; get; }
        public ICommand MinusBTClick { private set; get; }
        public ICommand NovelListSelectionChanged { private set; get; }
        public ICommand NextPageBTClick { private set; get; }
        public ICommand PreviousPageBTClick { private set; get; }
        public ICommand DownloadBTClick { private set; get; }

        public ICommand CurrentPageKeyDown { private set; get; }
        #endregion


        public void AddBT_Click()
        {
            var searchWindow = new View.SearchWindow();
            searchWindow.ShowDialog();
            loadList();
        }
        public void MinusBT_Click()
        {
            int index = NovelListSelectedIndex;
            if (index < 0) return;

            var novelInfo = NovelListItem[index];

            string alertTitle = "小説の削除";
            string alertMessage = novelInfo.Title + " を削除します。よろしいですか？";
            ExMessageBoxBase.DialogResult dr = ExMessageBoxBase.Show(view, alertMessage, alertTitle, ExMessageBoxBase.MessageType.Asterisk, ExMessageBoxBase.ButtonType.YesNo);
            if (dr == ExMessageBoxBase.DialogResult.No)
            {
                return;
            }

            NovelListItem.RemoveAt(index);
            model.RemoveNovel(novelInfo);
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
            model.DownloadNovel(NovelListItem[NovelListSelectedIndex]);
            loadNovel(NovelListSelectedIndex);
        }

        public void CurrentPage_KeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int index = CurrentPage;
                if (index > MaxPage || index <= 0) return;

                string text = textList[index - 1];
                string title = titleList[index - 1];
                OriginalText = text;
                SubTitleText = title;

                if (CurrentPage >= MaxPage)
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
        private void loadNovel(int index)
        {
            var selectedNovel = NovelListItem[index];
            bool isLoaded = model.LoadNovel(selectedNovel);

            if (!isLoaded)
            {
                SubTitleText = "";
                OriginalText = "ダウンロードボタンよりダウンロードしてください。";
                CurrentPage = 0;
                MaxPage = 0;
                NextBTIsEnabled = false;
                PreviousBTIsEnabled = false;
            }

            titleList = selectedNovel.Titles;
            textList = selectedNovel.Texts;
            if (textList.Count > 0 && titleList.Count > 0)
            {
                SubTitleText = titleList[0];
                OriginalText = textList[0];
                MaxPage = textList.Count;
                CurrentPageText = (1).ToString();
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
