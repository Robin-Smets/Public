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

        public NovelDTO? CurrentNovel { get; set; }
        public List<NovelDTO>? AllNovels { get; set; }
        public CharacterDTO? CurrentCharacter { get; set; }
        public List<CharacterDTO>? AllCurrentCharacters { get; set; }
        public PlotUnitDTO? CurrentPlotUnit { get; set; }
        public List<PlotUnitDTO>? AllCurrentPlotUnits { get; set; }

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

            _eventMediator.OnComponentDataChanged += _eventMediator_OnComponentDataChanged;
        }

        private void _eventMediator_OnComponentDataChanged(RoutedPage page, Dictionary<string, string> data)
        {
            if (page == RoutedPage.Novel)
            {
                CurrentNovel.Name = data["NovelName"];
                CurrentNovel.Description = data["NovelDescription"];
                CurrentNovel.Abstract = data["NovelAbstract"];
            }
            else if (page == RoutedPage.Character)
            {
                CurrentCharacter.Name = data["CharacterName"];
                CurrentCharacter.Age = int.Parse(data["CharacterAge"]);
                CurrentCharacter.Occupation = data["CharacterOccupation"];
                CurrentCharacter.RoleInStory = data["CharacterRoleInStory"];
                CurrentCharacter.PhysicalDescription = data["CharacterPhysicalDescription"];
                CurrentCharacter.PersonalityTraits = data["CharacterPersonalityTraits"];
                CurrentCharacter.Background = data["CharacterBackground"];
                CurrentCharacter.GoalsAndMotivations = data["CharacterGoalsAndMotivations"];
                CurrentCharacter.CharacterArc = data["CharacterCharacterArc"];
            }
            else if (page == RoutedPage.Plot)
            {
                CurrentPlotUnit.Title = data["PlotUnitTitle"];
                CurrentPlotUnit.Description = data["PlotUnitDescription"];
                CurrentPlotUnit.Premise = data["PlotUnitPremise"];
                CurrentPlotUnit.Location = data["PlotUnitLocation"];
            }
        }


        #endregion

        #region Methods

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
                await LoadCurrentCharactersAsync();
                await LoadCurrentPlotUnitsAsync();

                _eventMediator.NotifyRepositoryDataChanged(this);


            }
            else if (_pageAnalyzer.GetRoutedPage() == RoutedPage.Character && CurrentNovel is not null)
            {
                var response = await _characterRepository.NewCharacterAsync(CurrentNovel.Id);

                if (response != null)
                {
                    await LoadCurrentCharactersAsync();
                }

                CurrentCharacter = AllCurrentCharacters?.OrderByDescending(x => x.Id).FirstOrDefault();

                _eventMediator.NotifyRepositoryDataChanged(this);
            }
            else if (_pageAnalyzer.GetRoutedPage() == RoutedPage.Plot && CurrentNovel is not null)
            {
                var response = await _plotUnitRepository.NewPlotUnitAsync(CurrentNovel.Id);

                if (response != null)
                {
                    await LoadCurrentPlotUnitsAsync();
                }

                CurrentPlotUnit = AllCurrentPlotUnits?.OrderByDescending(x => x.Id).FirstOrDefault();

                _eventMediator.NotifyRepositoryDataChanged(this);
            }
        }

        public async Task SaveAsync()
        {
            if (CurrentNovel is null) return;

            await _novelRepository.UpdateNovelAsync(CurrentNovel);

            // Update Characters
            foreach (var character in AllCurrentCharacters)
            {
                await _characterRepository.UpdateCharacterAsync(character);
            }
            // Update PlotUnits
            foreach (var plotUnit in AllCurrentPlotUnits)
            {
                await _plotUnitRepository.UpdatePlotUnitAsync(plotUnit);
            }

            _eventMediator.NotifyRepositoryDataChanged(this);
        }

        public async Task SetCurrentNovelAsync(int id)
        {
            CurrentNovel = AllNovels?.FirstOrDefault(x => x.Id == id);
            AllCurrentCharacters?.Clear();
            AllCurrentPlotUnits?.Clear();
            await LoadCurrentCharactersAsync();
            await LoadCurrentPlotUnitsAsync();
            _eventMediator.NotifyRepositoryDataChanged(this);
        }

        public async Task SetCurrentCharacterAsync(int id)
        {
            CurrentCharacter = AllCurrentCharacters?.FirstOrDefault(x => x.Id == id);
            _eventMediator.NotifyRepositoryDataChanged(this);
        }

        public async Task SetCurrentPlotUnitAsync(int id)
        {
            CurrentPlotUnit = AllCurrentPlotUnits?.FirstOrDefault(x => x.Id == id);
            _eventMediator.NotifyRepositoryDataChanged(this);
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
            }
            if (_pageAnalyzer.GetRoutedPage() == RoutedPage.Character)
            {
                await _characterRepository.DeleteCharacterAsync(CurrentCharacter.Id);
                await LoadCurrentCharactersAsync();
            }
            if (_pageAnalyzer.GetRoutedPage() == RoutedPage.Plot)
            {
                await _plotUnitRepository.DeletePlotUnitAsync(CurrentPlotUnit.Id);
                await LoadCurrentPlotUnitsAsync();
            }

            _eventMediator.NotifyRepositoryDataChanged(this);
        }

        public async Task LoadNovelsAsync()
        {
            var response = await _novelRepository.LoadNovelsAsync();

            if (response != null)
            {
                AllNovels = await _responseDeserializer.DeserializeNovelDTOsAsync(response);
            }
            
            CurrentNovel = AllNovels.FirstOrDefault();

            _eventMediator.NotifyRepositoryDataChanged(this);
        }

        public async Task LoadCurrentCharactersAsync()
        {
            var response = await _characterRepository.LoadCharactersAsync();

            if (response != null)
            {
                var characters = await _responseDeserializer.DeserializeCharacterDTOsAsync(response);
                AllCurrentCharacters =  characters.Where(x => x.NovelId == CurrentNovel.Id).ToList();
            }
            
            CurrentCharacter = AllCurrentCharacters.FirstOrDefault();

            _eventMediator.NotifyRepositoryDataChanged(this);
        }

        public async Task LoadCurrentPlotUnitsAsync()
        {
            var response = await _plotUnitRepository.LoadPlotUnitsAsync();

            if (response != null)
            {
                var plotUnits = await _responseDeserializer.DeserializePlotUnitDTOsAsync(response);
                AllCurrentPlotUnits =  plotUnits.Where(x => x.NovelId == CurrentNovel.Id).ToList();
            }
            
            CurrentPlotUnit = AllCurrentPlotUnits.FirstOrDefault();

            _eventMediator.NotifyRepositoryDataChanged(this);
        }

        #endregion
    }
}
