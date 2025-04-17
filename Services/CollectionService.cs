using gacha.Database;
using gacha.Dto;
using gacha.Models;
using Gacha.Dtos;
using Microsoft.EntityFrameworkCore;

namespace gacha.Services;

public class CollectionService
{
    private readonly AppDbContext _context;

    public CollectionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedCollectionDto> GetByUserIdAsync(int userId, int page = 1, int pageSize = 10)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
        if (!userExists)
            throw new ArgumentException("Usuário não encontrado.");

        var query = _context.Collections
            .Where(c => c.UserId == userId)
            .Include(c => c.Card)
                .ThenInclude(c => c.Serie)
            .AsQueryable();

        var totalItems = await query.CountAsync();
        if (totalItems == 0)
            throw new InvalidOperationException("Não há nada na coleção.");

        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var items = await query
            .OrderBy(c => c.CardId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var result = items.Select(collection => new CollectionDto
        {
            CardId = collection.CardId,
            Quantity = collection.Quantity,
            Card = new CardDto
            {
                Id = collection.Card.Id,
                Name = collection.Card.Name,
                Rarity = collection.Card.Rarity,
                ThumbUrl = collection.Card.ThumbUrl,
                Value = collection.Card.Value,
                Serie = new SerieBasicDto
                {
                    Id = collection.Card.Serie.Id,
                    Name = collection.Card.Serie.Name,
                    Genre = collection.Card.Serie.Genre,
                    ThumbUrl = collection.Card.Serie.ThumbUrl
                }
            }
        }).ToList();

        return new PaginatedCollectionDto
        {
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize,
            TotalPages = totalPages,
            Items = result
        };
    }

    public async Task<CollectionDto> CreateOrUpdateAsync(CreateCollectionDto dto)
    {
        var user = await _context.Users.FindAsync(dto.UserId);
        if (user == null)
            throw new ArgumentException("Usuário não encontrado.");

        var card = await _context.Cards
            .Include(c => c.Serie)
            .FirstOrDefaultAsync(c => c.Id == dto.CardId);

        if (card == null)
            throw new ArgumentException("Carta não encontrada.");

        var collection = await _context.Collections
            .FirstOrDefaultAsync(c => c.UserId == dto.UserId && c.CardId == dto.CardId);

        if (collection != null)
        {
            collection.Quantity += dto.Quantity;

            if (collection.Quantity < 1)
            {
                _context.Collections.Remove(collection);
                await _context.SaveChangesAsync();
                return null!;
            }

            _context.Collections.Update(collection);
        }
        else
        {
            if (dto.Quantity < 1)
                throw new ArgumentException("Quantidade inválida. Não é possível adicionar quantidade menor que 1 para novo item.");

            collection = new Collection
            {
                UserId = dto.UserId,
                CardId = dto.CardId,
                Quantity = dto.Quantity
            };

            _context.Collections.Add(collection);
        }

        await _context.SaveChangesAsync();

        return new CollectionDto
        {
            CardId = collection.CardId,
            Quantity = collection.Quantity,
            Card = new CardDto
            {
                Id = card.Id,
                Name = card.Name,
                Rarity = card.Rarity,
                ThumbUrl = card.ThumbUrl,
                Value = card.Value,
                Serie = new SerieBasicDto
                {
                    Id = card.Serie.Id,
                    Name = card.Serie.Name,
                    Genre = card.Serie.Genre,
                    ThumbUrl = card.Serie.ThumbUrl
                }
            }
        };
    }

}
