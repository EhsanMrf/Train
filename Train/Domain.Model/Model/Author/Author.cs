using Common.Entity;
using Common.Interface;
using Common.Validator;
using Domain.Model.Model.Book;

namespace Domain.Model.Model.Author;

public class Author :BaseEntity<Guid>, IAggregateRoot
{
    public string Name { get; set; }
    public IEnumerable<Book.Book> Books { get; set; }


    /// <summary>
    /// for ef core
    /// </summary>
    private Author()
    {
        
    }

    public void SetName(string name)
    {
        GuardAssessment(name);
        Name = name;
    }
    void GuardAssessment(string name)
    {
        ObjectValidator.Instance
            .RuleFor(name)
            .NotNullOrEmpty(new BookTitleNullException())
            .Must(name, x => x.Length is 2 or < 2,
                new BookTitleLengthException());
    }
}