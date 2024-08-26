using Domain.Model.Model.Book;
using FluentAssertions;

namespace Domain.Model.Test.Book;

internal class BookTitleTest
{
    [Fact]
    public void should_throw_exception_if_title_null()
    {
        var courser = () =>
        {
            BookTitle.CreateInstance(null);
        };

        courser.Should().Throw<Exception>();
    }
}