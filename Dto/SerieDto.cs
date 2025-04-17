namespace gacha.Dto
{
    public class SerieDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Genre { get; set; }
        public string? ThumbUrl { get; set; }
        public int Id { get; internal set; }
    }

    public class SerieWithCardsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Genre { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ThumbUrl { get; set; } = default!;
        public List<CardDto> Cards { get; set; } = new();
    }

    public class CreateSerieDto
    {
        public string Name { get; set; } = default!;
        public string Genre { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ThumbUrl { get; set; } = default!;
    }
}