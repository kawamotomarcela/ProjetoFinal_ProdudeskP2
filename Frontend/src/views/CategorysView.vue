<script setup>
import { ref, onMounted } from 'vue';
import categoryService from "../services/categoryService";

const categorias = ref([]);
const showDeletePopup = ref(false);
let categoriaToDelete = null;
let produtosVinculados = [];

const confirmDelete = async (categoria) => {
  categoriaToDelete = categoria;
  try {
    produtosVinculados = await categoryService.getProductsByCategoryId(categoria.id);
    showDeletePopup.value = true;
  } catch (error) {
    console.error('Erro ao buscar produtos vinculados:', error);
  }
};

const deleteCategory = async () => {
  if (!categoriaToDelete) return;

  try {
    await categoryService.remove(categoriaToDelete.id);
    categorias.value = categorias.value.filter(cat => cat.id !== categoriaToDelete.id);
    categoriaToDelete = null;
    showDeletePopup.value = false;
  } catch (error) {
    console.error('Erro ao deletar categoria:', error);
  }
};

const cancelDelete = () => {
  categoriaToDelete = null;
  showDeletePopup.value = false;
};

onMounted(async () => {
  try {
    categorias.value = await categoryService.getAllWithProductsCount();
  } catch (error) {
    console.error('Erro ao carregar categorias com contagem de produtos:', error);
  }
});
</script>

<template>
  <div class="row m-3 mt-2 g-4 dashboard">
    <div class="col-6 d-flex align-items-center justify-content-center">
      <div class="dashboard-content m-2">
        <h4>Welcome Back, Admin</h4>
        <p class="">Admin Dashboard, Produdesk</p>
      </div>
      <div class="">
        <img src="../assets/img/okuyaso.jpg" class="img-fluid illustration-img" alt="imagem-legal" />
      </div>
    </div>
    <div class="col-6 d-flex align-items-start text-center">
      <div class="flex-grow-1 m-2">
        <h4 class="mb-2">Categorias Cadastradas</h4>
        <div class="category-count">
          <h2 class="m-2 d-flex align-items-center justify-content-center">{{ categorias.length }}
            <box-icon name="box" size="lg" class="box-icon m-1"></box-icon>
          </h2>
        </div>
      </div>
    </div>

    <div class="row mt-4">
      <div class="col-md-12">
        <div class="card">
          <div class="card-body">
            <h5 class="card-title">Categorias Cadastradas</h5>
            <table class="table">
              <thead>
                <tr>
                  <th>ID</th>
                  <th>Categoria</th>
                  <th>Total de Produtos</th>
                  <th>Ações</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(categoria, index) in categorias" :key="index">
                  <td>{{ categoria.id }}</td>
                  <td>{{ categoria.nome }}</td>
                  <td>{{ categoria.totalProdutos }}</td>
                  <td>
                    <button @click="confirmDelete(categoria)" class="btn btn-sm btn-danger">Excluir</button>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>

    <div v-if="showDeletePopup" class="popup">
      <div class="popup-content">
        <p v-if="produtosVinculados.length > 0">
          Existem {{ produtosVinculados.length }} produto(s) vinculado(s) a esta categoria. 
          Se você continuar, esses produtos também serão excluídos. Deseja continuar?
        </p>
        <p v-else>
          Tem certeza que deseja excluir esta categoria?
        </p>
        <div class="popup-buttons">
          <button @click="deleteCategory" class="btn btn-danger m-2">Confirmar</button>
          <button @click="cancelDelete" class="btn btn-secondary">Cancelar</button>
        </div>
      </div>
    </div>


  </div>
</template>

<style scoped>
.popup {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
}

.popup-content {
  background-color: #fff;
  padding: 20px;
  border-radius: 8px;
  width: 300px;
  text-align: center;
}

.popup-buttons {
  margin-top: 20px;
}
</style>