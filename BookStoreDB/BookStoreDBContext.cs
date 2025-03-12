using BookStoreDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreDBLibrary
{
    public class BookStoreDbContext : DbContext
    {
        private static DbContextOptions<BookStoreDbContext> _options;

        static BookStoreDbContext()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<BookStoreDbContext>();
            _options = optionsBuilder.UseSqlServer(connectionString).Options;
        }


        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookPromotion> BookPromotions { get; set; }
        public DbSet<BookStock> BookStock { get; set; }
        public DbSet<ReservedBook> ReservedBooks { get; set; }
        public DbSet<SalesReport> SalesReports { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.PreviousBook)
                .WithMany()
                .HasForeignKey(b => b.PreviousBookId);

            modelBuilder.Entity<BookStock>()
                .HasOne(bs => bs.Book)
                .WithOne()
                .HasForeignKey<BookStock>(bs => bs.BookId);

            modelBuilder.Entity<ReservedBook>()
                .HasOne(rb => rb.Book)
                .WithMany()
                .HasForeignKey(rb => rb.BookId);

            modelBuilder.Entity<SalesReport>()
                .HasOne(sr => sr.Book)
                .WithMany()
                .HasForeignKey(sr => sr.BookId);

            modelBuilder.Entity<BookPromotion>()
                .HasOne(bp => bp.Book)
                .WithMany(b => b.BookPromotions)
                .HasForeignKey(bp => bp.BookId);
            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<ReservedBook>()
                .HasOne(rb => rb.User)
                .WithMany(u => u.ReservedBooks)
                .HasForeignKey(rb => rb.UserId);
        }


        public BookStoreDbContext() : base(_options)
        {
            Database.EnsureCreated();
        }
    }
}
