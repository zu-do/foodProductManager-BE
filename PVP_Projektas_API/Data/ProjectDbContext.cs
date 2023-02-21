﻿using Microsoft.EntityFrameworkCore;
using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Data;

public class ProjectDbContext: DbContext
{
    public ProjectDbContext()
    {

    }
    public DbSet<User> DbUsers { get; set; } = null!;
    public DbSet<Product> DbProducts { get; set; } = null!;
    public DbSet<Category> DbCategories { get; set; } = null!;
    public DbSet<GiveawaySpot> DbGiveawaySpots { get; set; }
    public DbSet<Shelf> DbShelves { get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PVP_Projektas_API;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }
}
