using System.Net.Http.Headers;
using BlazorApp.Model;

namespace BlazorApp.Components.Services{
    public interface IBibleApiService
    {
        Task<List<Collection>?> GetCollectionsAsync(string token);
        Task<List<Verse>?> GetVerseCollectionAsync(string token, string name);
        Task PostVerseToCollectionAsync(string token, string name, Verse verse);
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

        public async Task<List<Verse>?> GetVerseCollectionAsync(string token, string name){
            var request = new HttpRequestMessage(HttpMethod.Get, "api/collection/"+name);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            try{
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Verse>>();  
                }
            }
            catch(Exception e){
                Console.WriteLine("Error: "+e.Message);
            }
            return null;
        }

        public async Task PostVerseToCollectionAsync(string token, string name, Verse verse){
            var request = new HttpRequestMessage(HttpMethod.Post, "api/collection/"+name+"/"+verse.book_id+"/"+verse.chapter+":"+verse.verse);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            try{
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Verso agregado a la coleccion exitosamente");
                }
            }
            catch(Exception e){
                Console.WriteLine("Error: "+e.Message);
            }
        }
        
    }

}