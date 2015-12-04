using Livet;

namespace YetAnotherClickOnceSample.Wpf.Models
{
    public class Installer : NotificationObject
    {
        private MyInstaller installer;
        
        public Installer()
        {
            var printer = new WindowPrinter(this, OutputLevel.Info);
            this.installer = new MyInstaller(printer);
        }

        public void Install(string deployManifestUriStr)
        {
            this.installer.InstallApplication(deployManifestUriStr);
        }
        
        #region HasInformed変更通知プロパティ
        private bool _HasInformed;

        public bool HasInformed
        {
            get
            { return _HasInformed; }
            set
            { 
                if (_HasInformed == value)
                    return;
                _HasInformed = value;
                RaisePropertyChanged("HasInformed");
            }
        }
        #endregion

        public void ClearHasInformed()
        {
            this._HasInformed = false;
        }
    }
}
