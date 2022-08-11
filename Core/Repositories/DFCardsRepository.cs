using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Core.Database;
using Model;

using static Microsoft.EntityFrameworkCore.EntityState;
using Core.Exceptions;

namespace Core.Repositories;

public class DFCardsRepository: IDFCardsRepository {
    private readonly DiceForgerDbContext _dbContext;

    public DFCardsRepository() {
        _dbContext = DiceForgerDbContextFactory.CreateDbContext();
    }

    public async Task<bool> AddCardAsync(DFCard card) {
        EntityEntry<DFCard> entry = await _dbContext.DFCards.AddAsync(card);

        await _dbContext.SaveChangesAsync();

        return entry.State == Added;
    }

    public async Task<DFCard?> GetCardAsync(int id) {
        DFCard? card = await _dbContext.DFCards.Include(c => c.Costs).Include(c => c.Effects).FirstOrDefaultAsync(c => c.Id == id);
        return card;
    }

    public async Task<List<DFCard>> GetCardsAsync() {
        List<DFCard> cards = await _dbContext.DFCards.Include(c => c.Costs).Include(c => c.Effects).ToListAsync();
        return cards;
    }

    public IEnumerable<DFCard> QueryCards(Func<DFCard, bool> predicate) {
        IEnumerable<DFCard> cards = _dbContext.DFCards.Where(predicate);
        return cards;
    }

    public async Task<bool> RemoveCardAsync(int id) {
        DFCard card = await _dbContext.DFCards.FindAsync(id) ?? throw new DbEntryNotFoundException($"Cannot find card with id {id}");

        EntityEntry<DFCard> tracking = _dbContext.DFCards.Remove(card);

        await _dbContext.SaveChangesAsync();

        return tracking.State == Deleted;
    }

    public async Task<bool> UpdateCardAsync(DFCard card) {
        EntityEntry<DFCard> tracking = _dbContext.DFCards.Update(card);

        await _dbContext.SaveChangesAsync();

        return tracking.State == Modified;
    }
}
