using SID.Domain.Base;
using SID.Domain.Interfaces;
using SID.Infra.Configurations;

namespace SID.Infra.Repositories
{
    internal class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        private MySqlContext _context;

        public Repository(MySqlContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);

            _context.SaveChanges();

            return entity;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }
    }
}