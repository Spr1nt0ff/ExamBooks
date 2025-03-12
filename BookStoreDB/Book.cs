namespace BookStoreDB
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }
        public int YearPublished { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int? PreviousBookId { get; set; }
        public Book? PreviousBook { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<BookPromotion> BookPromotions { get; set; }
    }
}
