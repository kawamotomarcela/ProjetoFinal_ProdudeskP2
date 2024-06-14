import http from './http-common';

class CategoryService {
  getAll = () => {
    return http.get('/categorias').then(data => {
      return data.data
    });
  };
  
  getAllWithProductsCount = async () => {
    try {
      const categories = await this.getAll();

      const categoriesWithCount = await Promise.all(categories.map(async category => {
        const productsResponse = await http.get(`/categorias/${category.id}/produtos`);
        const products = productsResponse.data;
        return {
          ...category,
          totalProdutos: products.length
        };
      }));

      return categoriesWithCount;
    } catch (error) {
      console.error('Erro ao obter categorias com contagem de produtos:', error);
      throw error;
    }
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

  getProductsByCategoryId = async (categoryId) => {
    try {
      const productsResponse = await http.get(`/categorias/${categoryId}/produtos`);
      return productsResponse.data;
    } catch (error) {
      console.error(`Erro ao obter produtos da categoria ${categoryId}:`, error);
      throw error;
    }
  };
  
}
export default new CategoryService();
