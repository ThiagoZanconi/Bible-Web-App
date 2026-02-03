using BlazorApp.Model;

namespace BlazorApp.Components.Services{
    public interface ICalendarApiService
    {
        Task<CalendarDay?> GetToday(string cal);
        Task<CalendarDay?> GetCalendarDay(string cal, int year, int month, int day);
    
    }
    public class CalendarApiService: ICalendarApiService
    {
        private readonly HttpClient _httpClient;
        public CalendarApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<CalendarDay?> GetToday(string cal)
        {
            var response = await _httpClient.GetAsync($"{cal}");

            try{
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CalendarDay>();  
                }
            }
            catch(Exception e){
                Console.WriteLine("Error: "+e.Message);
            }
            return null;
        }

        public async Task<CalendarDay?> GetCalendarDay(string cal, int year, int month, int day)
        {
            var response = await _httpClient.GetAsync($"{cal}/{year}/{month}/{day}");

            try{
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CalendarDay>();  
                }
            }
            catch(Exception e){
                Console.WriteLine("Error: "+e.Message);
            }
            return null;
        }


    }

}