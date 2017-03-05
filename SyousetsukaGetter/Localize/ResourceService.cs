using System.ComponentModel;
using System.Globalization;

namespace LanguageEx
{
    /// <summary>
    /// 多言語化されたリソースと、言語の切り替え機能を提供します。
    /// </summary>
    public class ResourceService : INotifyPropertyChanged
    {
        #region singleton members

        private static readonly ResourceService _current = new ResourceService();
        public static ResourceService Current
        {
            get { return _current; }
        }

        #endregion

        private readonly SyousetsukaGetter.Localize.Resources.Resources _resources = new SyousetsukaGetter.Localize.Resources.Resources();
        private readonly SyousetsukaGetter.Localize.Resources.SearchResources _searchResources = new SyousetsukaGetter.Localize.Resources.SearchResources();


        /// <summary>
        /// 多言語化されたリソースを取得します。
        /// </summary>
        public SyousetsukaGetter.Localize.Resources.Resources Resources
        {
            get { return this._resources; }
        }
        public SyousetsukaGetter.Localize.Resources.SearchResources SearchResources
        {
            get { return this._searchResources; }
        }

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        /// <summary>
        /// 指定されたカルチャ名を使用して、リソースのカルチャを変更します。
        /// </summary>
        /// <param name="name">カルチャの名前。</param>
        public void ChangeCulture(string name)
        {
            SyousetsukaGetter.Localize.Resources.Resources.Culture = CultureInfo.GetCultureInfo(name);

            this.RaisePropertyChanged("Resources");
            this.RaisePropertyChanged("RearchResources");
        }

        public string GetCulture()
        {
            if (SyousetsukaGetter.Localize.Resources.Resources.Culture != null)
            {
                return SyousetsukaGetter.Localize.Resources.Resources.Culture.Name;
            }
            else
            {
                return CultureInfo.CurrentCulture.Name;
            }
        }
    }
}
