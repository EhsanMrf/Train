using Common.Exception;

namespace Domain.Model.Model.Author;

public class AuthorNameNullException() : BaseException("Name Null");
public class AuthorTitleLengthException() : BaseException("Title Length");