using System.Net.Http.Headers;
using BlazorApp.model;
using BlazorApp.Model;

namespace BlazorApp.Components.Services{
    public interface IBibleApiService
    {
        Task<List<Book>?> GetBooksAsync();
        Task<List<Chapter>?> GetChaptersAsync(string book_id);
        Task<List<Verse>?> GetVersesAsync(string book_id, int chapter);
        Task<List<Verse>?> GetVersesByKeywordsAsync(string keywords);
        Task<List<Verse>?> GetVersesInBookByKeywordsAsync(string book_id, string keywords);
        Task<List<Collection>?> GetCollectionsAsync(string token);
        Task<List<Verse>?> GetVerseCollectionAsync(string token, string name);
        Task<bool> GetValidToken(string token);
        Task<string?> PostLogin(UserLogin user);
        Task<bool> PostRegister(UserRegister user);
        Task PostCollectionAsync(string token, string name);
        Task PostVerseToCollectionAsync(string token, string name, Verse verse);
        Task<bool> DeleteCollectionAsync(string token, string name);
    }
    public class BibleApiService: IBibleApiService{
        private readonly HttpClient _httpClient;
        private readonly TokenState tokenState;
        public BibleApiService(HttpClient httpClient, TokenState state)
        {
            _httpClient = httpClient;
            tokenState = state;
        }
        public async Task<List<Book>?> GetBooksAsync(){
            var request = new HttpRequestMessage(HttpMethod.Get, "api/bible");

            var response = await _httpClient.SendAsync(request);
            try{
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Book>>();  
                }
            }
            catch(Exception e){
                Console.WriteLine("Error: "+e.Message);
            }
            return null;
        }

        public async Task<List<Chapter>?> GetChaptersAsync(string book_id){
            var request = new HttpRequestMessage(HttpMethod.Get, "api/bible/"+book_id);

            var response = await _httpClient.SendAsync(request);
            try{
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Chapter>>();  
                }
            }
            catch(Exception e){
                Console.WriteLine("Error: "+e.Message);
            }
            return null;
        }

        public async Task<List<Verse>?> GetVersesAsync(string book_id, int chapter){
            string lan = tokenState.Languaje;
            var request = new HttpRequestMessage(HttpMethod.Get, "api/bible/"+lan+"/"+book_id+"/"+chapter);

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

        public async Task<List<Verse>?> GetVersesByKeywordsAsync(string keywords){
            var request = new HttpRequestMessage(HttpMethod.Get, "api/bible/kjv/keywords/"+keywords);
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

        public async Task<List<Verse>?> GetVersesInBookByKeywordsAsync(string book_id, string keywords){
            var request = new HttpRequestMessage(HttpMethod.Get, "api/bible/kjv/"+book_id+"/keywords/"+keywords);
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

        public async Task<bool> GetValidToken(string token){
            var request = new HttpRequestMessage(HttpMethod.Get, "api/login");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                return false;
            }
        }

        public async Task<string?> PostLogin(UserLogin user){
            try{
                var response = await _httpClient.PostAsJsonAsync("api/login", user);
                if(response.IsSuccessStatusCode){
                    var body = await response.Content.ReadFromJsonAsync<Token>();
                    if(body!=null){
                        return body.token;
                    }      
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        public async Task<bool> PostRegister(UserRegister user){
            try{
                var response = await _httpClient.PostAsJsonAsync("api/register", user);
                if(response.IsSuccessStatusCode){
                    return true;  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return false;
        }

        public async Task PostCollectionAsync(string token, string name){
            var request = new HttpRequestMessage(HttpMethod.Post, "api/collection/"+name);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Coleccion creada exitosamente");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
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

        public async Task<bool> DeleteCollectionAsync(string token, string name){
            var request = new HttpRequestMessage(HttpMethod.Delete, "api/collection/"+name);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Coleccion eliminada exitosamente");
                return true;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                return false;
            }
        }
        
    }

}