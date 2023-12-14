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

        private NovelDTO _currentNovel;
        public NovelDTO CurrentNovel 
        { 
            get => _currentNovel; 
            set
            {
                if (_currentNovel != value)
                {
                    _currentNovel = value;
                    RepositoryEventMediator.OnCurrentNovelChanged(_currentNovel);
                }
            }
        }
        
        private List<NovelDTO> _avaiableNovels;
        public List<NovelDTO> AvaiableNovels 
        { 
            get => _avaiableNovels; 
            set
            {
                if (_avaiableNovels != value)
                {
                    _avaiableNovels = value;
                    RepositoryEventMediator.OnAvaiableNovelsChanged(_avaiableNovels);
                }
            }
        }

        #endregion

        #region Constructor

        public NovelRepository(IHttpClientFactory httpClient, ResponseDeserializer responseDeserializer, RepositoryEventMediator repositoryEventMediator) : base(httpClient, responseDeserializer, repositoryEventMediator)
        {
            _currentNovel = new NovelDTO();
            _avaiableNovels = new List<NovelDTO>(); 
        }

        #endregion

        #region Methods

        public async Task Initialize()
        {
            await LoadNovelsAsync();
            CurrentNovel = AvaiableNovels.FirstOrDefault();
        }

        #endregion

        public async Task NewNovelAsync()
        {
                var novel = new NovelDTO();
                novel.Name = "New novel";
                await SaveNovelAsync(novel);
                await LoadNovelsAsync();
                CurrentNovel = AvaiableNovels.OrderByDescending(x => x.Id).First();
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

        public async Task<HttpResponseMessage> DeleteNovelAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7173/Novel/{CurrentNovel.Id}")
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

            var novels = await ResponseDeserializer.DeserializeNovelDTOsAsync(response);

            AvaiableNovels = novels;

            return response;
        }


        public async Task<HttpResponseMessage> UpdateNovelAsync()
        {
            var novelData = new
            {
                Id = CurrentNovel.Id,
                Name = CurrentNovel.Name,
                Description = CurrentNovel.Description,
                Abstract = CurrentNovel.Abstract
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
