using Common.Response;
using Domain.Model.Model.Book.Command;

namespace Domain.Model.Model.Book.IRepository;

public interface IBookCommandRepository
{
    Task Create(AddBookCommand command);
    Task<ServiceResponse<Book>> Update(UpdateBookCommand command);
}