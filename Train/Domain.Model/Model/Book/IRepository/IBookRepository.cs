using Common.Response;
using Domain.Model.Model.Book.QueryModel;

namespace Domain.Model.Model.Book.IRepository;

public interface IBookQueryRepository
{
    Task<ServiceResponse<BookQueryModel>> GetById(Guid id);
    Task<ServiceResponse<DataList<BookQueryModel>>> GetList();
}