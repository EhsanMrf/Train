 using Common.OperationCrud;
using Domain.Model.Model.Book.IRepository;
using Domain.Model.Model.Book.QueryModel;
using Infrastructure.Database;

namespace Infrastructure.Repository.Book;

public class BookQueryRepository : IBookQueryRepository
{
    private readonly ICrudManager<Domain.Model.Model.Book.Book, Guid, DatabaseContext> _repositoryManager;
    private readonly ICrudManager<Domain.Model.Model.Author.Author, Guid, DatabaseContext> _authorCrudManager;

    public BookQueryRepository(ICrudManager<Domain.Model.Model.Book.Book, Guid, DatabaseContext> repositoryManager, ICrudManager<Domain.Model.Model.Author.Author, Guid, DatabaseContext> authorCrudManager)
    {
        _repositoryManager = repositoryManager;
        _authorCrudManager = authorCrudManager;
    }

    public async Task<BookQueryModel?> GetById(Guid id)
    {
        return await _repositoryManager.FindById(id, x => new BookQueryModel
        {
            PublishYear = x.PublishYear,
            AuthorName =  _authorCrudManager.GetEntity().Where(q=>q.Id==x.AuthorId)
                .Select(q=>q.Name).FirstOrDefault(),
            Title = x.BookTitle.Title,
            Id = x.Id,
        });
    }

    public async Task<IEnumerable<BookQueryModel>> GetList()
    {
        return await _repositoryManager.GetList(x => new BookQueryModel()
        {
            AuthorName = _authorCrudManager.GetEntity().Where(q => q.Id == x.AuthorId)
                .Select(q => q.Name).FirstOrDefault(),
            PublishYear = x.PublishYear,
            Title = x.BookTitle.Title,
            Id = x.Id,
        });
    }
}