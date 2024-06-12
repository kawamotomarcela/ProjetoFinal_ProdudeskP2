import http from './http-common';

class ProductService {
  getAll = async (pagina, tamanho = 10) => {
    return await http.get(`/produtos?pagina=${pagina}&tamanhoPagina=${tamanho}`).then(response => {
      return response.data.data
    });
  };
  
  
  get = (id) => {
    return http.get(`/produtos/${id}`);
  };
  
  create = (data) => {
    return http.post('/produtos', data);
  };
  
  update = (id, data) => {
    return http.put(`/produtos/${id}`, data);
  };
  
  remove = (id) => {
    return http.delete(`/produtos/${id}`);
  };
}

export default new ProductService();
