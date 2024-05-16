using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class TaskDBContext : DbContext
    {
        public virtual DbSet<Request> Request { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-SGV5D71;Initial Catalog=Task 5.0;Integrated Security=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
