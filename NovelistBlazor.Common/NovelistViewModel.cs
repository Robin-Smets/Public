using System.Windows.Input;
using Microsoft.AspNetCore.Components;
using NovelistBlazor.Common;
using NovelistBlazor.Common.DTO;
using NovelistBlazor.Common.Model;
using NovelistBlazor.Common.Service;

namespace NovelistBlazor.Common
{
    public class NovelistViewModel : IDisposable
    {
        #region Properties and members

        private NovelDTO _currentNovel;
        public NovelDTO CurrentNovel
        {
            get => _currentNovel;
            set
            {
                _currentNovel = value;
                NotifyDataChanged();
            }
        }

        private List<NovelDTO> _allNovels;
        public List<NovelDTO> AllNovels
        {
            get => _allNovels;
            set
            {
                _allNovels = value;
                NotifyDataChanged(); 
            }
        }

        private CharacterDTO _currentCharacter;
        public CharacterDTO CurrentCharacter
        {
            get => _currentCharacter;
            set
            {
                _currentCharacter = value;
                NotifyDataChanged(); 
            }
        }

        private List<CharacterDTO> _allCurrentCharacters;
        public List<CharacterDTO> AllCurrentCharacters
        {
            get => _allCurrentCharacters;
            set
            {
                _allCurrentCharacters = value;
                NotifyDataChanged(); 
            }
        }

        private PlotUnitDTO _currentPlotUnit;
        public PlotUnitDTO CurrentPlotUnit
        {
            get => _currentPlotUnit;
            set
            {
                _currentPlotUnit = value;
                NotifyDataChanged(); 
            }
        }

        private List<PlotUnitDTO> _allCurrentPlotUnits;
        private bool disposedValue;

        public List<PlotUnitDTO> AllCurrentPlotUnits
        {
            get => _allCurrentPlotUnits;
            set
            {
                _allCurrentPlotUnits = value;
                NotifyDataChanged(); 
            }
        }

        public NavigationManager NavigationManager { get; set; }
        
        public event Action OnDataChanged;
        
        public bool Initialized { get; private set; }

        private void NotifyDataChanged() => OnDataChanged?.Invoke();

        private readonly Repository _repository;
        private readonly RepositoryEventMediator _repositoryEventMediator;
        
        #endregion

        #region Constructor

        public NovelistViewModel(Repository repository, RepositoryEventMediator repositoryEventMediator)
        {
            _repository = repository;
            _repositoryEventMediator = repositoryEventMediator;

            _currentNovel = new NovelDTO();
            _repositoryEventMediator.CurrentNovelChanged += _repositoryEventMediator_CurrentNovelChanged;
            _allNovels = new List<NovelDTO>();
            _repositoryEventMediator.AvaiableNovelsChanged += _repositoryEventMediator_AvaiableNovelsChanged;

            _currentCharacter = new CharacterDTO();
            _repositoryEventMediator.CurrentCharacterChanged += _repositoryEventMediator_CurrentCharacterChanged;
            _allCurrentCharacters = new List<CharacterDTO>();
            _repositoryEventMediator.AllCurrentCharactersChanged += _repositoryEventMediator_AllCurrentCharactersChanged;

            _currentPlotUnit = new PlotUnitDTO();
            _repositoryEventMediator.CurrentPlotUnitChanged += _repositoryEventMediator_CurrentPlotUnitChanged;
            _allCurrentPlotUnits = new List<PlotUnitDTO>();
            _repositoryEventMediator.AllCurrentPlotUnitsChanged += _repositoryEventMediator_AllCurrentPlotUnitsChanged;

            Initialized = false;
        }
        
        #endregion

        #region Event handlers

        private void _repositoryEventMediator_AllCurrentPlotUnitsChanged(object? sender, List<PlotUnitDTO> e)
        {
            if (e != null)
            {
                AllCurrentPlotUnits = e.ToList();
            }

        }

        private void _repositoryEventMediator_CurrentPlotUnitChanged(object? sender, PlotUnitDTO e)
        {
            if (e != null)
            {
                CurrentPlotUnit = e;
            }
        }

        private void _repositoryEventMediator_AllCurrentCharactersChanged(object? sender, List<CharacterDTO> e)
        {
            if (e != null)
            {
                AllCurrentCharacters = e.ToList();
            }

        }

        private void _repositoryEventMediator_CurrentCharacterChanged(object? sender, CharacterDTO e)
        {
            if (e != null)
            {
                CurrentCharacter = e;
            }

        }

        private void _repositoryEventMediator_AvaiableNovelsChanged(object? sender, List<NovelDTO> e)
        {
            if (e != null)
            {
                AllNovels = e.ToList();
            }

        }

        private void _repositoryEventMediator_CurrentNovelChanged(object? sender, NovelDTO e)
        {
            if (e != null)
            {
                CurrentNovel = e;
            }

        }

        #endregion

        #region Bound to UI elements

        public async Task OnNew()
        {
            var routedPage = GetRoutedPage();

            try
            {
                await _repository.NewAsync(routedPage);
                OnDataChanged();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task OnSave()
        {
            try
            {
                await _repository.SaveChanges();
                NotifyDataChanged();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void OnComboBoxChanged(DTOType dtoType, int id)
        {
            if(dtoType == DTOType.Novel)
            {
                CurrentNovel = AllNovels.FirstOrDefault(n => n.Id == id);
                _repository.SetCurrentNovel(CurrentNovel);
            }
           else if(dtoType == DTOType.Character)
            {
                CurrentCharacter = AllCurrentCharacters.FirstOrDefault(n => n.Id == id);
                _repository.SetCurrentCharacter(CurrentCharacter);
            }            
           else if(dtoType == DTOType.PlotUnit)
            {
                CurrentPlotUnit = AllCurrentPlotUnits.FirstOrDefault(n => n.Id == id);
                _repository.SetCurrentPlotUnit(CurrentPlotUnit);
            }  
        }

        public async Task OnDelete()
        {
            var routedPage = GetRoutedPage();

            try
            {
                await _repository.DeleteAsync(routedPage);

                NotifyDataChanged();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        public void OnExport()
        {

        }

        public void OnMinimize()
        {

        }

        public void OnExpand()
        {

        }

        public void OnExit()
        {

        }

        #endregion

        #region Methods

        public async Task InitializeAsync()
        {
            if (Initialized) return;

            await _repository.Initialize();
            NotifyDataChanged(); 

            Initialized = true;
        }

        private RoutedPage GetRoutedPage()
        {
            if (NavigationManager.Uri == NavigationManager.BaseUri)
            {
                return RoutedPage.Home;
            }
            else if (NavigationManager.Uri.Contains("/novel", StringComparison.OrdinalIgnoreCase))
            {
                return RoutedPage.Novel;
            }
            else if (NavigationManager.Uri.Contains("/character", StringComparison.OrdinalIgnoreCase))
            {
                return RoutedPage.Character;
            }
            else if (NavigationManager.Uri.Contains("/plot", StringComparison.OrdinalIgnoreCase))
            {
                return RoutedPage.Plot;
            }
            else if (NavigationManager.Uri.Contains("/text", StringComparison.OrdinalIgnoreCase))
            {
                return RoutedPage.Text;
            }
            else
            {
                return RoutedPage.Unexpected;
            }
        }

        #endregion

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                _repositoryEventMediator.CurrentNovelChanged += _repositoryEventMediator_CurrentNovelChanged;
                _repositoryEventMediator.AvaiableNovelsChanged += _repositoryEventMediator_AvaiableNovelsChanged;
                _repositoryEventMediator.CurrentCharacterChanged += _repositoryEventMediator_CurrentCharacterChanged;
                _repositoryEventMediator.AllCurrentCharactersChanged += _repositoryEventMediator_AllCurrentCharactersChanged;
                _repositoryEventMediator.CurrentPlotUnitChanged += _repositoryEventMediator_CurrentPlotUnitChanged;
                _repositoryEventMediator.AllCurrentPlotUnitsChanged += _repositoryEventMediator_AllCurrentPlotUnitsChanged;

                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~NovelistViewModel()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
