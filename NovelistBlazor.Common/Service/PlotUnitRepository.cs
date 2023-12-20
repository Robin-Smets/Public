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


        public PlotUnitRepository(IHttpClientFactory httpClient, ResponseDeserializer responseDeserializer, EventMediator repositoryEventMediator, DataFactory dataFactory) : base(httpClient, responseDeserializer, repositoryEventMediator, dataFactory)
        {

        }

        public async Task<HttpResponseMessage> NewPlotUnitAsync(int novelId)
        {
            var plotUnit = DataFactory.CreatePlotUnitDTO();
            plotUnit.Title = "New plot unit";
            plotUnit.NovelId = novelId;
            plotUnit.PlotUnitTypeId = 1;
            await SavePlotUnitAsync(plotUnit);
            return await LoadPlotUnitsAsync();
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

        public async Task<HttpResponseMessage> DeletePlotUnitAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7173/PlotUnit/{id}")
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

            return response;
        }


        public async Task<HttpResponseMessage> UpdatePlotUnitAsync(PlotUnitDTO plotUnit)
        {
            var plotUnitData = new
            {
                Id = plotUnit.Id,
                Title = plotUnit.Title,
                Description = plotUnit.Description,
                Premise = plotUnit.Premise,
                Location = plotUnit.Location,
                PlotUnitTypeId = plotUnit.PlotUnitTypeId,
                NovelId = plotUnit.NovelId
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
