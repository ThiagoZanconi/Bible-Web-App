namespace BlazorApp.Model
{
    public class Passage
    {
        public string book { get; set; } = string.Empty;
        public int chapter { get; set; }
        public int verse { get; set; }
        public string content { get; set; } = string.Empty;
        public bool paragraph_start { get; set; }
    }
    public class Reading
    {
        public string source { get; set; } = string.Empty;
        public string book { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string display { get; set; } = string.Empty;
        public string short_display { get; set; } = string.Empty;
        public List<Passage> passage { get; set; } = new List<Passage>();
    }

    public class Story
    {
        public string title { get; set; } = string.Empty;
        public string story { get; set; } = string.Empty;
    }
    public class CalendarDay
    {
        public int pascha_distance { get; set; }
        public int julian_day_number { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int weekday { get; set; }
        public int tone { get; set; }
        public List<string>? titles { get; set; } 
        public string summary_title { get; set; } = string.Empty;
        public int feast_level { get; set; }
        public string feast_level_description { get; set; } = string.Empty;
        public List<string>? feasts { get; set; }
        public int fast_level { get; set; }
        public string fast_level_desc { get; set; } = string.Empty;
        public int fast_exception { get; set; }
        public string fast_exception_desc { get; set; } = string.Empty;
        public List<string> saints { get; set; } = new();
        public List<string> service_notes { get; set; } = new();
        public List<int> abbreviated_reading_indices { get; set; } = new List<int>();
        public List<Reading> readings { get; set; } = new List<Reading>();
        public List<Story> stories { get; set; } = new List<Story>();
    }
}