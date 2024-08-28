using Common.Exception;
using Infrastructure.Locazation.Book;

namespace Application.Service.Exception;

public class BookNotFoundServiceException() : BaseException(BookResource.BookNoutFoundService);


//public class BookTitleLengthException() : BaseException(BookResource.BookTitleLength);
// 