namespace RemoteMvpLib
{
    public interface IActionAdapter
    {
        Task<RemoteActionResponse> PerformActionAsync(RemoteActionRequest request);
    }
}