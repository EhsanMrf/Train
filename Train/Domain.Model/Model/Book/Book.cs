using Common.Entity;
using Common.Interface;

namespace Domain.Model.Model.Book;

public class Book : BaseEntity<Guid>, IAggregateRoot
{

    #region Peroperty
    public BookTitle BookTitle { get;private set; }
    public int PublishYear { get; private set; }
    public Guid AuthorId { get; set; }

    #endregion


    /// <summary>
    /// for ef core
    /// </summary>
    private Book()
    {
        
    }

    public Book(BookTitle bookTitle,int publishYear,Guid authorId)
    {
        SetData(bookTitle, publishYear, authorId);
        CreateDateTime = DateTime.Now;
    }

    public void Update(BookTitle bookTitle, int publishYear, Guid authorId)
    {
        SetData(bookTitle, publishYear, authorId);
        UpdateDateTime= DateTime.Now;
    }

    public void Delete()
    {
        IsDeleted=true;
    }






    #region Behavior

    void SetData(BookTitle bookTitle, int publishYear, Guid authorId)
    {
        SetBookTitle(bookTitle);
        SetPublishYear(publishYear);
        SetAuthorId(authorId);
    }

    //invariant
    void SetBookTitle(BookTitle bookTitle)
    {
        if (bookTitle==null)
            throw new BookTitleInvalidObjectException();
        
        BookTitle = bookTitle;
    }
    void SetPublishYear(int publishYear)
    {
        if (publishYear == 0)
            throw new BookPublishYearException();
        PublishYear = publishYear;
    }

    void SetAuthorId(Guid authorId)
    {
        if (authorId==Guid.Empty)
            throw new BookAuthorIdInvalidException();
        
        AuthorId=authorId;
    }
    #endregion

}