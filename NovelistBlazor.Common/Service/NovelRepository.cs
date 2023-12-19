using NovelistBlazor.Common.DTO;
using NovelistBlazor.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace NovelistBlazor.Common.Service
{
    public class NovelRepository : RepositoryBase
    {
        #region Properties & members

        #endregion

        #region Constructor

        public NovelRepository(IHttpClientFactory httpClient, ResponseDeserializer responseDeserializer, EventMediator repositoryEventMediator) : base(httpClient, responseDeserializer, repositoryEventMediator)
        {

        }

        #endregion

        #region Methods

        #endregion

        public async Task<HttpResponseMessage> NewNovelAsync()
        {
            var novel = new NovelDTO();
            novel.Name = "New novel";
            await SaveNovelAsync(novel);
            return await LoadNovelsAsync();
        }

        public async Task<HttpResponseMessage> SaveNovelAsync(NovelDTO novelDTO)
        {
            var jsonObject = JsonConvert.SerializeObject(novelDTO);

            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            // Setze den Modus auf 'no-cors'
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7173/Novel/")
            {
                Content = content,
                Method = HttpMethod.Post,
            };

            return await SendRequest(HttpClient, request);
        }

        public async Task<HttpResponseMessage> DeleteNovelAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7173/Novel/{id}")
            {
                Method = HttpMethod.Delete,
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await SendRequest(HttpClient, request);
        }

        public async Task<HttpResponseMessage> LoadNovelsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7173/Novel/")
            {
                Method = HttpMethod.Get,
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await SendRequest(HttpClient, request);

            return response;
        }


        public async Task<HttpResponseMessage> UpdateNovelAsync(NovelDTO novel)
        {
            var novelData = new
            {
                Id = novel.Id,
                Name = novel.Name,
                Description = novel.Description,
                Abstract = novel.Abstract
            };

            var jsonNovelData = JsonConvert.SerializeObject(novelData);

            var httpContent = new StringContent(jsonNovelData, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7173/Novel/{novelData.Id}"),
                Content = httpContent,
            };
            
            return await SendRequest(HttpClient, request);
        }
    }
}
