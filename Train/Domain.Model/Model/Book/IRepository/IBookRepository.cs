using Common.Response;
using Common.Response.Query;
using Common.TransientService;
using Domain.Model.Model.Book.QueryModel;

namespace Domain.Model.Model.Book.IRepository;

public interface IBookQueryRepository : ITransientService
{
    Task<BookQueryModel?> GetById(Guid id);
    Task<Book?> Load(Guid id);
    Task<DataList<BookQueryModel>> GetList(DataRequest request);
}