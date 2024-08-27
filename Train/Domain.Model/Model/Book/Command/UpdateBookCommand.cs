using Common.MediatR;
using Common.Response;

namespace Domain.Model.Model.Book.Command;

public class UpdateBookCommand : RequestMediator<Guid,ServiceResponse<Book>>
{
    public BookTitle BookTitle { get; set; } = null!;
    public int PublishYear { get;  set; }
    public Guid AuthorId { get; set; }
}