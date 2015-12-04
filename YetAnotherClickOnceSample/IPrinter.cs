namespace YetAnotherClickOnceSample
{
    public interface IPrinter
    {
        void Print(string value);
        void Print(string format, object arg0);
        void PrintError(string format, object arg0);
        void PrintDebug(string value);
        void PrintDebug(string format, object arg0, object arg1, object arg2, object arg3);
    }
}
