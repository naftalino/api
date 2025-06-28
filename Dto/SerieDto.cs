namespace gacha.Dto
{
    public class SerieDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ThumbUrl { get; set; } = string.Empty;
        public string Type { get; set; } = "genre"; // "genre" ou "subgenre"
        public int? GenreId { get; set; }
        public int? SubgenreId { get; set; }
    }

}