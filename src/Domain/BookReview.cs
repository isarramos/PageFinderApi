namespace Domain
{
    public class BookReview
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Score { get; set; }
        public string Text { get; set; }
    }
}
