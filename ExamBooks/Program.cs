using BookStoreDB;
using BookstoreDBLibrary;

try
{
    using (var db = new BookStoreDbContext())
    {
        Console.WriteLine("Attempting to connect...");

        InitializationDataBase();

        bool isLoggedIn = false;
        do
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            var action = Console.ReadLine();

            if (action == "1")
            {
                isLoggedIn = Login(db);
            }
            else if (action == "2")
            {
                Register(db);
                isLoggedIn = true;
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        } while (!isLoggedIn);

        ShowMenu(db);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

static void InitializationDataBase()
{
    try
    {
        Console.WriteLine("Attempting to connect to the database and initializing...");

        using (BookStoreDbContext db = new BookStoreDbContext())
        {
            Console.WriteLine("Connection to the database and initialization successful.");

            var author1 = new Author { FullName = "Author 1" };
            var author2 = new Author { FullName = "Author 2" };
            var author3 = new Author { FullName = "Author 3" };
            var author4 = new Author { FullName = "Author 4" };
            var author5 = new Author { FullName = "Author 5" };

            db.Authors.AddRange(author1, author2, author3, author4, author5);
            db.SaveChanges();

            // Books
            var book1 = new Book
            {
                Title = "Book 1",
                PageCount = 300,
                Genre = "Fiction",
                YearPublished = 2022,
                CostPrice = 10.99m,
                SalePrice = 15.99m
            };
            var book2 = new Book
            {
                Title = "Book 2",
                PageCount = 250,
                Genre = "Mystery",
                YearPublished = 2021,
                CostPrice = 12.99m,
                SalePrice = 17.99m
            };
            var book3 = new Book
            {
                Title = "Book 3",
                PageCount = 320,
                Genre = "Fantasy",
                YearPublished = 2020,
                CostPrice = 14.99m,
                SalePrice = 19.99m
            };
            var book4 = new Book
            {
                Title = "Book 4",
                PageCount = 180,
                Genre = "Non-fiction",
                YearPublished = 2023,
                CostPrice = 9.99m,
                SalePrice = 13.99m
            };
            var book5 = new Book
            {
                Title = "Book 5",
                PageCount = 400,
                Genre = "Science",
                YearPublished = 2021,
                CostPrice = 16.99m,
                SalePrice = 22.99m
            };

            db.Books.AddRange(book1, book2, book3, book4, book5);
            db.SaveChanges();

            // BookAuthors
            db.BookAuthors.AddRange(
                new BookAuthor { Author = author1, Book = book1 },
                new BookAuthor { Author = author2, Book = book2 },
                new BookAuthor { Author = author3, Book = book3 },
                new BookAuthor { Author = author4, Book = book4 },
                new BookAuthor { Author = author5, Book = book5 }
            );
            db.SaveChanges();

            // BookStocks
            db.BookStock.AddRange(
                new BookStock { Book = book1, Quantity = 100 },
                new BookStock { Book = book2, Quantity = 200 },
                new BookStock { Book = book3, Quantity = 150 },
                new BookStock { Book = book4, Quantity = 250 },
                new BookStock { Book = book5, Quantity = 300 }
            );
            db.SaveChanges();

            // Users
            var user1 = new Users { Login = "user1", Email = "user1@example.com", PasswordHash = "password1" };
            var user2 = new Users { Login = "user2", Email = "user2@example.com", PasswordHash = "password2" };
            var user3 = new Users { Login = "user3", Email = "user3@example.com", PasswordHash = "password3" };
            var user4 = new Users { Login = "user4", Email = "user4@example.com", PasswordHash = "password4" };
            var user5 = new Users { Login = "user5", Email = "user5@example.com", PasswordHash = "password5" };

            db.Users.AddRange(user1, user2, user3, user4, user5);
            db.SaveChanges();

            // ReservedBooks
            db.ReservedBooks.AddRange(
                new ReservedBook { Book = book1, User = user1, ReservedUntil = DateTime.Now.AddDays(7) },
                new ReservedBook { Book = book2, User = user2, ReservedUntil = DateTime.Now.AddDays(7) }
            );
            db.SaveChanges();

            // SalesReports
            db.SalesReports.AddRange(
                new SalesReport { Book = book1, QuantitySold = 50, TotalRevenue = 799.50m, ReportDate = DateTime.Now },
                new SalesReport { Book = book2, QuantitySold = 30, TotalRevenue = 539.70m, ReportDate = DateTime.Now }
            );
            db.SaveChanges();

            // BookPromotions
            db.BookPromotions.AddRange(
                new BookPromotion { Book = book1, DiscountPercentage = 10, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) },
                new BookPromotion { Book = book2, DiscountPercentage = 15, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) }
            );
            db.SaveChanges();
        }

        Thread.Sleep(1000);
        Console.Clear();
        Console.WriteLine("Database initialization completed!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database error: {ex.Message}\n{ex.InnerException?.Message}");
    }
}


bool Login(BookStoreDbContext db)
{
    string login = RequestInput("Enter username:");
    string password = RequestInput("Enter password:");

    var user = db.Users.FirstOrDefault(u => u.Login == login);
    if (user != null && user.PasswordHash == password)
    {
        Console.WriteLine("Login successful.");
        return true;
    }

    Console.WriteLine("Invalid username or password.");
    return false;
}

void Register(BookStoreDbContext db)
{
    string login = RequestInput("Enter username:");
    string password = RequestInput("Enter password:");
    string email = RequestInput("Enter email:");

    var newUser = new Users { Login = login, PasswordHash = password, Email = email };
    db.Users.Add(newUser);
    db.SaveChanges();

    Console.WriteLine("Registration successful.");
}

void ShowMenu(BookStoreDbContext db)
{
    bool isRunning = true;
    while (isRunning)
    {
        Console.WriteLine("1. Add a book");
        Console.WriteLine("2. Remove a book");
        Console.WriteLine("3. Edit book details");
        Console.WriteLine("4. Sell a book");
        Console.WriteLine("5. Write off a book");
        Console.WriteLine("6. Add a book to a sale");
        Console.WriteLine("7. Put a book on hold for a customer");
        Console.WriteLine("8. Search books");
        Console.WriteLine("9. View statistics");
        Console.WriteLine("0. Exit");

        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                AddBook(db);
                break;
            case "2":
                RemoveBook(db);
                break;
            case "3":
                EditBook(db);
                break;
            case "4":
                SellBook(db);
                break;
            case "5":
                WriteOffBook(db);
                break;
            case "6":
                PutBookOnSale(db);
                break;
            case "7":
                PutBookOnHold(db);
                break;
            case "8":
                SearchBooks(db);
                break;
            case "9":
                ShowStatistics(db);
                break;
            case "0":
                isRunning = false;
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
}

void AddBook(BookStoreDbContext db)
{
    string title = RequestInput("Enter book title:");
    string authorFullName = RequestInput("Enter author name:");
    string genre = RequestInput("Enter book genre:");
    int pageCount = int.Parse(RequestInput("Enter page count:"));
    int yearPublished = int.Parse(RequestInput("Enter year published:"));
    decimal costPrice = decimal.Parse(RequestInput("Enter cost price:"));
    decimal salePrice = decimal.Parse(RequestInput("Enter sale price:"));

    var author = db.Authors.FirstOrDefault(a => a.FullName == authorFullName);
    if (author == null)
    {
        author = new Author { FullName = authorFullName };
        db.Authors.Add(author);
    }

    var newBook = new Book
    {
        Title = title,
        Genre = genre,
        PageCount = pageCount,
        YearPublished = yearPublished,
        CostPrice = costPrice,
        SalePrice = salePrice,
        BookAuthors = new List<BookAuthor>
        {
            new BookAuthor { Author = author }
        }
    };
    db.Books.Add(newBook);
    db.SaveChanges();

    Console.WriteLine("Book added.");
}

void RemoveBook(BookStoreDbContext db)
{
    int bookId = int.Parse(RequestInput("Enter book ID to remove:"));
    var book = db.Books.Find(bookId);

    if (book != null)
    {
        db.Books.Remove(book);
        db.SaveChanges();
        Console.WriteLine("Book removed.");
    }
    else
    {
        Console.WriteLine("Book not found.");
    }
}

void EditBook(BookStoreDbContext db)
{
    int bookId = int.Parse(RequestInput("Enter book ID to edit:"));
    var book = db.Books.Find(bookId);

    if (book != null)
    {
        string newTitle = RequestInput("Enter new title (leave blank to keep unchanged):");
        if (!string.IsNullOrEmpty(newTitle)) book.Title = newTitle;

        string newAuthorName = RequestInput("Enter new author (leave blank to keep unchanged):");
        if (!string.IsNullOrEmpty(newAuthorName))
        {
            var author = db.Authors.FirstOrDefault(a => a.FullName == newAuthorName);
            if (author == null)
            {
                author = new Author { FullName = newAuthorName };
                db.Authors.Add(author);
            }
            book.BookAuthors.Clear();
            book.BookAuthors.Add(new BookAuthor { Author = author });
        }

        string newGenre = RequestInput("Enter new genre (leave blank to keep unchanged):");
        if (!string.IsNullOrEmpty(newGenre)) book.Genre = newGenre;

        db.SaveChanges();
        Console.WriteLine("Book updated.");
    }
    else
    {
        Console.WriteLine("Book not found.");
    }
}

void SellBook(BookStoreDbContext db)
{
    int bookId = int.Parse(RequestInput("Enter book ID to sell:"));
    var book = db.Books.Find(bookId);

    if (book != null)
    {
        var bookStock = db.BookStock.FirstOrDefault(b => b.BookId == book.Id);
        if (bookStock != null && bookStock.Quantity > 0)
        {
            bookStock.Quantity--;
            db.SaveChanges();
            Console.WriteLine("Book sold.");
        }
        else
        {
            Console.WriteLine("Book not available in stock.");
        }
    }
    else
    {
        Console.WriteLine("Book not found.");
    }
}

void WriteOffBook(BookStoreDbContext db)
{
    int bookId = int.Parse(RequestInput("Enter book ID to write off:"));
    var book = db.Books.Find(bookId);

    if (book != null)
    {
        var bookStock = db.BookStock.FirstOrDefault(b => b.BookId == book.Id);
        if (bookStock != null)
        {
            db.BookStock.Remove(bookStock);
            db.SaveChanges();
            Console.WriteLine("Book written off.");
        }
    }
    else
    {
        Console.WriteLine("Book not found.");
    }
}

void PutBookOnSale(BookStoreDbContext db)
{
    int bookId = int.Parse(RequestInput("Enter book ID to put on sale:"));
    var book = db.Books.Find(bookId);

    if (book != null)
    {
        string discount = RequestInput("Enter discount percentage:");
        var promotion = new BookPromotion
        {
            BookId = book.Id,
            DiscountPercentage = decimal.Parse(discount),
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(1)
        };
        db.BookPromotions.Add(promotion);
        db.SaveChanges();
        Console.WriteLine("Book added to sale.");
    }
    else
    {
        Console.WriteLine("Book not found.");
    }
}

void PutBookOnHold(BookStoreDbContext db)
{
    int bookId = int.Parse(RequestInput("Enter book ID to put on hold:"));
    int userId = int.Parse(RequestInput("Enter user ID:"));

    var book = db.Books.Find(bookId);
    var user = db.Users.Find(userId);
    if (book != null && user != null)
    {
        var reservedBook = new ReservedBook
        {
            BookId = bookId,
            UserId = userId,
            ReservedUntil = DateTime.Now.AddDays(7)
        };

        db.ReservedBooks.Add(reservedBook);
        db.SaveChanges();
        Console.WriteLine("Book put on hold for customer.");
    }
    else
    {
        if (book == null)
        {
            Console.WriteLine("Book not found.");
        }
        if (user == null)
        {
            Console.WriteLine("User not found.");
        }
    }
}


void SearchBooks(BookStoreDbContext db)
{
    string searchTerm = RequestInput("Enter book title, author, or genre:");
    var result = db.Books
                   .Where(b => b.Title.Contains(searchTerm) || b.BookAuthors.Any(a => a.Author.FullName.Contains(searchTerm)) || b.Genre.Contains(searchTerm))
                   .ToList();

    foreach (var book in result)
    {
        Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {string.Join(", ", book.BookAuthors.Select(a => a.Author.FullName))}");
    }
}

void ShowStatistics(BookStoreDbContext db)
{
    var totalBooks = db.Books.Count();
    var booksInStock = db.BookStock.Sum(b => b.Quantity);

    Console.WriteLine($"Total books: {totalBooks}");
    Console.WriteLine($"Books in stock: {booksInStock}");
}

string RequestInput(string prompt)
{
    Console.WriteLine(prompt);
    return Console.ReadLine();
}
