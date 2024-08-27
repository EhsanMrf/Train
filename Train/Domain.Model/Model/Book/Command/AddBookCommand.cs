using MediatR;

namespace Domain.Model.Model.Book.Command;

public record AddBookCommand(BookTitle BookTitle, int PublishYear):IRequest;