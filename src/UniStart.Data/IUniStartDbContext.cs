namespace UniStart.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Unistart.Models;

    public interface IUniStartDbContext
    {
        IDbSet<Topic> Topics { get; set; }

        IDbSet<Lecture> Lectures { get; set; }

        void SaveChanges();

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
