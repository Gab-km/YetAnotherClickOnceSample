using System;
using System.ComponentModel;

using Livet;
using Livet.Messaging;
using Livet.EventListeners;

using YetAnotherClickOnceSample.Wpf.Views;
using YetAnotherClickOnceSample.Wpf.Models;

namespace YetAnotherClickOnceSample.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private Installer installer;
        private InformationWindowViewModel informationWindow;

        public void Initialize()
        {
            this.CompositeDisposable.Add(new PropertyChangedEventListener(Notifier.Current)
            {
                { "Warning", new PropertyChangedEventHandler((sender, e) => NotifyWarning(Notifier.Current.Warning)) },
                { "Error", new PropertyChangedEventHandler((sender, e) => NotifyError(Notifier.Current.Error)) }
            });
            this.installer = new Installer();
            this.CompositeDisposable.Add(new PropertyChangedEventListener(this.installer)
            {
                { "HasInformed", new PropertyChangedEventHandler((sender, e) => this.HasInformed = this.installer.HasInformed) }
            });
        }
        
        #region DownloadUri変更通知プロパティ
        private string _DownloadUri;

        public string DownloadUri
        {
            get
            { return _DownloadUri; }
            set
            { 
                if (_DownloadUri == value)
                    return;
                _DownloadUri = value;
                RaisePropertyChanged("DownloadUri");
            }
        }
        #endregion

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
        
        public void Install()
        {
            if (string.IsNullOrEmpty(this.DownloadUri))
            {
                Notifier.Current.Warning = "Download URIが未入力です。";
            }
            else
            {
                this.installer.Install(this.DownloadUri);
            }
        }

        private void NotifyWarning(string message)
        {
            Messenger.RaiseAsync(new InformationMessage(
                message,
                "Warning",
                System.Windows.MessageBoxImage.Warning,
                "Dialog"
                ));
        }

        private void NotifyError(string message)
        {
            Messenger.RaiseAsync(new InformationMessage(
                message,
                "Error",
                System.Windows.MessageBoxImage.Error,
                "Dialog"
                ));
        }

        public void ShowInformationWindow()
        {
            this.informationWindow = new InformationWindowViewModel();
            this.informationWindow.Closed += informationWindow_Closed;
            this.informationWindow.Info = Notifier.Current.Info;

            this.Transit(typeof(InformationWindow), this.informationWindow, TransitionMode.Modal, true);
        }

        void informationWindow_Closed(object sender, EventArgs e)
        {
            if (this.informationWindow != null)
            {
                this.informationWindow.Closed -= this.informationWindow_Closed;
                this.informationWindow.Close();
                this.informationWindow = null;
            }
        }

        // ref. from https://github.com/Grabacr07/MetroTrilithon/blob/beddfdd2c8d124d6419888ee9311cd519264758b/source/MetroTrilithon.Desktop/Mvvm/WindowViewModel.cs#L172
        // MetroTrilithon is released under the term of MIT license.
        // Copyright (c) 2015 Manato KAMEYA alrights reserved.
        private void Transit(Type windowType, ViewModel viewModel, TransitionMode mode, bool isOwnedView)
        {
            this.Messenger.Raise(new TransitionMessage(
                windowType,
                viewModel,
                mode,
                isOwnedView ? "Window.Transition.Child" : "Window.Transition"
                ));
        }
    }
}
