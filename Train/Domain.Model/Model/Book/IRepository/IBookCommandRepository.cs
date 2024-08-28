using Common.TransientService;

namespace Domain.Model.Model.Book.IRepository;

public interface IBookCommandRepository : ITransientService
{
    Task<bool> Create(Book command);
    Task<Book> Update(Book command);
}