using Common.Entity;
using Common.Interface;

namespace Domain.Model.Model.Author;

public class Author :BaseEntity<Guid>, IAggregate<Book.Book>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Book.Book> Books { get; set; }


    /// <summary>
    /// for ef core
    /// </summary>
    private Author()
    {
        
    }



}