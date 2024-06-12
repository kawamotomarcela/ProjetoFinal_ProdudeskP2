using ProjetoAPI.Models;

namespace ProjetoAPI.DTOs
{

    public class ProdutoDtoInput
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Fornecedor { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }
        public int CategoriaId { get; set; }



        public Produto ToProduto()
        {
            return new Produto(Nome, Descricao, Fornecedor, Quantidade, Preco, CategoriaId);
        }
    }
}


   