using Microsoft.AspNetCore.Components;

namespace NovelistBlazor.Common.Service
{
    public class EventMediator
    {
        public event Action<RoutedPage, Dictionary<string,string>> OnComponentDataChanged;
        public event Action<Repository> OnRepositoryDataChanged;

        public void NotifyComponentDataChanged(RoutedPage page, Dictionary<string,string> data)
        {
            OnComponentDataChanged?.Invoke(page, data);
        }

        public void NotifyRepositoryDataChanged(Repository repository)
        {
            OnRepositoryDataChanged?.Invoke(repository);
        }

    }
}
