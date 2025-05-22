
using ExamenAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenAWS.Data
{
    public class ComicsContext : DbContext
    {
        public ComicsContext(DbContextOptions<ComicsContext> options) : base(options)
        {
        }
        public DbSet<Comic> Comics { get; set; }
    }
}
