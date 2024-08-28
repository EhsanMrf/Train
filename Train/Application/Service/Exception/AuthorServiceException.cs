using Common.Exception;
using Infrastructure.Locazation.Author;

namespace Application.Service.Exception;

public class AuthorServiceException() : BaseException(AuthorResource.AuthorDuplicate);
public class AuthorFoundServiceException() : BaseException(AuthorResource.AuthorNoutFoundService);