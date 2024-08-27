using Common.Response;
using MediatR;

namespace Domain.Model.Model.Book.Command;

public class UpdateBookCommand : IRequest<ServiceResponse<Book>>
{
    public Guid Id { get; set; }
    public BookTitle BookTitle { get; set; } = null!;
    public int PublishYear { get;  set; }
    public Guid AuthorId { get; set; }
}