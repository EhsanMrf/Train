using Common.OperationCrud;
using Domain.Model.Model.Author.IRepository;
using Infrastructure.Database;

namespace Infrastructure.Repository.Author;

public class AuthorCommandRepository : IAuthorCommandRepository
{

    private readonly DatabaseContext _databaseContext;
    public AuthorCommandRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<bool> Create(Domain.Model.Model.Author.Author command)
    {
        _databaseContext.Authors.Add(command);
        return await _databaseContext.SaveChangesAsync() > 0;
    }

    public async Task<Domain.Model.Model.Author.Author> Update(Domain.Model.Model.Author.Author command)
    {
        _databaseContext.Authors.Update(command);
         await _databaseContext.SaveChangesAsync();
         return command;
    }
}