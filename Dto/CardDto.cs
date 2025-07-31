namespace gacha.Dto
{
    public class SerieBasicDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Genre { get; set; } = "";
        public string ThumbUrl { get; set; } = "";
    }

    public class CreateCardDto
    {
        public string Name { get; set; }
        public string ThumbUrl { get; set; } = "https://placehold.co/450x700/png";
        public int Value { get; set; } = 10;
        public int SerieId { get; set; }
        public string Credits { get; set; } = "t.me/padocard";
    }

    public class UpdateCardDto
    {
        public string? Name { get; set; }
        public string? ThumbUrl { get; set; }
        public int? Value { get; set; }
        public int? SerieId { get; set; }
        public string? Credits { get; set; }
    }

    public class CardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ThumbUrl { get; set; } = "https://placehold.co/450x700/png";
        public string? Credits { get; set; }
        public int? Value { get; set; }
        public SerieBasicDto Serie { get; set; } = new();
    }

    public class ReturnAllCardsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ThumbUrl { get; set; } = "https://placehold.co/450x700/png";
        public string? Credits { get; set; }
        public int? TimesPulled { get; set; }
        public int? PeopleOwned { get; set; }
        public int? Value { get; set; }
        public string? SerieName { get; set; }
    }

    public record CardsDto(int Id, string Name, SerieInfoDto Serie);
    public record SerieInfoDto(string Name);
}
