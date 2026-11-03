namespace PAP.Models
{
    /// <summary>
    /// Representa una relación entre un libro y un autor.
    /// BookAuthors no se genera como entidad EF porque el modelo la mapea como muchos-a-muchos.
    /// </summary>
    public class BookAuthor
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public string BookTitle { get; set; }
        public string AuthorName { get; set; }
    }
}
