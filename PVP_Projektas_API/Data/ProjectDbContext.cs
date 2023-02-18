using Microsoft.EntityFrameworkCore;
using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Data;

public class ProjectDbContext: DbContext
{
    public ProjectDbContext()
    {

    }
    public DbSet<User> DbUsers { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PVP_Projektas_API2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }
}
