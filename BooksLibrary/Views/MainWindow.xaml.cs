using BooksLibrary.Entities;
using BooksLibrary.Exceptions;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows;

namespace BooksLibrary;

/// <summary>
/// f.
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        try
        {
            Book book1 = new Book("978-3-16-148410-0", "Title", "Опис...", "/images/poster1.jpg", new DateTime(2000, 1, 1));
            Book book2 = new Book("978-3-16-148410-1", "Title 2", "Опис 2", "/images/poster2.jpg", new DateTime(2001, 2, 2));
            Book book3 = new Book("978-3-16-148410-2", "Title 3", "Опис 3", "/images/poster3.jpg", new DateTime(2002, 3, 3));

            List<Book> books = new List<Book>();
            books.Add(book1);
            books.Add(book2);
            books.Add(book3);

            // Серіалізуємо колекцію книг в JSON
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            string booksJson = JsonSerializer.Serialize(books, options);

            // Зберігаємо JSON у файл
            File.WriteAllText("books.json", booksJson);

            // Зчитуємо JSON з файлу
            string jsonString = File.ReadAllText("books.json");

            // Десеріалізуємо JSON у колекцію книг
            List<Book> books2 = JsonSerializer.Deserialize<List<Book>>(jsonString);
            foreach (var book in books)
            {
                string bookInfo = $"ISBN: {book.ISBN13}\n" +
                                  $"Title: {book.Title}\n" +
                                  $"Description: {book.Description}\n" +
                                  $"Image Path: {book.Poster}\n" +
                                  $"Publication Date: {book.CreatedAt}\n\n";
                MessageBox.Show(bookInfo, "Book Information");
            }
        }
        catch (ValidationException ex)
        {
            string errors = string.Join(", ", ex.Errors.Values);
            MessageBox.Show(errors);
        }
    }
}