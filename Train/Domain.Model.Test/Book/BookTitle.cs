using Domain.Model.Model.Book;
using FluentAssertions;

namespace Domain.Model.Test.Book;

public class BookTitleTest
{
    [Fact]
    public void should_throw_exception_if_title_null()
    {
        var courser = () =>
        {
            BookTitle.CreateInstance(null);
        };

        courser.Should().Throw<BookTitleNullException>();
    }
    [Fact]
    public void should_throw_exception_if_title_less_two()
    {
        var courser = () =>
        {
            BookTitle.CreateInstance("A");
        };

        courser.Should().Throw<BookTitleLengthException>();
    }

    [Fact]
    public void should_throw_exception_if_title_equalto_two()
    {
        var courser = () =>
        {
            BookTitle.CreateInstance("Ab");
        };

        courser.Should().Throw<BookTitleLengthException>();
    }

    [Fact]
    public void should_instance_with_out_exception()
    {
        var courser = () =>
        {
            BookTitle.CreateInstance("Book Title");
        };

        courser.Should().NotThrow();
    }
}