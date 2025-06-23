using gacha.Database;
using gacha.Models;
using Microsoft.EntityFrameworkCore;

namespace pd.Services
{
    public class GachaService
    {
        private readonly AppDbContext _context;

        public GachaService(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        // service para randomizar os itens para o usuário escolher.
        public Card DrawCard(int serieId)
        {
            var serieCards = _context.Set<Card>()
                .Where(c => c.SerieId == serieId)
                .ToList();
            if (!serieCards.Any())
            {
                throw new InvalidOperationException("Nenhuma carta encontrad para a série especificada.");
            }
            var randomizedCard = serieCards.OrderBy(c => Guid.NewGuid()).FirstOrDefault();
            return randomizedCard;
        }

        public Serie DrawSerie(string genre)
        {
            var series = _context.Set<Serie>().Where(s => s.Genre == genre).ToList();
            if (!series.Any())
            {
                throw new InvalidOperationException("Nenhuma série encontrada para o gênero especificado.");
            }
            var randomizedSerie = series.OrderBy(s => Guid.NewGuid()).FirstOrDefault();
            return randomizedSerie;
        }
    }
}
