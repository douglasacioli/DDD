using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>  
    {
        public MyContext CreateDbContext(string[] args)
        {
            var connectionString = "Data Source=DESKTOP-UMTKU90\\SQLEXPRESS;Initial Catalog=DougDb;Integrated Security=True;TrustServerCertificate=True";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new MyContext(optionsBuilder.Options);
        }
    }
}