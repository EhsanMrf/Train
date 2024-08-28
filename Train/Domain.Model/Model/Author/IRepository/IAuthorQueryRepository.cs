using Common.TransientService;
using Domain.Model.Model.Author.QueryModel;

namespace Domain.Model.Model.Author.IRepository;

public interface IAuthorQueryRepository : ITransientService
{
    Task<AuthorQueryModel?> GetById(Guid id);
    Task<Author?> Load(Guid id);
    Task<IEnumerable<AuthorQueryModel>> GetList();
    Task<IEnumerable<AuthorBookQueryModel>> GetAuthorBooksById(Guid id);
}