using Microsoft.EntityFrameworkCore;
using DatabaseRepository.Interfaces;
using DatabaseProvider.Models;
using Microsoft.Extensions.Logging;

namespace DatabaseRepository.Classes
{
    public class Repository<T>(LainLotContext context, ILogger<Repository<T>> logger) : IRepository<T> where T : class
    {
        private readonly ILogger<Repository<T>> _logger = logger;
        private readonly LainLotContext _context = context;

        public async Task Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            }

            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exc)
            {
                _logger.LogError($"Concurrency error occurred while creating: {exc.Message}");
                throw;
            }
            catch (DbUpdateException exc)
            {
                _logger.LogError($"Database error occurred while creating: {exc.Message}");
                throw;
            }
            catch (Exception exc)
            {
                _logger.LogError($"Create method. {exc.Message}");
                throw;
            }
        }

        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            }

            try
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exc)
            {
                _logger.LogError($"Concurrency error occurred while deleting: {exc.Message}");
                throw;
            }
            catch (DbUpdateException exc)
            {
                _logger.LogError($"Database error occurred while deleting: {exc.Message}");
                throw;
            }
            catch (Exception exc)
            {
                _logger.LogError($"Delete method. {exc.Message}");
                throw;
            }
        }


        public IQueryable<T> GetAll()
        {
            try
            {
                return _context.Set<T>().AsQueryable().AsNoTracking();
            }
            catch (Exception exc)
            {
                _logger.LogError($"GetAll method. {exc.Message}");
                throw;
            }
        }

        public async Task<T?> GetById(int id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception exc)
            {
                _logger.LogError($"GetById method. {exc.Message}");
                throw;
            }
        }

        public async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            }

            try
            {
                var entityType = typeof(T);
                var idProperty = entityType.GetProperty("Id") ?? throw new InvalidOperationException("Entity does not have an Id property.");
                var entityId = idProperty.GetValue(entity);

                var trackedEntity = _context.Set<T>().Local.FirstOrDefault(e => idProperty.GetValue(e).Equals(entityId));
                if (trackedEntity != null)
                {
                    _context.Entry(trackedEntity).State = EntityState.Detached;
                }

                _context.Entry(entity).State = EntityState.Modified;

                int changes = await _context.SaveChangesAsync();
                if (changes == 0)
                {
                    throw new InvalidOperationException("No records were updated.");
                }
            }
            catch (DbUpdateConcurrencyException exc)
            {
                _logger.LogError($"Concurrency error occurred while updating: {exc.Message}");
                throw;
            }
            catch (DbUpdateException exc)
            {
                _logger.LogError($"Database error occurred while updating: {exc.Message}");
                throw;
            }
            catch (Exception exc)
            {
                _logger.LogError($"Update method. {exc.Message}");
                throw;
            }
        }
    }
}