using System;
using System.Collections.Generic;
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
        public MainWindowViewModel(Window view) : base(view)
        {
            initialize();
        }

        private void initialize()
        {
            AddBTClick = new RelayCommand(AddBT_Click);
        }

        public ICommand AddBTClick { private set; get; }

        public void AddBT_Click()
        {
            var searchWindow = new View.SearchWindow();
            searchWindow.ShowDialog();
        }
    }
}
