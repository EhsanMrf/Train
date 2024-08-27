namespace Domain.Model.Model.Book.QueryModel;

public class BookQueryModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public int PublishYear { get; set; }
    public string AuthorName { get; set; } = null!;
}