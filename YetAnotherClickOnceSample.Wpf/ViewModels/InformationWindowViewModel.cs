using System;

using Livet;

namespace YetAnotherClickOnceSample.Wpf.ViewModels
{
    public class InformationWindowViewModel : ViewModel
    {
        public event EventHandler Closed;

        public void Initialize()
        {
            if (this.Info == null)
            {
                this.Info = new ObservableSynchronizedCollection<string>();
            }
        }

        #region Info変更通知プロパティ
        private ObservableSynchronizedCollection<string> _Info;

        public ObservableSynchronizedCollection<string> Info
        {
            get
            { return _Info; }
            set
            {
                if (_Info == value)
                    return;
                _Info = value;
                RaisePropertyChanged("Info");
            }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (this.Closed != null)
            {
                this.Closed.Invoke(this, new EventArgs());
            }
        }

        internal void Close()
        {
            this.Dispose(true);
        }
    }
}
