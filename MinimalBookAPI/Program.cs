var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var books = new List<Book>
{
    new Book { Id = 1, Title = "Book 1", Author = "Author Generic 1" },
    new Book { Id = 2, Title = "Book 2", Author = "Author Generic 2" },
    new Book { Id = 3, Title = "Book 3", Author = "Author Generic 3" },
    new Book { Id = 4, Title = "Book 4", Author = "Author Generic 4" },
    new Book { Id = 5, Title = "Book 5", Author = "Author Generic 5" },
};

app.MapGet("/book", () =>
{
    return books;
});

app.MapGet("/book/{id}", (int id) =>
{
    var book = books.Find(b  => b.Id == id);

    if (book is null)
        return Results.NotFound("Livro não encontrado!");

    return Results.Ok(book);
});

app.MapPost("/book/add", (Book book) =>
{
    books.Add(book);

    return books;
});

app.MapPut("/book/edit/{id}", (Book updatedBook, int id) =>
{
    var book = books.Find(b => b.Id == id);

    if (book is null)
        return Results.NotFound("Livro não encontrado!");

    book.Title = updatedBook.Title;
    book.Author = updatedBook.Author;

    return Results.Ok(book);
});

app.MapDelete("/book/delete/{id}", (int id) =>
{
    var book = books.Find(b => b.Id == id);

    if (book is null)
        return Results.NotFound("Livro não encontrado!");

    books.Remove(book);

    return Results.Ok(book);
});

app.Run();

public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
}
