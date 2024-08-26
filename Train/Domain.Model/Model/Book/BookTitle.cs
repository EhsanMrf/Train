using Common.Interface;

namespace Domain.Model.Model.Book;

public class BookTitle : IObjectValue
{
    public string Title { get; private set; }

    public static BookTitle CreateInstance(string title)=> new (title);

    /// <summary>
    /// for ef core 
    /// </summary>
    private BookTitle() { }

    private BookTitle(string title)
    {
        SetTitle(title);
    }

    void SetTitle(string title)
    {
        GuardAssessment(title);
        Title = title;
    }

    void GuardAssessment(string title)
    {
        if (title == null)
            throw new BookTitleNullException();

        if (title.Length is 2 or < 2)
            throw new BookTitleLengthException();
    }

}