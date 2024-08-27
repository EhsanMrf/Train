 using Common.OperationCrud;
using Common.Response;
using Domain.Model.Model.Book.IRepository;
using Infrastructure.Database;

namespace Infrastructure.Repository.Book;

public class BookCommandRepository : IBookCommandRepository
{
    private readonly ICrudManager<Domain.Model.Model.Book.Book,Guid,DatabaseContext> _crudManager;

    public BookCommandRepository(ICrudManager<Domain.Model.Model.Book.Book, Guid, DatabaseContext> crudManager)
    {
        _crudManager = crudManager;
    }


    public async Task Create(Domain.Model.Model.Book.Book command)
    {
        await _crudManager.Create(command);
    }

    public async Task<Domain.Model.Model.Book.Book> Update(Domain.Model.Model.Book.Book command)
    {
        return await _crudManager.Update(command);
    }
}