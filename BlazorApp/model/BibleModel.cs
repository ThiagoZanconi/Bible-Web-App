namespace BlazorApp.Model{
    public class Book
    {
        public string id { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string translationId { get; set; } = string.Empty;
    }
    public class Chapter(string bookId, int chp, string translationId)
    {
        public string bookId { get; set; } = bookId;
        public int chp { get; set; } = chp;
        public string translationId { get; set; } = translationId;
    }
    public class Verse(string bookId, int chapter, int vrs, string text, string translationId)
    {
        public string bookId { get; set; } = bookId;
        public int chapter { get; set; } = chapter;
        public int vrs { get; set; } = vrs;
        public string text { get; set; } = text;
        public string translationId { get; set; } = translationId;
    }
}