using Application.Contract.Dto;
using Common.Response;
using Domain.Model.Model.Book.QueryModel;

namespace Application.Contract.Mapper;

public static class BookMapper
{
    public static DataList<BookDto> ToDto(this IEnumerable<BookQueryModel> models, int page, int pageSize)
    {
        var books = models.Select(x => x.ToDtoBook()).ToList();
        if (books == null || !books.Any())
            return new DataList<BookDto>(null, 0, 0, 0);

        return new DataList<BookDto>(books, books.Count(), page, pageSize);
    }

    public static ServiceResponse<BookDto> ToDto(this BookQueryModel model)
    {
        var book = model.ToDtoBook();
        return book==null ? new ServiceResponse<BookDto>() : new ServiceResponse<BookDto>(book);
    }

    private static BookDto ToDtoBook(this BookQueryModel model)
    {
        return new BookDto(model.Id, model.Title, model.PublishYear, model.AuthorName);
    }

}