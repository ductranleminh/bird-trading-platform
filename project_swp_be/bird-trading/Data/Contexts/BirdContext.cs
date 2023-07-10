using bird_trading.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace bird_trading.Data.Contexts
{
    public partial class BirdContext : DbContext
    {
        public BirdContext()
        {
        }

        public BirdContext(DbContextOptions<BirdContext> options)
            : base(options)
        {
        }
        public virtual DbSet<BankInfomation> BankInfomations { set; get; } = null!;
        public virtual DbSet<Category> Categories { set; get; } = null!;
        public virtual DbSet<Classify> Classifies { set; get; } = null!;
        public virtual DbSet<FeedBack> FeedBacks { set; get; } = null!;
        public virtual DbSet<Media> Medias { set; get; } = null!;
        public virtual DbSet<New> News { set; get; } = null!;
        public virtual DbSet<Pack> Packs { set; get; } = null!;
        public virtual DbSet<PackPrice> PackPrices { set; get; } = null!;
        public virtual DbSet<Post> Posts { set; get; } = null!;
        public virtual DbSet<PostTransaction> PostTransactions { set; get; } = null!;
        //public virtual DbSet<Reply> Replies { set; get; } = null!;
        public virtual DbSet<Role> Roles { set; get; } = null!;
        //public virtual DbSet<Topic> Topics { set; get; } = null!;
        public virtual DbSet<Transaction> Transactions { set; get; } = null!;
        public virtual DbSet<User> Users { set; get; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankInfomation>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.BankInfomations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankInfomations_Users");
            });

            modelBuilder.Entity<FeedBack>(entity =>
            {
                entity.HasOne(d => d.Post)
                    .WithMany(p => p.FeedBacks)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeedBacks_Posts");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FeedBacks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeedBacks_Users");
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Medias)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Medias_Posts");
            });

            modelBuilder.Entity<New>(entity =>
            {
                entity.HasOne(d => d.Classify)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.ClassifyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_News_Classifies");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_News_Users");
            });

            modelBuilder.Entity<PackPrice>(entity =>
            {
                entity.HasOne(d => d.Pack)
                    .WithMany(p => p.PackPrices)
                    .HasForeignKey(d => d.PackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PackPrices_Packs");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Posts_Categories");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Posts_Users");
            });

            modelBuilder.Entity<PostTransaction>(entity =>
            {
                entity.HasOne(d => d.Pack)
                    .WithMany(p => p.PostTransactions)
                    .HasForeignKey(d => d.PackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostTransactions_Packs");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostTransactions)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostTransactions_Posts");
            });

            // modelBuilder.Entity<Reply>(entity =>
            // {
            //     entity.HasOne(d => d.Topic)
            //         .WithMany(p => p.Replies)
            //         .HasForeignKey(d => d.TopicId)
            //         .OnDelete(DeleteBehavior.ClientSetNull)
            //         .HasConstraintName("FK_Replies_Topics");

            //     entity.HasOne(d => d.User)
            //         .WithMany(p => p.Replies)
            //         .HasForeignKey(d => d.UserId)
            //         .OnDelete(DeleteBehavior.ClientSetNull)
            //         .HasConstraintName("FK_Replies_Users");
            // });

            // modelBuilder.Entity<Topic>(entity =>
            // {
            //     entity.HasOne(d => d.User)
            //         .WithMany(p => p.Topics)
            //         .HasForeignKey(d => d.UserId)
            //         .OnDelete(DeleteBehavior.ClientSetNull)
            //         .HasConstraintName("FK_Topics_Users");
            // });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                //optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
            }
        }
    }
}

