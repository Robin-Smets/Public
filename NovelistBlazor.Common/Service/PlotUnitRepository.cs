using NovelistBlazor.Common.DTO;
using NovelistBlazor.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using NovelistBlazor.Common.Model;

namespace NovelistBlazor.Common.Service
{
    public class PlotUnitRepository : RepositoryBase
    {
        private PlotUnitDTO _currentPlotUnit;
        public PlotUnitDTO CurrentPlotUnit 
        { 
            get => _currentPlotUnit; 
            set
            {
                if (_currentPlotUnit != value)
                {
                    _currentPlotUnit = value;
                }
            }
        }
        
        private List<PlotUnitDTO> _allCurrentPlotUnits;
        public List<PlotUnitDTO> AllCurrentPlotUnits 
        { 
            get => _allCurrentPlotUnits; 
            set
            {
                if (_allCurrentPlotUnits != value)
                {
                    _allCurrentPlotUnits = value;
                }
            }
        }

        public PlotUnitRepository(IHttpClientFactory httpClient, ResponseDeserializer responseDeserializer, EventMediator repositoryEventMediator) : base(httpClient, responseDeserializer, repositoryEventMediator)
        {
            _currentPlotUnit = new PlotUnitDTO();
            _allCurrentPlotUnits = new List<PlotUnitDTO>(); 
        }

        public async Task NewPlotUnitAsync()
        {
                CurrentPlotUnit = new PlotUnitDTO();
                await SavePlotUnitAsync(CurrentPlotUnit);
                await LoadPlotUnitsAsync();
        }

        public async Task<HttpResponseMessage> SavePlotUnitAsync(PlotUnitDTO plotUnitDTO)
        {
            var jsonObject = JsonConvert.SerializeObject(plotUnitDTO);

            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            // Setze den Modus auf 'no-cors'
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7173/PlotUnit/")
            {
                Content = content,
                Method = HttpMethod.Post,
            };

            return await SendRequest(HttpClient, request);
        }

        public async Task<HttpResponseMessage> DeletePlotUnitAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7173/PlotUnit/{CurrentPlotUnit.Id}")
            {
                Method = HttpMethod.Delete,
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await SendRequest(HttpClient, request);
        }

        public async Task<HttpResponseMessage> LoadPlotUnitsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7173/PlotUnit/")
            {
                Method = HttpMethod.Get,
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await SendRequest(HttpClient, request);

            var plotUnits = await ResponseDeserializer.DeserializePlotUnitDTOsAsync(response);

            AllCurrentPlotUnits = plotUnits;

            return response;
        }


        public async Task<HttpResponseMessage> UpdatePlotUnitAsync()
        {
            var plotUnitData = new
            {
                Id = CurrentPlotUnit.Id,
                Title = CurrentPlotUnit.Title,
                Description = CurrentPlotUnit.Description,
                Premise = CurrentPlotUnit.Premise,
                Location = CurrentPlotUnit.Location,
                PlotUnitTypeId = CurrentPlotUnit.PlotUnitTypeId,
                NovelId = CurrentPlotUnit.NovelId
            };

            var jsonPlotUnitData = JsonConvert.SerializeObject(plotUnitData);

            var httpContent = new StringContent(jsonPlotUnitData, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7173/Novel/{plotUnitData.Id}"),
                Content = httpContent,
            };
            
            return await SendRequest(HttpClient, request);
        }
    }
}
