namespace YetAnotherClickOnceSample.Wpf.Models
{
    public enum OutputLevel
    {
        Debug,
        Info,
        Error
    }

    public static class OutputLevelExtention
    {
        public static bool CanOutputDebug(this OutputLevel self)
        {
            return self <= OutputLevel.Debug;
        }

        public static bool CanOutputInfo(this OutputLevel self)
        {
            return self <= OutputLevel.Info;
        }

        public static bool CanOutputError(this OutputLevel self)
        {
            return self <= OutputLevel.Error;
        }
    }

    public class WindowPrinter : IPrinter
    {
        private OutputLevel outputLevel;
        private Installer caller;

        public WindowPrinter(Installer model, OutputLevel outputLevel)
        {
            this.outputLevel = outputLevel;
            this.caller = model;
            this.caller.ClearHasInformed();            
            Notifier.Current.ClearInfo();
            Notifier.Current.ClearError();
        }

        public void Print(string value)
        {
            if (this.outputLevel.CanOutputInfo())
            {
                Notifier.Current.Info.Add(value);
                this.caller.HasInformed = true;
            }
        }

        public void Print(string format, object arg0)
        {
            if (this.outputLevel.CanOutputInfo())
            {
                Notifier.Current.Info.Add(string.Format(format, arg0));
                this.caller.HasInformed = true;
            }
        }

        public void PrintError(string format, object arg0)
        {
            if (this.outputLevel.CanOutputError())
            {
                Notifier.Current.Error = string.Format(format, arg0);
            }
        }

        public void PrintDebug(string value)
        {
            if (this.outputLevel.CanOutputDebug())
            {
                Notifier.Current.Info.Add(value);
                this.caller.HasInformed = true;
            }
        }

        public void PrintDebug(string format, object arg0, object arg1, object arg2, object arg3)
        {
            if (this.outputLevel.CanOutputDebug())
            {
                Notifier.Current.Info.Add(string.Format(format, arg0, arg1, arg2, arg3));
                this.caller.HasInformed = true;
            }
        }
    }
}
