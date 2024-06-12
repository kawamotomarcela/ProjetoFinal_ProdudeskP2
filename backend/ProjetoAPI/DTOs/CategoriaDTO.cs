using ProjetoAPI.Models;

namespace ProjetoAPI.DTOs
{
    public class CategoriaDtoInput
    {
        public string Nome { get; set; }


        public Categoria ToCategoria()
        {
            return new Categoria(Nome);
        }
    }

}

