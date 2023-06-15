namespace URLShortenerAPI.Data.Entities
{
    public class URLShortener
    {
        public int Id { get; set; }
        public string Url { get; set; } = "";
        public string ShortUrl { get; set; } = "";
    }
}
