using System.Net.Http.Headers;
using BlazorApp.Model;

namespace BlazorApp.Components.Services{
    public interface IBibleApiService
    {
        Task<List<Collection>?> GetCollectionsAsync(string token);
    }
    public class BibleApiService: IBibleApiService{
        private readonly HttpClient _httpClient;
        public BibleApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Collection>?> GetCollectionsAsync(string token){
            var request = new HttpRequestMessage(HttpMethod.Get, "api/collection");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            try{
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Collection>>();  
                }
            }
            catch(Exception e){
                Console.WriteLine("Error: "+e.Message);
            }
            return null;
        }
        
    }

}