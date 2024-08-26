namespace Domain.Model.Model.Book;

public class BookTitleNullException() : Exception("Title Null");
public class BookTitleLengthException() : Exception("Title Length");
public class BookTitleInvalidObjectException() : Exception("Book Title Invalid");
public class BookPublishYearException() : Exception("Invalid Publish Year");
public class BookAuthorIdInvalidException() : Exception("Invalid Publish Year");