using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Model
{

        public partial class JobHunterContext : DbContext
        {
            public JobHunterContext()
            {
            }

            public JobHunterContext(DbContextOptions<JobHunterContext> options)
                : base(options)
            {
            }
            public virtual DbSet<Users> Users { get; set; }
            public virtual DbSet<JobOffer> JobOffer { get; set; }
            public virtual DbSet<BidOffer> BidOffer { get; set; }
            public virtual DbSet<Recomendation> Recomendation { get; set; }
            public virtual DbSet<TakenOffer> TakenOffer { get; set; }
            public virtual DbSet<Category> Category { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=jobhunter;Integrated Security=true;");
                }
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1,Description="IT"},
                new Category { Id=2,Description="Budownictwo"},
                new Category { Id=3,Description="Gastronomia"},
                new Category { Id=4,Description="Ogrodnictwo"},
                new Category { Id=5,Description= "Rolnictwo" }
                
                );
                OnModelCreatingPartial(modelBuilder);
       
        }

            partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        }

    }

