using Domain.Model.Model.Book.IRepository;
using Infrastructure.Database;

namespace Infrastructure.Repository.Book;

public class BookCommandRepository : IBookCommandRepository
{

    private readonly DatabaseContext _dbContext;

    public BookCommandRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<bool> Create(Domain.Model.Model.Book.Book command)
    {
        _dbContext.Books.Add(command);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<Domain.Model.Model.Book.Book> Update(Domain.Model.Model.Book.Book command)
    {
        _dbContext.Books.Update(command);
        await _dbContext.SaveChangesAsync();
        return command;
    }
}