using Common.Exception;
using Infrastructure.Locazation.Author;

namespace Domain.Model.Model.Author;

public class AuthorNameNullException() : BaseException(AuthorResource.AuthorNameNull);
public class AuthorNameLengthException() : BaseException(AuthorResource.AuthorNameLength);