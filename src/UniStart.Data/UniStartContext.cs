namespace UniStart.Data
{
    using System.Data.Entity;
    using Unistart.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class UniStartContext : IdentityDbContext<User>, IUniStartDbContext
    {
        public UniStartContext()
            : base("UniStartDatabase", throwIfV1Schema:false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
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
