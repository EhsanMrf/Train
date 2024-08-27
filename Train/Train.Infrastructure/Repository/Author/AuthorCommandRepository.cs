using Common.OperationCrud;
using Domain.Model.Model.Author.IRepository;
using Infrastructure.Database;

namespace Infrastructure.Repository.Author;

public class AuthorCommandRepository : IAuthorCommandRepository
{
    private readonly ICrudManager<Domain.Model.Model.Author.Author, Guid, DatabaseContext> _crudManager;

    public AuthorCommandRepository(ICrudManager<Domain.Model.Model.Author.Author, Guid, DatabaseContext> crudManager)
    {
        _crudManager = crudManager;
    }

    public async Task Create(Domain.Model.Model.Author.Author command)
    {
        await _crudManager.Create(command);
    }

    public async Task<Domain.Model.Model.Author.Author> Update(Domain.Model.Model.Author.Author command)
    {
        return await _crudManager.Update(command);
    }
}