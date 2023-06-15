using URLShortenerAPI.Data.Entities;

namespace URLShortenerAPI.Data.UnitOfWork
{
    public interface IURLShortenerUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(bool isTransactionalData);

    }
}
