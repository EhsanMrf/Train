using Common.Response;
using Domain.Model.Model.Author.QueryModel;

namespace Domain.Model.Model.Author.IRepository;

public interface IAuthorQueryRepository
{
    Task<ServiceResponse<AuthorQueryModel>> GetById(Guid id);
    Task<ServiceResponse<DataList<AuthorQueryModel>>> GetList();
    Task<ServiceResponse<DataList<AuthorBookQueryModel>>> GetAuthorBooksById();
}