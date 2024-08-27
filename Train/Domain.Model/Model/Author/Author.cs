using Common.Entity;
using Common.Interface;
using Common.Validator;

namespace Domain.Model.Model.Author;

public class Author :BaseEntity<Guid>, IAggregateRoot
{
    public string Name { get; set; }
    public IReadOnlyList<Book.Book> Books { get; set; }


    /// <summary>
    /// for ef core
    /// </summary>
    private Author()
    {
        
    }

    public Author(string name)
    {
        SetName(name);
        CreateDateTime = DateTime.Now;
    }

    public void Update(string name)
    {
        SetName(name);
        UpdateDateTime = DateTime.Now;
    }

    public void Delete()
    {
        IsDeleted = true;
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
            .NotNullOrEmpty(new AuthorNameNullException())
            .Must(name, x => x.Length is 2 or < 2,
                new AuthorTitleLengthException());
    }
}