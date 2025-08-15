using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class AppContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }

    }
}