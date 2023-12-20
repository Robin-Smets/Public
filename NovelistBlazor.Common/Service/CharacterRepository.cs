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
    public class CharacterRepository : RepositoryBase
    {


        public CharacterRepository(IHttpClientFactory httpClient, ResponseDeserializer responseDeserializer, EventMediator repositoryEventMediator, DataFactory dataFactory) : base(httpClient, responseDeserializer, repositoryEventMediator, dataFactory)
        {

        }

        public async Task<HttpResponseMessage> NewCharacterAsync(int novelId)
        {
            var character = DataFactory.CreateCharacterDTO();
            character.Name = "New Character";
            character.NovelId = novelId;
            await SaveCharacterAsync(character);
            return await LoadCharactersAsync();
        }

        public async Task<HttpResponseMessage> SaveCharacterAsync(CharacterDTO characterDTO)
        {
            var jsonObject = JsonConvert.SerializeObject(characterDTO);

            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            // Setze den Modus auf 'no-cors'
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7173/Character/")
            {
                Content = content,
                Method = HttpMethod.Post,
            };

            return await SendRequest(HttpClient, request);
        }

        public async Task<HttpResponseMessage> DeleteCharacterAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7173/Character/{id}")
            {
                Method = HttpMethod.Delete,
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await SendRequest(HttpClient, request);
        }

        public async Task<HttpResponseMessage> LoadCharactersAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7173/Character/")
            {
                Method = HttpMethod.Get,
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await SendRequest(HttpClient, request);

            return response;
        }


        public async Task<HttpResponseMessage> UpdateCharacterAsync(CharacterDTO character)
        {
            var characterData = new
            {
                Id = character.Id,
                Name = character.Name,
                Occupation = character.Occupation,
                RoleInStory = character.RoleInStory,
                PhysicalDescription = character.PhysicalDescription,
                PersonalityTraits = character.PersonalityTraits,
                Background = character.Background,
                GoalsAndMotivations = character.GoalsAndMotivations,
                CharacterArc = character.CharacterArc,
                NovelId = character.NovelId
            };

            var jsonNovelData = JsonConvert.SerializeObject(characterData);

            var httpContent = new StringContent(jsonNovelData, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7173/Character/{characterData.Id}"),
                Content = httpContent,
            };
            
            return await SendRequest(HttpClient, request);
        }
    }
}
