var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// 1. dados
var livros = new List<Livro>
{
    new Livro { Id = 1, Titulo = "Clean Code", Ano = 2008, Autor = "Martin" },
    new Livro { Id = 2, Titulo = "O Programador Pragmático", Ano = 1999, Autor = "Hunt" }
};

// 2. rotas
app.MapGet("/", () => "Hello World!");
// GET /livros              -> todos
// GET /livros?autor=martin -> só os do autor
app.MapGet("/livros", (string? autor) =>
{
    if (string.IsNullOrWhiteSpace(autor))
        return Results.Ok(livros);
 
    var filtrados = livros.Where(l =>
        l.Autor.Contains(autor, StringComparison.OrdinalIgnoreCase));
    return Results.Ok(filtrados);
});

// O {id} da rota chega no parâmetro int id: 
// o nome precisa ser o mesmo. O :int só aceita números.
app.MapGet("/livros/{id:int}", (int id) =>
{
    var livro = livros.FirstOrDefault(l => l.Id == id);
 
    if (livro is null)
        return Results.NotFound(new { mensagem = "Livro não encontrado." });
 
    return Results.Ok(livro);
});

// Parâmetro do tipo classe: 
// a Minimal API lê o corpo JSON e preenche um Livro sozinha.
app.MapPost("/livros", (Livro novo) =>
{
    var erro = ValidarLivro(novo);
    if (erro is not null)
        return Results.BadRequest(new { mensagem = erro });
    // Quem gera o Id é o servidor, nunca o cliente
    novo.Id = livros.Count > 0 ? livros.Max(l => l.Id) + 1 : 1;
    livros.Add(novo);
 
    return Results.Created($"/livros/{novo.Id}", novo);
});

// Dois parâmetros, dois lugares: id vem da rota, dados vem do corpo. 
// Results.BadRequest(erro) devolve a mensagem como texto.
app.MapPut("/livros/{id:int}", (int id, Livro dados) =>
{
    var livro = livros.FirstOrDefault(l => l.Id == id);
    if (livro is null) return Results.NotFound();
 
    var erro = ValidarLivro(dados);
    if (erro is not null) return Results.BadRequest(erro);
 
    livro.Titulo = dados.Titulo;
    livro.Autor = dados.Autor;
    livro.Ano = dados.Ano;
    livro.Disponivel = dados.Disponivel;
    return Results.Ok(livro);
});

// Mandar o mesmo DELETE duas vezes: 
// a primeira devolve 204, a segunda 404. 
// O estado final é o mesmo (idempotente).
app.MapDelete("/livros/{id:int}", (int id) =>
{
    var livro = livros.FirstOrDefault(l => l.Id == id);
    if (livro is null) return Results.NotFound();
 
    livros.Remove(livro);
    return Results.NoContent();
});

// porta fixa: a turma toda usa o mesmo endereço
app.Run("http://localhost:5000");


static string? ValidarLivro(Livro livro)
{
    if (string.IsNullOrWhiteSpace(livro.Titulo))
        return "Título é obrigatório.";
    if (livro.Ano < 1450 || livro.Ano > DateTime.Now.Year)
        return "Ano inválido.";
    return null;
}

// 4. classes no fim, no futuro em outro file
public class Livro
{
    public int Id { get; set; }
    public string Titulo { get; set; } = "";
    public string Autor { get; set; } = "";
    public int Ano { get; set; }
    public bool Disponivel { get; set; } = true;
}