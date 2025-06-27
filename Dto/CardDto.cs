using gacha.Models;
using pd.Models;

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
        public string Rarity { get; set; } = "common";
        public int Value { get; set; } = 10;
        public int SerieId { get; set; }
    }

    public class UpdateCardDto
    {
        public string? Name { get; set; }
        public string? ThumbUrl { get; set; }
        public string? Rarity { get; set; }
        public int? Value { get; set; }
        public int? SerieId { get; set; }
    }


    public class CardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ThumbUrl { get; set; }
        public string? Rarity { get; set; }
        public int? Value { get; set; }
        public SerieBasicDto Serie { get; set; } = new();
    }

    public class CardDtoPaged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ThumbUrl { get; set; }
        public string? Rarity { get; set; }
    }

    public class GetAllDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string ThumbUrl { get; set; } = "";
        public string Rarity { get; set; } = "";
        public int Value { get; set; }
        public int SerieId { get; set; }
        public string SerieName { get; set; } = "";
    }

    public class SerieWithPagedCardsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Genre { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ThumbUrl { get; set; } = default!;
        public List<CardDtoPaged> Cards { get; set; } = new();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCards { get; set; }
    }

    public record CardsDto(int Id, string Name, SerieInfoDto Serie);
    public record SerieInfoDto(string Name);
}
