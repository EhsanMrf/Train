using Domain.Model.Model.Book;
using FluentAssertions;

namespace Domain.Model.Test.Author;

public class AuthorTest
{

    [Fact]
    public void should_throw_exception_if_name_null()
    {
        var courser = () =>
        {
            BookTitle.CreateInstance(null);
        };

        courser.Should().Throw<BookTitleNullException>();
    }

    [Fact]
    public void should_throw_exception_if_name_less_two()
    {
        var courser = () =>
        {
            BookTitle.CreateInstance("A");
        };

        courser.Should().Throw<BookTitleLengthException>();
    }

    [Fact]
    public void should_instance_with_out_exception()
    {
        var courser = () =>
        {
            var author = new Model.Author.Author(nameof(Model.Author.Author));
        };
        courser.Should().NotThrow();
    }
}