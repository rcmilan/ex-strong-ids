using SID.Domain.Base;

namespace SID.Domain.Interfaces
{
    public interface IRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        IEnumerable<TEntity> GetAll();
    }
}
