using Common.Exception;

namespace Domain.Model.Model.Book;

public class BookTitleNullException() : BaseException("Title Null");
public class BookTitleLengthException() : BaseException("Title Length");
public class BookTitleInvalidObjectException() : BaseException("Book Title Invalid");
public class BookPublishYearException() : BaseException("Invalid Publish Year");
public class BookAuthorIdInvalidException() : BaseException("Invalid Publish Year");