namespace UniStart.Data
{
    using System.Data.Entity;
    using Unistart.Models;

    public class UniStartContext : DbContext, IUniStartDbContext
    {
        public UniStartContext()
            : base("UniStartDatabase")
        {
        }

        public virtual IDbSet<Topic> Topics { get; set; }
        public virtual IDbSet<Lecture> Lectures { get; set; }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
