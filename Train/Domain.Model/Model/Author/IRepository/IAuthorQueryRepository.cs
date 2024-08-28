using Common.Response;
using Common.Response.Query;
using Common.TransientService;
using Domain.Model.Model.Author.QueryModel;

namespace Domain.Model.Model.Author.IRepository;

public interface IAuthorQueryRepository : ITransientService
{
    Task<AuthorQueryModel?> GetById(Guid id);
    Task<Author?> Load(Guid id);
    Task<DataList<AuthorQueryModel>> GetList(DataRequest request);
    Task<DataList<AuthorBookQueryModel>> GetAuthorBooksById(Guid id,DataRequest request);
    Task<Guid?> GetAuthorId(Guid id);
}