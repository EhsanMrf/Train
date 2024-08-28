namespace Domain.Model.Model.Book;

public class BookBuilder
{
    private BookTitle BookTitle { get;  set; }
    private int PublishYear { get;  set; }
    private Guid AuthorId { get; set; }

    public BookBuilder WithBookTitle(BookTitle bookTitle)
    {
        this.BookTitle = bookTitle;
        return this;
    }

    public BookBuilder WithPublishYear(int publishYear)
    {
        this.PublishYear = publishYear;
        return this;
    }

    public BookBuilder WithAuthorId(Guid authorId)
    {
        this.AuthorId = authorId;
        return this;
    }

    public static BookBuilder Instance()
    {
        return new BookBuilder();
    }
    public Book Build()
    {
        return new Book(BookTitle.Title, PublishYear, AuthorId);
    }

}