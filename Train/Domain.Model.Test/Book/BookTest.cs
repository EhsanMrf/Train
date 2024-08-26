using Domain.Model.Model.Book;
using FluentAssertions;

namespace Domain.Model.Test.Book;

public class BookTest
{
    [Fact]
    public void should_throw_exception_if_bookTitle_null()
    {
        var courser = () =>
        {
            BookBuilder.Instance()
                .WithBookTitle(null)
                .Build();
        };
        courser.Should().Throw<BookTitleInvalidObjectException>();
    }

    [Fact]
    public void should_throw_exception_if_publishYear_equalto_zero()
    {
        var courser = () =>
        {
            BookBuilder.Instance()
                .WithBookTitle(BookTitle.CreateInstance(nameof(BookTitle)))
                .WithPublishYear(0)
                .Build();
        };
        courser.Should().Throw<BookPublishYearException>();
    } 
    
    [Fact]
    public void should_throw_exception_if_AuthorId_equalto_empty()
    {
        var courser = () =>
        {
            BookBuilder.Instance()
                .WithBookTitle(BookTitle.CreateInstance(nameof(BookTitle)))
                .WithPublishYear(2014)
                .WithAuthorId(Guid.Empty)
                .Build();
        };
        courser.Should().Throw<BookAuthorIdInvalidException>();
    } 
    
    [Fact]
    public void should_instance_with_out_exception()
    {
        var courser = () =>
        {
            BookBuilder.Instance()
                .WithBookTitle(BookTitle.CreateInstance(nameof(BookTitle)))
                .WithPublishYear(2014)
                .WithAuthorId(Guid.NewGuid())
                .Build();
        };
        courser.Should().NotThrow();
    }
}