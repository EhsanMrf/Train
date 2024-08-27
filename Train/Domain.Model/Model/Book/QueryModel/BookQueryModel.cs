namespace Domain.Model.Model.Book.QueryModel;

public class BookQueryModel
{
    public string Title { get; set; }
    public int PublishYear { get; set; }
    public Guid AuthorId { get; set; }
}