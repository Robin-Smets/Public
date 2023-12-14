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

namespace NovelistBlazor.Common.Service
{
    public class Repository
    {
        #region Properties & members

        private NovelRepository _novelRepository;
        private CharacterRepository _characterRepository;
        private PlotUnitRepository _plotUnitRepository;

        #endregion

        #region Constructor

        public Repository(NovelRepository novelRepository, CharacterRepository characterRepository, PlotUnitRepository plotUnitRepository)
        {
            _novelRepository = novelRepository;
            _characterRepository = characterRepository;
            _plotUnitRepository = plotUnitRepository;
        }

        #endregion

        #region Methods

        public async Task Initialize()
        {
            await _novelRepository.Initialize();
        }

        #endregion

        public async Task NewAsync(RoutedPage routedPage)
        {
            if (routedPage == RoutedPage.Novel)
            {
                await _novelRepository.NewNovelAsync();
            }
            else if (routedPage == RoutedPage.Character)
            {
                await _characterRepository.NewCharacterAsync();
            }
            else if (routedPage == RoutedPage.Plot)
            {
                await _plotUnitRepository.NewPlotUnitAsync();
            }
        }

        public async Task SaveChanges()
        {
            await _novelRepository.UpdateNovelAsync();

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
                
        public async Task LoadPlotUnitsAsync()
        {
            await _plotUnitRepository.LoadPlotUnitsAsync();
        }

        public void SetCurrentNovel(NovelDTO currentNovel)
        {
            _novelRepository.CurrentNovel = currentNovel;
        }

        public void SetCurrentCharacter(CharacterDTO currentCharacter)
        {
            _characterRepository.CurrentCharacter = currentCharacter;
        }

        public void SetCurrentPlotUnit(PlotUnitDTO currentPlotUnit)
        {
            _plotUnitRepository.CurrentPlotUnit = currentPlotUnit;
        }

        public async Task DeleteAsync(RoutedPage routedPage)
        {
            if (routedPage == RoutedPage.Novel)
            {
                // to force clearing elements of their value even if last delete
                _novelRepository.CurrentNovel.Name = string.Empty;
                _novelRepository.CurrentNovel.Description = string.Empty;
                _novelRepository.CurrentNovel.Abstract = string.Empty;

                await _novelRepository.DeleteNovelAsync();
                await _novelRepository.LoadNovelsAsync();
                _novelRepository.CurrentNovel = _novelRepository.AvaiableNovels.FirstOrDefault();
            }
            if (routedPage == RoutedPage.Character)
            {
                await _characterRepository.DeleteCharacterAsync();
                await _characterRepository.LoadCharactersAsync();
                _characterRepository.CurrentCharacter = _characterRepository.AllCurrentCharacters.FirstOrDefault();
            }
            if (routedPage == RoutedPage.Plot)
            {
                await _plotUnitRepository.DeletePlotUnitAsync();
                await _plotUnitRepository.LoadPlotUnitsAsync();
                _plotUnitRepository.CurrentPlotUnit = _plotUnitRepository.AllCurrentPlotUnits.FirstOrDefault();
            }

        }

    }
}
