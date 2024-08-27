using Common.TransientService;
using Domain.Model.Model.Book.QueryModel;

namespace Domain.Model.Model.Book.IRepository;

public interface IBookQueryRepository : ITransientService
{
    Task<BookQueryModel?> GetById(Guid id);
    Task<IEnumerable<BookQueryModel>> GetList();
}