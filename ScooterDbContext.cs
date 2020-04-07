﻿using Beam_intern.Scooter.Data;
using Microsoft.EntityFrameworkCore;

public class ScooterDbContext : DbContext
{
    public ScooterDbContext(DbContextOptions<ScooterDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
    }
    
    public DbSet<ScooterDataModel> Scooters { get; set; }
}