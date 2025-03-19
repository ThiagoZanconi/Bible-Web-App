namespace BlazorApp.Model{
    public class Book
    {
        public string id { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public int number { get; set; }
    }
    public class Chapter(string book_id, string book, int chapter)
    {
        public string book_id { get; set; } = book_id;
        public string book { get; set; } = book;
        public int chapter { get; set; } = chapter;
    }
    public class Verse(string book_id, string book, int chapter, int verse, string text, string translation_id)
    {
        public string book_id { get; set; } = book_id;
        public int chapter { get; set; } = chapter;
        public string book { get; set; } = book;
        public int verse { get; set; } = verse;
        public string text { get; set; } = text;
        public string translation_id { get; set; } = translation_id;
    }
}