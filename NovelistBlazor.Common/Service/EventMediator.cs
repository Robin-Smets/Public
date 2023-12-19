public class EventMediator
{
    public event Action OnAppStateChanged;
    public event Action OnRepositoryDataChanged;

    public void NotifyAppStateChanged()
    {
        OnAppStateChanged?.Invoke();
    }

    public void NotifyRepositoryDataChanged()
    {
        OnRepositoryDataChanged?.Invoke();
    }
}
