using Model;

namespace Core.Repositories;

public interface IDFCardsRepository {
    Task<bool> AddCardAsync(DFCard card);
    Task<DFCard?> GetCardAsync(int id);
    Task<List<DFCard>> GetCardsAsync();
    IEnumerable<DFCard> QueryCards(Func<DFCard, bool> predicate);
    Task<bool> RemoveCardAsync(int id);
    Task<bool> UpdateCardAsync(DFCard card);
}