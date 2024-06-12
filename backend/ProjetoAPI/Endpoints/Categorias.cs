using ProjetoAPI.DataBase;
using ProjetoAPI.Models;
using ProjetoAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Migrations;
namespace ProjetoAPI.Endpoints
{
    public static class Categorias
    {

        public static void RegistrarEndpointsCategorias(this IEndpointRouteBuilder rotas)
        {
            RouteGroupBuilder rotaCategorias = rotas.MapGroup("/categorias");

            // GET /categorias
            rotaCategorias.MapGet("/", (ProdutosDbContext dbContext) =>
            {
                try
                {
                    List<Categoria> categorias = dbContext.Categorias.ToList();
                    return TypedResults.Ok(categorias);
                }
                catch (Exception)
                {
                    throw new ArgumentException("Ocorreu um erro durante a busca das categorias.");
                }
            }).Produces<List<Categoria>>();

            // GET /categorias/{Id}
            rotaCategorias.MapGet("/{Id}", (ProdutosDbContext dbContext, int Id) =>
            {
                try
                {
                    Categoria categoria = dbContext.Categorias.Find(Id);
                    if (categoria == null)
                    {
                        return Results.NotFound("Categoria não encontrada.");
                    }

                    return TypedResults.Ok(categoria);
                }
                catch (Exception)
                {
                    throw new ArgumentException("Ocorreu um erro durante a busca da categoria.");
                }
            }).Produces<Categoria>();

            // GET /categorias/{Id}/produtos
            rotaCategorias.MapGet("/{Id}/produtos", (ProdutosDbContext dbContext, int Id) =>
            {
                try
                {
                    var categoria = dbContext.Categorias.Find(Id);
                    if (categoria == null)
                    {
                        return Results.NotFound("Categoria não encontrada.");
                    }

                    var produtos = dbContext.Produtos.Where(p => p.CategoriaId == Id).ToList();

                    return TypedResults.Ok(produtos);
                }
                catch (Exception)
                {
                    throw new ArgumentException("Ocorreu um erro durante a busca dos produtos da categoria.");
                }
            }).Produces<List<Produto>>();

            // POST /categorias
            rotaCategorias.MapPost("/", (ProdutosDbContext dbContext, CategoriaDtoInput categoria) =>
            {
                try
                {

                    var categoriaExistente = dbContext.Categorias.FirstOrDefault(c => c.Nome == categoria.Nome);
                    if (categoriaExistente != null)
                    {
                        throw new ArgumentException("Categoria já existente! ");
                    }

                    Categoria _novaCategoria = categoria.ToCategoria();
                    var novaCategoria = dbContext.Categorias.Add(_novaCategoria);
                    dbContext.SaveChanges();

                    return TypedResults.Created($"/categorias/{_novaCategoria.Id}", categoria);
                }
                catch (Exception)
                {
                    throw new ArgumentException("Ocorreu um erro durante o cadastro da categoria.");
                }
            });



            //TESTE
            // POST     /categorias/seed
            rotaCategorias.MapPost("/seed", (ProdutosDbContext dbContext, bool excluirCategoriasExistentes = false) =>
            {
                
                Categoria categoria1 = new Categoria("Samsung");
                Categoria categoria2 = new Categoria("Apple");
                Categoria categoria3 = new Categoria("Motorola");
                Categoria categoria4 = new Categoria("Xiaomi");
                Categoria categoria5 = new Categoria("Transsion");
                Categoria categoria6 = new Categoria("OPPO");

                if (excluirCategoriasExistentes)
                {
                    dbContext.Categorias.RemoveRange(dbContext.Categorias);
                }

                dbContext.Categorias.AddRange([
                    categoria1,
                    categoria2,
                    categoria3,
                    categoria4,
                    categoria5,
                    categoria6
                ]);

                dbContext.SaveChanges();

                return TypedResults.Created();
            });


            // PUT /categorias/{Id}
            rotaCategorias.MapPut("/{Id}", (ProdutosDbContext dbContext, int Id, CategoriaDtoInput categoria) =>
            {
                try
                {
                    Categoria categoriaEncontrada = dbContext.Categorias.Find(Id);
                    if (categoriaEncontrada == null)
                    {
                        return Results.NotFound("Categoria não encontrada.");
                    }

                    dbContext.Entry(categoriaEncontrada).CurrentValues.SetValues(categoria);
                    dbContext.SaveChanges();

                    return TypedResults.NoContent();
                }
                catch (Exception)
                {
                    throw new ArgumentException("Ocorreu um erro durante a atualização da categoria.");
                }
            });

            // DELETE /categorias/{Id}
            rotaCategorias.MapDelete("/{Id}", (ProdutosDbContext dbContext, int Id) =>
            {
                try
                {
                    Categoria categoriaEncontrada = dbContext.Categorias.Find(Id);
                    if (categoriaEncontrada == null)
                    {
                        return Results.NotFound("Categoria não encontrada.");
                    }

                    dbContext.Categorias.Remove(categoriaEncontrada);
                    dbContext.SaveChanges();

                    return TypedResults.NoContent();
                }
                catch (Exception)
                {
                    throw new ArgumentException("Ocorreu um erro durante a remoção da categoria.");
                }
            });
        }

    }
}
