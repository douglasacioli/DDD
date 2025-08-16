using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MyContext _context;
        private DbSet<T> _dataset;

        public BaseRepository(MyContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(x => x.Id.Equals(id));
                if (result == null)
                {
                    return false;
                }
                _dataset.Remove(result);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting item", ex);
            }
        }

        public async Task<bool> ExistAsync(Guid id)
        {
           return await _dataset.AnyAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _dataset.ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Error retrieving all items", ex);
            }
            
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(x => x.Id.Equals(id));
                if (result == null)
                {
                    throw new Exception("Item not found");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving item", ex);
            }
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
               if(item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }
                item.CreatedAt = DateTime.UtcNow; 
                _dataset.Add(item);
                await _context.SaveChangesAsync(); 
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting item", ex);
            }
            return item;
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(x => x.Id.Equals(item.Id));
                if (result == null)
                {
                    throw new Exception("Item not found");
                }
                item.UpdatedAt = DateTime.UtcNow; 
                item.CreatedAt = DateTime.UtcNow; 
                _context.Entry(result).CurrentValues.SetValues(item);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating item", ex);
            }
            return item;
        }
    }
}