using Common.Interface;
using Common.Validator;

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
        ObjectValidator.Instance
            .RuleFor(title)
            .NotNullOrEmpty(new BookTitleNullException())
            .Must(title,x=>x.Length is 2 or<2,
                new BookTitleLengthException());
    }

}