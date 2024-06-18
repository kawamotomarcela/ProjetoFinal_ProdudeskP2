using ProjetoAPI.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoAPI.Models
{
    public class Produto
    {

        private int _id;
        private string _nome;
        private string _descricao;
        private string _fornecedor;
        private int _quantidade;
        private double _preco;
        private string _imagemUrl;

        private Produto() { }

        public Produto(string nome, string descricao, string fornecedor, int quantidade, double preco, int categoriaId, string imagemUrl)
        {
            Nome = nome;
            Descricao = descricao;
            Fornecedor = fornecedor;
            Quantidade = quantidade;
            Preco = preco;
            CategoriaId = categoriaId;
            ImagemUrl = imagemUrl;
        }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get => _id; set { _id = value; } }

        public string Nome
        {
            get => _nome;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("O nome não pode estar vazio! ");
                }
                _nome = value;
            }
        }

        public string Descricao
        {
            get => _descricao;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("A descrição não pode estar vazia! ");

                }
                _descricao = value;
            }
        }

        public string Fornecedor
        {
            get => _fornecedor;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("O fornecedor não pode estar vazio! ");
                }
                _fornecedor = value;
            }
        }

        public int Quantidade
        {
            get => _quantidade;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        "A quantidade deve ser maior que 0"
                        );
                }
                _quantidade = value;
            }
        }

        public double Preco
        {
            get => _preco;
            set
            {
                if(value <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        "O preço deve ser maior que 0"
                        );
                }
                _preco = value;
            }
        }

        public string ImagemUrl
        {
            get => _imagemUrl;
            set
            {
                if (!Uri.IsWellFormedUriString(value, UriKind.Absolute))
                {
                    throw new ArgumentException("A URL da imagem não é válida!");
                }
                _imagemUrl = value;
            }
        }

        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }
  
    }
}
