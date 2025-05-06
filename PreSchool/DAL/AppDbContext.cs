using Microsoft.EntityFrameworkCore;
using PreSchool.Models;

namespace PreSchool.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
    
    public DbSet<Teacher> Teachers { get; set; }
}