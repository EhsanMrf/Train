using Common.Response;
using MediatR;

namespace Domain.Model.Model.Book.Command;

public class UpdateBookCommand(Guid id, string title, int publishYear, Guid authorId)
    : IRequest<ServiceResponse<Book>>
{
    public Guid Id { get; set; } = id;
    public string Title { get; set; } = title;
    public int PublishYear { get;  set; } = publishYear;
    public Guid AuthorId { get; set; } = authorId;
}