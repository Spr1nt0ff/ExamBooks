using BookStoreDB;
using BookstoreDBLibrary;

namespace ExamBooks
{
    public class AdminPanel
    {
        private BookStoreDbContext _context;

        public AdminPanel(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Register(string login, string email, string password)
        {
            if (_context.Users.Any(u => u.Email == email))
            {
                Console.WriteLine("Error: this email is already registered.");
                return;
            }

            Users newUser = new Users
            {
                Login = login,
                Email = email,
                PasswordHash = password // Просто сохраняем пароль как есть
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            Console.WriteLine("Registration successful!");
        }

        public bool Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null || user.PasswordHash != password) // Сравниваем пароли напрямую
            {
                Console.WriteLine("Error: incorrect login or password.");
                return false;
            }

            Console.WriteLine($"Welcome, {user.Login}!");
            return true;
        }
    }
}
