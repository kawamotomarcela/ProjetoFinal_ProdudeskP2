<script setup>
import { defineProps, defineEmits, computed, ref } from 'vue';
import productService from '../services/productService';
import { useRouter } from 'vue-router';

const props = defineProps(['products']);
const emit = defineEmits(['edit', 'remove']);
const router = useRouter();
const showRemovePopup = ref(false);
const productIdToRemove = ref(null);


const sortedProducts = computed(() => {
  return props.products.slice().sort((a, b) => a.id - b.id);
});


const editProduct = (productId) => {
  router.push({ name: 'editid', params: { id: productId } });
};

const confirmRemoveProduct = (productId) => {
  productIdToRemove.value = productId;
  showRemovePopup.value = true;
};

const removeProductConfirmed = async () => {
  try {
    await productService.remove(productIdToRemove.value);
    emit('remove', productIdToRemove.value); 
    showRemovePopup.value = false; 
  } catch (error) {
    console.error('Erro ao remover o produto:', error);
  } finally {
    productIdToRemove.value = null; 
  }
};

const cancelRemoveProduct = () => {
  productIdToRemove.value = null;
  showRemovePopup.value = false;
};
</script>

<template>
  <div class="table-responsive mt-4">
    <table class="table table-striped">
      <thead>
        <tr>
          <th>ID</th>
          <th>Produto</th>
          <th>Fornecedor</th>
          <th>Categoria</th>
          <th>Estoque</th>
          <th>Valor</th>
          <th>Editar</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(product, index) in sortedProducts" :key="index">
          <td>{{ product.id }}</td>
          <td>{{ product.nome }}</td>
          <td>{{ product.fornecedor }}</td>
          <td>{{ product.categoria.nome }}</td>
          <td>{{ product.quantidade }}</td>
          <td>{{ product.preco }}</td>
          <td>
            <div class="d-flex justify-content-evenly" role="group">
              <button @click="editProduct(product.id)" class="btn btn-edit edit-buttons">
                Editar <i class="fas fa-edit"></i>
              </button>
              <button
                type="button"
                class="btn btn-remove remove-buttons"
                @click="confirmRemoveProduct(product.id)"
              >
                Remover <i class="fas fa-trash"></i>
              </button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
  </div>

  <div v-if="showRemovePopup" class="popup">
    <div class="popup-content">
      <p>Tem certeza que deseja remover este produto?</p>
      <div class="popup-buttons">
        <button @click="removeProductConfirmed" class="btn btn-danger">Confirmar</button>
        <button @click="cancelRemoveProduct" class="btn btn-secondary">Cancelar</button>
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




