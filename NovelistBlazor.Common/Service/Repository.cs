using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NovelistBlazor.Common.DTO;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using NovelistBlazor.Common.Interface;
using Microsoft.AspNetCore.Components;

namespace NovelistBlazor.Common.Service
{
    public class Repository
    {
        #region Properties & members

        private NovelDTO _currentNovel;
        public NovelDTO? CurrentNovel
        {
            get => _currentNovel;
            set
            {
                _currentNovel = value;
                _eventMediator.NotifyRepositoryDataChanged();
            }
        }

        private List<NovelDTO> _allNovels;
        public List<NovelDTO>? AllNovels
        {
            get => _allNovels;
            set
            {
                _allNovels = value;
                _eventMediator.NotifyRepositoryDataChanged(); 
            }
        }

        private CharacterDTO _currentCharacter;
        public CharacterDTO? CurrentCharacter
        {
            get => _currentCharacter;
            set
            {
                _currentCharacter = value;
                _eventMediator.NotifyRepositoryDataChanged(); 
            }
        }

        private List<CharacterDTO> _allCurrentCharacters;
        public List<CharacterDTO>? AllCurrentCharacters
        {
            get => _allCurrentCharacters;
            set
            {
                _allCurrentCharacters = value;
                _eventMediator.NotifyRepositoryDataChanged(); 
            }
        }

        private PlotUnitDTO _currentPlotUnit;
        public PlotUnitDTO? CurrentPlotUnit
        {
            get => _currentPlotUnit;
            set
            {
                _currentPlotUnit = value;
                _eventMediator.NotifyRepositoryDataChanged(); 
            }
        }

        private List<PlotUnitDTO> _allCurrentPlotUnits;
        public List<PlotUnitDTO>? AllCurrentPlotUnits
        {
            get => _allCurrentPlotUnits;
            set
            {
                _allCurrentPlotUnits = value;
                _eventMediator.NotifyRepositoryDataChanged(); 
            }
        }

        private PageAnalyzer _pageAnalyzer;
        private NovelRepository _novelRepository;
        private CharacterRepository _characterRepository;
        private PlotUnitRepository _plotUnitRepository;
        private EventMediator _eventMediator;
        private ResponseDeserializer _responseDeserializer;

        #endregion

        #region Constructor

        public Repository(NovelRepository novelRepository, CharacterRepository characterRepository, PlotUnitRepository plotUnitRepository, PageAnalyzer pageAnalyzer, EventMediator eventMediator, ResponseDeserializer responseDeserializer)
        {
            _novelRepository = novelRepository;
            _characterRepository = characterRepository;
            _plotUnitRepository = plotUnitRepository;
            _pageAnalyzer = pageAnalyzer;
            _eventMediator = eventMediator;
            _responseDeserializer = responseDeserializer;
        }

        #endregion

        #region Methods

        public async Task LoadNovelsAsync()
        {
            var response = await _novelRepository.LoadNovelsAsync();

            if (response != null)
            {
                AllNovels = await _responseDeserializer.DeserializeNovelDTOsAsync(response);
            }
            
            CurrentNovel = AllNovels.FirstOrDefault();
        }

        #endregion

        public async Task NewAsync()
        {
            if (_pageAnalyzer.GetRoutedPage() == RoutedPage.Novel)
            {
                var response = await _novelRepository.NewNovelAsync();

                if (response != null)
                {
                    AllNovels = await _responseDeserializer.DeserializeNovelDTOsAsync(response);
                }
            
                CurrentNovel = AllNovels?.OrderByDescending(x => x.Id).FirstOrDefault();
            }
            else if (_pageAnalyzer.GetRoutedPage() == RoutedPage.Character)
            {
                await _characterRepository.NewCharacterAsync();
            }
            else if (_pageAnalyzer.GetRoutedPage() == RoutedPage.Plot)
            {
                await _plotUnitRepository.NewPlotUnitAsync();
            }
        }

        public async Task SaveAsync()
        {
            //await _novelRepository.UpdateNovelAsync();

            // Update Characters
            foreach (var character in _characterRepository.AllCurrentCharacters)
            {
                _characterRepository.CurrentCharacter = character;
                await _characterRepository.UpdateCharacterAsync();
            }

            // Update PlotUnits
            foreach (var plotUnit in _plotUnitRepository.AllCurrentPlotUnits)
            {
                _plotUnitRepository.CurrentPlotUnit = plotUnit;
                await _plotUnitRepository.UpdatePlotUnitAsync();
            }
        }

        public async Task LoadNovelAsync(int id)
        {

        }

        public async Task LoadPlotUnitsAsync()
        {
            await _plotUnitRepository.LoadPlotUnitsAsync();
        }

        public async Task DeleteAsync()
        {
            if (CurrentNovel == null) return;

            if (_pageAnalyzer.GetRoutedPage() == RoutedPage.Novel)
            {
                await _novelRepository.DeleteNovelAsync(CurrentNovel.Id);
                await LoadNovelsAsync();
                // to force clearing elements of their value even if last delete
                /*
                CurrentNovel.Name = string.Empty;
                CurrentNovel.Description = string.Empty;
                CurrentNovel.Abstract = string.Empty;
                */
                //await _novelRepository.DeleteNovelAsync();
                //await _novelRepository.LoadNovelsAsync();
                //_novelRepository.CurrentNovel = _novelRepository.AvaiableNovels.FirstOrDefault();
            }
            if (_pageAnalyzer.GetRoutedPage() == RoutedPage.Character)
            {
                await _characterRepository.DeleteCharacterAsync();
                await _characterRepository.LoadCharactersAsync();
                _characterRepository.CurrentCharacter = _characterRepository.AllCurrentCharacters.FirstOrDefault();
            }
            if (_pageAnalyzer.GetRoutedPage() == RoutedPage.Plot)
            {
                await _plotUnitRepository.DeletePlotUnitAsync();
                await _plotUnitRepository.LoadPlotUnitsAsync();
                _plotUnitRepository.CurrentPlotUnit = _plotUnitRepository.AllCurrentPlotUnits.FirstOrDefault();
            }

        }

    }
}
