using URLShortenerAPI.Context;
using URLShortenerAPI.Data.Entities;
using URLShortenerAPI.Tools.IoC;

namespace URLShortenerAPI.Data.UnitOfWork
{
    public class URLShortenerUnitOfWork : IURLShortenerUnitOfWork
    {
        private readonly ApiDbContext _dbContext;
        public string ContextId { get; private set; }

        public URLShortenerUnitOfWork(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(bool isTransactionalData)
        {
            var result = 0;

            if (!isTransactionalData)
            {
                result = await _dbContext.SaveChangesAsync();

                _dbContext.ChangeTracker.Clear();

                return result;
            }

            await _dbContext.Database.BeginTransactionAsync();

            try
            {
                result = await _dbContext.SaveChangesAsync();

                await _dbContext.Database.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _dbContext.Database.RollbackTransactionAsync();
            }

            _dbContext.ChangeTracker.Clear();

            return result;
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
