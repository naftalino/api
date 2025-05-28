using gacha.Dto;

namespace Gacha.Dtos;

public class CollectionDto
{
    public int CardId { get; set; }
    public int Quantity { get; set; }
    public CardDto Card { get; set; } = null!;
}

public class PaginatedCollectionDto
{
    public int TotalItems { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public List<CollectionDto> Items { get; set; } = new();
}

public class PaginatedResultDto<T>
{
    public List<T> Items { get; set; } = [];
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
}

public class CreateCollectionDto
{
    public long UserId { get; set; }
    public int CardId { get; set; }
    public int Quantity { get; set; } = 1;
}

