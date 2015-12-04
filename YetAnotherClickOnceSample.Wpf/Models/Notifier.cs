using Livet;

namespace YetAnotherClickOnceSample.Wpf.Models
{
    public class Notifier : NotificationObject
    {
        #region Singleton property

        public static Notifier Current { get; set; }
        
        #endregion

        static Notifier()
        {
            Notifier.Current = new Notifier();
            Notifier.Current.Info = new ObservableSynchronizedCollection<string>();
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

        public void ClearInfo()
        {
            if (this._Info != null)
                this._Info.Clear();
        }
        
        #region Warning変更通知プロパティ
        private string _Warning;

        public string Warning
        {
            get
            { return _Warning; }
            set
            { 
                if (_Warning == value)
                    return;
                _Warning = value;
                RaisePropertyChanged("Warning");
            }
        }
        #endregion

        public void ClearWarning()
        {
            this._Warning = string.Empty;
        }
        
        #region Error変更通知プロパティ
        private string _Error;

        public string Error
        {
            get
            { return _Error; }
            set
            { 
                if (_Error == value)
                    return;
                _Error = value;
                RaisePropertyChanged("Error");
            }
        }
        #endregion

        public void ClearError()
        {
            this._Error = string.Empty;
        }
    }
}
