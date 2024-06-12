import http from './http-common';

class CategoryService {
  getAll = () => {
    return http.get('/categorias').then(data => {
      return data.data
    });
  };
  
  get = (id) => {
    return http.get(`/categorias/${id}`);
  };
  
  create = (data) => {
    return http.post('/categorias', data);
  };
  
  update = (id, data) => {
    return http.put(`/categorias/${id}`, data);
  };
  
  remove = (id) => {
    return http.delete(`/categorias/${id}`);
  };
  
}
export default new CategoryService();
