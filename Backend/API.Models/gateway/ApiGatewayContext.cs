using System;
using API.AppConstants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Models.gateway
{
    public partial class ApiGatewayContext : DbContext
    {
        public ApiGatewayContext()
        {
        }

        public ApiGatewayContext(DbContextOptions<ApiGatewayContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Configuration> Configuration { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<Team> Team { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql(Constants.SQLConnection, x => x.ServerVersion("8.0.18-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Configuration>(entity =>
            {
                entity.HasKey(e => new { e.OpenTo, e.EndPoint })
                    .HasName("PRIMARY");

                entity.ToTable("configuration");

                entity.HasIndex(e => e.EndPoint)
                    .HasName("end_point_idx");

                entity.HasIndex(e => e.OpenTo)
                    .HasName("owner_idx");

                entity.Property(e => e.OpenTo)
                    .HasColumnName("open_to")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.EndPoint)
                    .HasColumnName("end_point")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Steps)
                    .IsRequired()
                    .HasColumnName("steps")
                    .HasColumnType("varchar(400)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.EndPointNavigation)
                    .WithMany(p => p.Configuration)
                    .HasPrincipalKey(p => p.Endpoint)
                    .HasForeignKey(d => d.EndPoint)
                    .HasConstraintName("end_point");

                entity.HasOne(d => d.OpenToNavigation)
                    .WithMany(p => p.Configuration)
                    .HasForeignKey(d => d.OpenTo)
                    .HasConstraintName("owned_by");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("service");

                entity.HasIndex(e => e.Endpoint)
                    .HasName("endpoint_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Owner)
                    .HasName("owner_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Dataformat)
                    .HasColumnName("dataformat")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Endpoint)
                    .IsRequired()
                    .HasColumnName("endpoint")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Input)
                    .HasColumnName("input")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Output)
                    .HasColumnName("output")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Owner)
                    .IsRequired()
                    .HasColumnName("owner")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.Service)
                    .HasForeignKey(d => d.Owner)
                    .HasConstraintName("owner");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.ClientId)
                    .HasName("PRIMARY");

                entity.ToTable("team");

                entity.HasIndex(e => e.CallbackUrl)
                    .HasName("callback_irl_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("username_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.WebsiteUrl)
                    .HasName("website_irl_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.CallbackUrl)
                    .IsRequired()
                    .HasColumnName("callback_url")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Digest)
                    .IsRequired()
                    .HasColumnName("digest")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Secret)
                    .IsRequired()
                    .HasColumnName("secret")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.WebsiteUrl)
                    .IsRequired()
                    .HasColumnName("website_url")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
