using Common.Exception;
using Infrastructure.Locazation.Book;

namespace Application.Service.Exception;

public class BookNotFoundServiceException() : BaseException(BookResource.BookNoutFoundService);
public class BookDuplicateServiceException() : BaseException(BookResource.BookDuplicate);