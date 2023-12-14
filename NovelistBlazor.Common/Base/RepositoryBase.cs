using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovelistBlazor.Common.Service;

namespace NovelistBlazor.Common.Base
{
    public abstract class RepositoryBase
    {
        public HttpClient HttpClient { get; set;}
        public ResponseDeserializer ResponseDeserializer { get; set;}
        public RepositoryEventMediator RepositoryEventMediator { get; set;} 

        public RepositoryBase(IHttpClientFactory httpClient, ResponseDeserializer responseDeserializer, RepositoryEventMediator repositoryEventMediator)
        {
            HttpClient = httpClient.CreateClient();
            ResponseDeserializer = responseDeserializer;
            RepositoryEventMediator = repositoryEventMediator;
        }

        protected async Task<HttpResponseMessage> SendRequest(HttpClient httpClient, HttpRequestMessage request)
        {
            HttpResponseMessage response;

            try
            {
                response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"HTTP Error: {e.Message}");
                response = new HttpResponseMessage();
            }
            catch (Exception e)
            {
                Console.WriteLine($"General Error: {e.Message}");
                response = new HttpResponseMessage();
            }

            return response;
        }
    }
}
