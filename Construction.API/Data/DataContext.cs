﻿//this class contains the dbsets for the entitys to intercat whit the database
using Construction.Shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Construction.API.Data
{
    //inheritance whit the identityDbcontext whit User
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<ProjectConstruction> ProjectConstructions { get; set; }
        public DbSet<ConstructionTeam> ConstructionTeams { get; set; }
        public DbSet<Dutie> Duties { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        public DbSet<MaterialAssignment> MaterialAssignments { get; set; }

        public DbSet<EquipmentAssignment> EquipmentAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

    }
}
