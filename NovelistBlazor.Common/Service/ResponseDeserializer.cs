using Newtonsoft.Json;
using NovelistBlazor.Common.DTO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NovelistBlazor.Common.Service
{
    public class ResponseDeserializer
    {
        public async Task<List<NovelDTO>> DeserializeNovelDTOsAsync(HttpResponseMessage response)
        {
            if (response == null)
            {
                // Handle null response as needed
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    // Deserialisiere den JSON-Inhalt in eine Liste von NovelDTOs
                    var novelDTOs = JsonConvert.DeserializeObject<List<NovelDTO>>(content);
                    return novelDTOs;
                }
                catch (JsonException ex)
                {
                    // Handle JSON deserialization error
                    Console.WriteLine($"JSON Deserialization Error: {ex.Message}");
                }
            }
            else
            {
                // Handle unsuccessful response (non-2xx status codes)
                Console.WriteLine($"Unsuccessful Response: {response.StatusCode}, {content}");
            }

            // Rückgabe einer leeren Liste im Fehlerfall
            return new List<NovelDTO>();
        }

        public async Task<List<CharacterDTO>> DeserializeCharacterDTOsAsync(HttpResponseMessage response)
        {
            if (response == null)
            {
                // Handle null response as needed
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    // Deserialisiere den JSON-Inhalt in eine Liste von NovelDTOs
                    var characterDTOs = JsonConvert.DeserializeObject<List<CharacterDTO>>(content);
                    return characterDTOs;
                }
                catch (JsonException ex)
                {
                    // Handle JSON deserialization error
                    Console.WriteLine($"JSON Deserialization Error: {ex.Message}");
                }
            }
            else
            {
                // Handle unsuccessful response (non-2xx status codes)
                Console.WriteLine($"Unsuccessful Response: {response.StatusCode}, {content}");
            }

            // Rückgabe einer leeren Liste im Fehlerfall
            return new List<CharacterDTO>();
        }

        public async Task<List<PlotUnitDTO>> DeserializePlotUnitDTOsAsync(HttpResponseMessage response)
        {
            if (response == null)
            {
                // Handle null response as needed
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    // Deserialisiere den JSON-Inhalt in eine Liste von PlotUnitDTOs
                    var plotUnitDTOs = JsonConvert.DeserializeObject<List<PlotUnitDTO>>(content);
                    return plotUnitDTOs;
                }
                catch (JsonException ex)
                {
                    // Handle JSON deserialization error
                    Console.WriteLine($"JSON Deserialization Error: {ex.Message}");
                }
            }
            else
            {
                // Handle unsuccessful response (non-2xx status codes)
                Console.WriteLine($"Unsuccessful Response: {response.StatusCode}, {content}");
            }

            // Rückgabe einer leeren Liste im Fehlerfall
            return new List<PlotUnitDTO>();
        }
    }
}

