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
            LoadList();
        }

        private void initialize()
        {
            AddBTClick = new RelayCommand(AddBT_Click);
            NovelListSelectionChanged = new RelayCommand(NovelList_SelectionChanged);
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
        #endregion

        #region EventProperties
        public ICommand AddBTClick { private set; get; }
        public ICommand NovelListSelectionChanged { private set; get; }
        #endregion

        public void AddBT_Click()
        {
            var searchWindow = new View.SearchWindow();
            searchWindow.ShowDialog();
            LoadList();
        }

        public void NovelList_SelectionChanged()
        {
            Console.WriteLine(0);
        }

        private void LoadList()
        {
            NovelListItem.Clear();
            var novelList = model.NovelListLoad();

            foreach (var novelInfo in novelList)
            {
                NovelListItem.Add(novelInfo);
            }
        }
    }
}
