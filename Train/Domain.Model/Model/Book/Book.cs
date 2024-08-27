﻿using Common.Entity;
using Common.Interface;
using Common.Validator;

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
        ObjectValidator.Instance
            .RuleFor(bookTitle)
            .NotNullOrEmpty(new BookTitleInvalidObjectException());
        
        BookTitle = bookTitle;
    }
    void SetPublishYear(int publishYear)
    {
        ObjectValidator.Instance
            .Must(publishYear, x => x == 0,
                new BookPublishYearException());
            
        PublishYear = publishYear;
    }

    void SetAuthorId(Guid authorId)
    {
        ObjectValidator.Instance
            .Must(authorId, x => x == Guid.Empty,
                new BookAuthorIdInvalidException());
        
        AuthorId=authorId;
    }
    #endregion

}