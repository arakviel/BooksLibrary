namespace BooksLibrary.Exceptions;

class ValidationException : ArgumentException
{
    public Dictionary<string, string> Errors { get; set; }

    public ValidationException(string message, Dictionary<string, string> errors)
        : base(message)
    {
        Errors = errors;
    }
}
