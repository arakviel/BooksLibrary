using BooksLibrary.Exceptions;
using System.IO;
using System.Text.Json.Serialization;

namespace BooksLibrary.Entities;

class Book
{
    private readonly string _ISBN13;

    public string ISBN13
    {
        get
        {
            return _ISBN13;
        }

        init
        {
            if (value.Length != 17)
            {
                Errors.Add("ISBN13", "ISBN13 не може бути менше 13 символів з дефізами.");
            }

            _ISBN13 = value;
        }
    }

    private string _title;

    public string Title
    {
        get => _title;

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Errors.Add("Title", "Заголовок книги не може бути пустим.");
            }
            if (value.Length > 128)
            {
                Errors.Add("Title", "Заголовок книги не може бути пустим.");
            }

            _title = value;
        }
    }

    private string _description;

    public string Description
    {
        get => _description;

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Errors.Add("Description", "Опис книги не може бути пустим.");
            }

            _description = value;
        }
    }

    private string _poster;

    public string Poster
    {
        get => _poster;

        set
        {
            if (!Path.IsPathRooted(value))
            {
                Errors.Add("Poster", "Постер картинки має бути відносним шляхом.");
            }

            _poster = value;
        }
    }

    private DateTime _createdAt;

    public DateTime CreatedAt
    {
        get => _createdAt;

        set
        {
            if (_createdAt > DateTime.Now)
            {
                Errors.Add("CreatedAt", "Книга не може бути випущена в майбутньому.");
            }

            _createdAt = value;
        }
    }

    [JsonIgnore]
    public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();

    public Book(string isbn13, string title, string description, string poster, DateTime createdAt)
    {
        ISBN13 = isbn13;
        Title = title;
        Description = description;
        Poster = poster;
        CreatedAt = createdAt;

        if (Errors.Count > 0)
        {
            throw new ValidationException("Помилки валідації при створенні книги.", Errors);
        }
    }
}
