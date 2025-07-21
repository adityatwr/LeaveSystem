using LeaveApprovalSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeaveApprovalSystem.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LeaveDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(LeaveDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<IEnumerable<T>> GetItemsIdAsync(int id, string anotherTableName) => 
            await _dbSet
                .Include(anotherTableName)
                .Where(e => EF.Property<int>(e, "ApproverId") == id)
                .ToListAsync();
        public async Task AddAsync(T entity) { await _dbSet.AddAsync(entity); await _context.SaveChangesAsync(); }
        public void Update(T entity) { _dbSet.Update(entity); _context.SaveChanges(); }
        public void Remove(T entity) { _dbSet.Remove(entity); _context.SaveChanges(); }
    }
}
