using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Models
{
    public class Categoria
    {
        private int _id;
        private string _nome;

        private Categoria() { }

        public Categoria(string nome)
        {
            Nome = nome;
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
        
    }
}
