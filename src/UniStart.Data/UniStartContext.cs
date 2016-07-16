namespace UniStart.Data
{
    using System.Data.Entity;
    using Unistart.Models;

    public class UniStartContext : DbContext
    {
        public UniStartContext()
            : base("UniStartDatabase")
        {
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
