using Domain.Model.Model.Author;
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
            new Model.Author.Author(null);
        };

        courser.Should().Throw<AuthorNameNullException>();
    }

    [Fact]
    public void should_throw_exception_if_name_less_two()
    {
        var courser = () =>
        {
            new Model.Author.Author("A");
        };

        courser.Should().Throw<AuthorNameLengthException>();
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