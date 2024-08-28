using Common.Response;
using Common.Response.Query;
using Domain.Model.Model.Author.IRepository;
using Domain.Model.Model.Author.QueryModel;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Author;

public class AuthorQueryRepository(DatabaseContext dbContext) : IAuthorQueryRepository
{
    public async Task<AuthorQueryModel?> GetById(Guid id)
    {
       return await dbContext.Authors.Where(x => x.Id == id).Select(x => new AuthorQueryModel
        {
            Id = x.Id,
            Name = x.Name
        }).AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<Domain.Model.Model.Author.Author?> Load(Guid id)
    {
        return await dbContext.Authors.Where(x => x.Id == id).AsTracking().FirstOrDefaultAsync();
    }

    public async Task<DataList<AuthorQueryModel>> GetList(DataRequest request)
    {
        return await dbContext.Authors.Select(x => new AuthorQueryModel
        {
            Id = x.Id,
            Name = x.Name
        }).ToDataSourceResultAsync(request);
    }

    public async Task<DataList<AuthorBookQueryModel>> GetAuthorBooksById(Guid id,DataRequest request)
    {
         return await dbContext.Authors.Where(q=>q.Id==id).Select(x => new AuthorBookQueryModel
        {
            Id = x.Id,
            Name = x.Name,
            TitleBook = x.Books.Select(s=>s.BookTitle).FirstOrDefault()!.Title
        }).ToDataSourceResultAsync(request);
    }

    public async Task<Guid?> GetAuthorId(Guid id)
    {
        return await dbContext.Authors.Select(s => s.Id).FirstOrDefaultAsync(q => q == id);
    }
}