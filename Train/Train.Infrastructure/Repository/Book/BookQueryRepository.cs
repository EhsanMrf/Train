 using Common.OperationCrud;
using Domain.Model.Model.Book.IRepository;
using Domain.Model.Model.Book.QueryModel;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Book;

public class BookQueryRepository : IBookQueryRepository
{

    private readonly DatabaseContext _dbContext;

    public BookQueryRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<BookQueryModel?> GetById(Guid id)
    {
        return await _dbContext.Books.Where(x => x.Id == id).Select(s => new BookQueryModel
        {
            AuthorName = _dbContext.Authors.Where(q => q.Id == s.AuthorId).Select(s => s.Name).FirstOrDefault(),
            Id = s.Id,
            PublishYear = s.PublishYear,
            Title = s.BookTitle.Title
        }).FirstOrDefaultAsync();
    }

    public async Task<Domain.Model.Model.Book.Book?> Load(Guid id)
    {
        return await _dbContext.Books.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<BookQueryModel>> GetList()
    {
        return await _dbContext.Books.Select(s => new BookQueryModel
        {
            AuthorName = _dbContext.Authors.Where(q => q.Id == s.AuthorId).Select(s => s.Name).FirstOrDefault(),
            Id = s.Id,
            PublishYear = s.PublishYear,
            Title = s.BookTitle.Title
        }).ToListAsync();
    }
}