namespace ProjetoAPI.Utils
{


        public class ListaPaginada<T>
        {
            public List<T> Data { get; set; }
            public int PaginaAtual { get; set; }
            public int TamanhoPagina { get; set; }
            public int TotalPaginas => (int)Math.Ceiling(TotalItens / (double)TamanhoPagina);
            public int TotalItens { get; set; }

            public ListaPaginada(List<T> data, int paginaAtual, int tamanhoPagina, int totalItens)
            {
                Data = data;
                PaginaAtual = paginaAtual;
                TamanhoPagina = tamanhoPagina;
                TotalItens = totalItens;
            }
        }

    }
