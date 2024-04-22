

public class ThreadChecker
{
    private static int mainThreadId;

    static ThreadChecker()
    {
        mainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
    }

    public static bool IsMainThread()
    {
        return mainThreadId == System.Threading.Thread.CurrentThread.ManagedThreadId;
    }
}