using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TestController.sakila
{
    public partial class exogredientContext : DbContext
    {
        public exogredientContext()
        {
        }

        public exogredientContext(DbContextOptions<exogredientContext> options)
            : base(options)
        {
        }

        public virtual DbSet<IpAddress> IpAddress { get; set; }
        public virtual DbSet<SaveList> SaveList { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<Upload> Upload { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
 // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;user=root;database=exogredient;port=3306;password=poop1234", x => x.ServerVersion("8.0.18-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IpAddress>(entity =>
            {
                entity.HasKey(e => e.Ip)
                    .HasName("PRIMARY");

                entity.ToTable("ip_address");

                entity.Property(e => e.Ip)
                    .HasColumnName("ip")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LastRegFailTimestamp)
                    .HasColumnName("last_reg_fail_timestamp")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.RegistrationFailures)
                    .HasColumnName("registration_failures")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TimestampLocked)
                    .HasColumnName("timestamp_locked")
                    .HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<SaveList>(entity =>
            {
                entity.HasKey(e => new { e.Ingredient, e.Store, e.Username })
                    .HasName("PRIMARY");

                entity.ToTable("save_list");

                entity.HasIndex(e => e.Store)
                    .HasName("store_id_idx");

                entity.HasIndex(e => e.Username)
                    .HasName("username_idx");

                entity.Property(e => e.Ingredient)
                    .HasColumnName("ingredient")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Store)
                    .HasColumnName("store")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.SaveList)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("username");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("store");

                entity.HasIndex(e => e.Owner)
                    .HasName("owner_idx");

                entity.HasIndex(e => new { e.Latitude, e.Longitude })
                    .HasName("geocode")
                    .IsUnique();

                entity.Property(e => e.StoreId)
                    .HasColumnName("store_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("double(10,8)");

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasColumnType("double(11,8)");

                entity.Property(e => e.Owner)
                    .HasColumnName("owner")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PlaceId)
                    .IsRequired()
                    .HasColumnName("place_id")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StoreDescription)
                    .HasColumnName("store_description")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasColumnName("store_name")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.Store)
                    .HasForeignKey(d => d.Owner)
                    .HasConstraintName("owner");
            });

            modelBuilder.Entity<Upload>(entity =>
            {
                entity.ToTable("upload");

                entity.HasIndex(e => e.StoreId)
                    .HasName("store_id_idx");

                entity.HasIndex(e => new { e.Uploader, e.PostTimeDate })
                    .HasName("unique_post")
                    .IsUnique();

                entity.Property(e => e.UploadId)
                    .HasColumnName("upload_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Downvote)
                    .HasColumnName("downvote")
                    .HasColumnType("int(11)");

                entity.Property(e => e.InProgress).HasColumnName("in_progress");

                entity.Property(e => e.IngredientName)
                    .IsRequired()
                    .HasColumnName("ingredient_name")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasColumnName("photo")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PostTimeDate)
                    .HasColumnName("post_time_date")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("double(5,2)");

                entity.Property(e => e.Rating)
                    .IsRequired()
                    .HasColumnName("rating")
                    .HasColumnType("varchar(1)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StoreId)
                    .HasColumnName("store_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Uploader)
                    .IsRequired()
                    .HasColumnName("uploader")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Upvote)
                    .HasColumnName("upvote")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Upload)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("store_id");

                entity.HasOne(d => d.UploaderNavigation)
                    .WithMany(p => p.Upload)
                    .HasForeignKey(d => d.Uploader)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("uploader");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PRIMARY");

                entity.ToTable("user");

                entity.HasIndex(e => e.Email)
                    .HasName("email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.PhoneNumber)
                    .HasName("phone_number_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.EmailCode)
                    .IsRequired()
                    .HasColumnName("email_code")
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.EmailCodeFailures)
                    .HasColumnName("email_code_failures")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EmailCodeTimestamp)
                    .HasColumnName("email_code_timestamp")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.LastLoginFailTimestamp)
                    .HasColumnName("last_login_fail_timestamp")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.LoginFailures)
                    .HasColumnName("login_failures")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PhoneCodeFailures)
                    .HasColumnName("phone_code_failures")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("phone_number")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("salt")
                    .HasColumnType("varchar(8)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TempTimestamp)
                    .HasColumnName("temp_timestamp")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.UserType)
                    .IsRequired()
                    .HasColumnName("user_type")
                    .HasColumnType("varchar(11)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
