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
        private CharacterDTO _currentCharacter;
        public CharacterDTO CurrentCharacter 
        { 
            get => _currentCharacter; 
            set
            {
                if (_currentCharacter != value)
                {
                    _currentCharacter = value;
                    RepositoryEventMediator.OnCurrentCharacterChanged(_currentCharacter);
                }
            }
        }
        
        private List<CharacterDTO> _allCurrentCharacters;
        public List<CharacterDTO> AllCurrentCharacters 
        { 
            get => _allCurrentCharacters; 
            set
            {
                if (_allCurrentCharacters != value)
                {
                    _allCurrentCharacters = value;
                    RepositoryEventMediator.OnAllCurrentCharactersChanged(_allCurrentCharacters);
                }
            }
        }

        public CharacterRepository(IHttpClientFactory httpClient, ResponseDeserializer responseDeserializer, RepositoryEventMediator repositoryEventMediator) : base(httpClient, responseDeserializer, repositoryEventMediator)
        {
            _currentCharacter = new CharacterDTO();
            _allCurrentCharacters = new List<CharacterDTO>(); 
        }

        public async Task NewCharacterAsync()
        {
                CurrentCharacter = new CharacterDTO();
                await SaveCharacterAsync(CurrentCharacter);
                await LoadCharactersAsync();
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

        public async Task<HttpResponseMessage> DeleteCharacterAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7173/Character/{CurrentCharacter.Id}")
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

            var characters = await ResponseDeserializer.DeserializeCharacterDTOsAsync(response);

            AllCurrentCharacters = characters;

            return response;
        }


        public async Task<HttpResponseMessage> UpdateCharacterAsync()
        {
            var characterData = new
            {
                Id = CurrentCharacter.Id,
                Name = CurrentCharacter.Name,
                Occupation = CurrentCharacter.Occupation,
                RoleInStory = CurrentCharacter.RoleInStory,
                PhysicalDescription = CurrentCharacter.PhysicalDescription,
                PersonalityTraits = CurrentCharacter.PersonalityTraits,
                Background = CurrentCharacter.Background,
                GoalsAndMotivations = CurrentCharacter.GoalsAndMotivations,
                CharacterArc = CurrentCharacter.CharacterArc,
                NovelId = CurrentCharacter.NovelId
            };

            var jsonNovelData = JsonConvert.SerializeObject(characterData);

            var httpContent = new StringContent(jsonNovelData, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7173/Novel/{characterData.Id}"),
                Content = httpContent,
            };
            
            return await SendRequest(HttpClient, request);
        }
    }
}
