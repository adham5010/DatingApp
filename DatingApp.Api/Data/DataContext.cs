using DatingApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> Options) : base(Options){}

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
    }
}