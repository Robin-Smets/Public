using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelistBlazor.Common.Service
{
    public class AppState : IDisposable
    {
        private readonly EventMediator _eventMediator;

        public AppState(EventMediator eventMediator)
        {
            _eventMediator = eventMediator;
            _eventMediator.OnRepositoryDataChanged += NotifyAppStateChanged;
        }

        private void NotifyAppStateChanged()
        {
            _eventMediator.NotifyAppStateChanged();
        }

        public void Dispose()
        {
            _eventMediator.OnRepositoryDataChanged -= NotifyAppStateChanged;
        }
    }
}
