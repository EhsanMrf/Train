using MediatR;

namespace Domain.Model.Model.Book.Command;

public record AddBookCommand(string Title, int PublishYear,Guid AuthorId):IRequest<bool>;