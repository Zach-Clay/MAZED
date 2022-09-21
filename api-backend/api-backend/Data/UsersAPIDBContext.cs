using api_backend.Models;
using Microsoft.EntityFrameworkCore;
namespace api_backend.Data
{
    public class UsersAPIDBContext : DbContext
    {

        public UsersAPIDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<users> Users{ get; set; }
    }
}


 