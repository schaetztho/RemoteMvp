namespace RemoteMvpLib
{
    public interface IActionAdapter
    {
        Task<string> PerformActionAsync(string request);
    }
}