using Common.OperationCrud;
using Domain.Model.Model.Author.IRepository;
using Domain.Model.Model.Author.QueryModel;
using Infrastructure.Database;

namespace Infrastructure.Repository.Author;

public class AuthorQueryRepository : IAuthorQueryRepository
{
    private readonly ICrudManager<Domain.Model.Model.Author.Author, Guid, DatabaseContext> _crudManager;
    private readonly ICrudManager<Domain.Model.Model.Book.Book, Guid, DatabaseContext> _bookCrudManager;

    public AuthorQueryRepository(ICrudManager<Domain.Model.Model.Author.Author, Guid, DatabaseContext> crudManager, ICrudManager<Domain.Model.Model.Book.Book, Guid, DatabaseContext> bookCrudManager)
    {
        _crudManager = crudManager;
        _bookCrudManager = bookCrudManager;
    }


    public async Task<AuthorQueryModel?> GetById(Guid id)
    {
        return await _crudManager.FindById(id, x => new AuthorQueryModel
        {
            Id = x.Id,
            Name = x.Name
        });
    }

    public async Task<IEnumerable<AuthorQueryModel>> GetList()
    {
        return await _crudManager.GetList(x => new AuthorQueryModel
        {
            Id = x.Id,
            Name = x.Name
        });
    }

    public async Task<IEnumerable<AuthorBookQueryModel>> GetAuthorBooksById()
    {
        return await _crudManager.GetList(x => new AuthorBookQueryModel
        {
            Id = x.Id,
            Name = x.Name,
            TitleBook =  _bookCrudManager.GetEntity().Where(q=>q.AuthorId==x.Id)
                .Select(s=>s.BookTitle.Title).FirstOrDefault()
        });
    }
}