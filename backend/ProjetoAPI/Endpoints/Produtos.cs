using Microsoft.EntityFrameworkCore;
using ProjetoAPI.DataBase;
using Microsoft.AspNetCore.Builder;
using ProjetoAPI.DTOs;
using ProjetoAPI.Models;
using static ProjetoAPI.DTOs.ProdutoDtoInput;
using ProjetoAPI.Utils;
namespace ProjetoAPI.Endpoints
{
    public static class Produtos
    {
        public static void RegistrarEndpointsProdutos(this IEndpointRouteBuilder rotas)
        {
            RouteGroupBuilder rotaProdutos = rotas.MapGroup("/produtos");


            // GET      /produtos
            rotaProdutos.MapGet("/", (ProdutosDbContext dbContext, int pagina = 1, int tamanhoPagina = 10) =>
            {
                try
                {
                    var totalItens = dbContext.Produtos.Count();

                    List<Produto> produtos = dbContext.Produtos.Include(p => p.Categoria).Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToList();
                    ListaPaginada<Produto> listaProdutos = new ListaPaginada<Produto>(produtos, pagina, tamanhoPagina, totalItens);

                    return TypedResults.Ok(listaProdutos);
                }
                catch (Exception)
                {
                    throw new ArgumentException("Ocorreu um erro durante a busca dos produtos ");
                }
            }).Produces<ListaPaginada<Produto>>();


            // GET      /produtos/{Id}
            rotaProdutos.MapGet("/{Id}", (ProdutosDbContext dbContext, int Id) =>
            {
                try
                {
                    Produto produto = dbContext.Produtos.Include(p => p.Categoria).FirstOrDefault(p => p.Id == Id);
                    if (produto == null)
                    {
                        return Results.NotFound();
                    }

                    return TypedResults.Ok(produto);
                }
                catch (Exception)
                {
                    throw new ArgumentException("Ocorreu um erro durante a busca do produto.");
                }
            }).Produces<Produto>();



            // POST     /produtos
            rotaProdutos.MapPost("/", (ProdutosDbContext dbContext, ProdutoDtoInput produto) =>
            {
                try
                {
                    var produtoExistente = dbContext.Produtos.FirstOrDefault(p => p.Nome == produto.Nome);
                    if (produtoExistente != null)
                    {
                        throw new ArgumentException("Produto já existente! ");
                    }

                    Produto _novoProduto = produto.ToProduto();
                    var novoProduto = dbContext.Produtos.Add(_novoProduto);
                    dbContext.SaveChanges();

                    return TypedResults.Created($"/produtos/{_novoProduto.Id}", _novoProduto);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Ocorreu um erro durante o cadastro do produto.", ex);
                }
            });


            //TESTE
            // POST     /produtos/seed
            rotaProdutos.MapPost("/seed", (ProdutosDbContext dbContext, bool excluirProdutosExistentes = false) =>
            {
                Produto celular1 = new Produto("Iphone 13" ,"Câmera boa", "Rodrigo", 5, 3000, 2);
                Produto celular2 = new Produto("Motorola Edge", "Excelente", "Marcelo", 7, 1000, 3);
                Produto celular3 = new Produto("POCO C65", "Resistente", "André", 4, 2300, 4);
                Produto celular4 = new Produto("Samsung S10", "Muito bom", "Michel", 8, 2600, 1);
                Produto celular5 = new Produto("Iphone 15", "Boa qualidade", "Daniel", 7, 5000, 2);
                Produto celular6 = new Produto("OPPO A79", "Ágil", "Marcus", 6, 1300, 6);

                if (excluirProdutosExistentes)
                {
                    dbContext.Produtos.RemoveRange(dbContext.Produtos);
                }

                dbContext.Produtos.AddRange([
                    celular1,
                    celular2,
                    celular3,
                    celular4,
                    celular5,
                    celular6,
                ]);

                dbContext.SaveChanges();

                return TypedResults.Created();
            });


            // PUT /produtos/{Id}
            rotaProdutos.MapPut("/{Id}", (ProdutosDbContext dbContext, int Id, ProdutoDtoInput produto) =>
            {
                try
                {
                    Produto produtoEncontrado = dbContext.Produtos.Find(Id);
                    if (produtoEncontrado is null)
                    {
                        return Results.NotFound("Produto não encontrado");
                    }

                    dbContext.Entry(produtoEncontrado).CurrentValues.SetValues(produto);
                    dbContext.SaveChanges();

                    return Results.NoContent();
                }

                catch (Exception)
                {
                    throw new ArgumentException("Ocorreu um erro durante a atualização do produto.");
                }
            });

            // PUT /produtos/{Id}/atualizar-quantidade
            rotaProdutos.MapPut("/{Id}/atualizar-quantidade", (ProdutosDbContext dbContext, int Id, int novaQuantidade) =>
            {
                try
                {
                    Produto produtoEncontrado = dbContext.Produtos.Find(Id);
                    if (produtoEncontrado is null)
                    {
                        return Results.NotFound("Produto não encontrado");
                    }

                    produtoEncontrado.Quantidade = novaQuantidade;
                    dbContext.SaveChanges();

                    return TypedResults.Ok(produtoEncontrado); 
                }
                catch (Exception)
                {
                    throw new ArgumentException("Ocorreu um erro durante a atualização da quantidade do produto.");
                }
            }).Produces<Produto>();


            // DELETE   /produtos/{Id}
            rotaProdutos.MapDelete("/{Id}", (ProdutosDbContext dbContext, int Id) =>
            {
                try
                {
                    Produto produtoEncontrado = dbContext.Produtos.Find(Id);
                    if (produtoEncontrado == null)
                    {
                        return Results.NotFound();
                    }

                    dbContext.Produtos.Remove(produtoEncontrado);

                    dbContext.SaveChanges();

                    return TypedResults.NoContent();
                }
                catch (Exception)
                {
                    throw new ArgumentException("Ocorreu um erro durante a remoção do produto.");
                }
            });

        }

    }
}
