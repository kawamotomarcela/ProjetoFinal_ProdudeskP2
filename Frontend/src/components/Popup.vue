<script setup>
import { ref } from "vue";
import productService from "../services/categoryService";
import categoryService from "../services/categoryService";

const props = defineProps({
  popupType: String,
  fileName: String,
  uploadSuccessProduct: Boolean,
  uploadSuccessCategory: Boolean,
  uploadSuccess: Boolean,
  newProduct: Object,
  newCategory: Object,
  categories: Array,
});

const emit = defineEmits(["closePopup", "closeAddCategoryPopup", "closeAddProductPopup", "updateFileName", "uploadFile", "addCategory", "addProduct"]);
const closePopup = () => {
  emit("closePopup");
};

const updateFileName = (event) => {
  emit("updateFileName", event);
};

const uploadFile = () => {
  emit("uploadFile");
};

const addCategory = () => {
  emit("addCategory");
};

const addProduct = () => {
  emit("addProduct");
};

</script>

<template>
  <div v-if="popupType" class="popup">
    <div class="popup-content">
      <span class="close fs-2 float-right" @click="closePopup()">&times;</span>
      <h2 v-if="popupType === 'import'">Importar produtos</h2>
      <h2 v-if="popupType === 'addCategory'">Adicionar Categoria</h2>
      <h2 v-if="popupType === 'addProduct'">Adicionar Produto</h2>

      <template v-if="popupType === 'import'">
        <p>1. Realize o upload do seu arquivo em formato XLSX</p>
        <p>2. Se você não sabe qual deve ser o padrão do arquivo, você pode comer uma batata</p>
        <div class="container-import">
          <input type="file" id="fileInput" @change="updateFileName" class="form-control" />
          <span id="fileName">{{ fileName }}</span>
        </div>
        <div class="button-container mt-3">
          <button id="uploadBtn" @click="uploadFile()" class="btn btn-primary">Enviar</button>
          <button id="cancelBtn" @click="closePopup()" class="btn btn-secondary">Cancelar</button>
        </div>
        <p id="successMessage" v-if="uploadSuccess">Enviado com sucesso!</p>
      </template>

      <template v-if="popupType === 'addCategory'">
        <form @submit.prevent="addCategory">
          <div class="mb-3">
            <label for="categoryName" class="form-label">Nome da Categoria:</label>
            <input type="text" id="categoryName" v-model="newCategory.name" class="form-control" required />
          </div>
          <div class="mb-3">
            <label for="categoryDescription" class="form-label">Descrição:</label>
            <input type="text" id="categoryDescription" v-model="newCategory.description" class="form-control"
              required />
          </div>
          <button type="submit" class="btn btn-primary">Adicionar</button>
          <p id="successMessage" class="alert alert-success mt-3" v-if="uploadSuccessCategory">Categoria cadastrada com
            sucesso!</p>

        </form>
      </template>
      <template v-if="popupType === 'addProduct'">
  <form @submit.prevent="addProduct">
    <div class="row">
      <div class="col-md-6 mb-3">
        <label for="productName" class="form-label">Nome do Produto:</label>
        <input type="text" id="productName" v-model="newProduct.name" class="form-control" required />
      </div>
      <div class="col-md-6 mb-3">
        <label for="productSupplier" class="form-label">Fornecedor:</label>
        <input type="text" id="productSupplier" v-model="newProduct.supplier" class="form-control" required />
      </div>
    </div>
    <div class="row">
      <div class="col-md-6 mb-3">
        <label for="productStock" class="form-label">Estoque:</label>
        <input type="number" id="productStock" v-model="newProduct.stock" class="form-control" required />
      </div>
      <div class="col-md-6 mb-3">
        <label for="productValue" class="form-label">Valor:</label>
        <input type="number" id="productValue" v-model="newProduct.value" class="form-control" required />
      </div>
    </div>
    <div class="mb-3">
      <label for="categoria" class="form-label">Categoria:</label>
      <select id="categoria" v-model="newProduct.category" class="form-control" required>
        <option v-for="categoria in categories" :key="categoria.id" :value="categoria.id">
          {{ categoria.nome }}
        </option>
      </select>
    </div>
    <div class="mb-3">
      <label for="productDescription" class="form-label">Descrição:</label>
      <input type="text" id="productDescription" v-model="newProduct.description" class="form-control" required />
    </div>
    <div class="mb-3">
      <label for="productImageUrl" class="form-label">URL da Imagem:</label>
      <input type="text" id="productImageUrl" v-model="newProduct.imageUrl" class="form-control" @input="previewImage" required />
    </div>
    <div class="mb-3" v-if="newProduct.imageUrl">
      <label for="imagePreview" class="form-label">Pré-visualização da Imagem:</label>
      <img :src="newProduct.imageUrl" alt="Pré-visualização da Imagem" class="img-fluid product-image" />
    </div>
    <button type="submit" class="btn btn-primary">Adicionar</button>
    <p id="successMessage" class="alert alert-success mt-3" v-if="uploadSuccessProduct">Produto cadastrado com sucesso!</p>
  </form>
</template>
    </div>
  </div>
</template>

<style scoped>
.product-image {
  padding: 10px;
  width: 100px; 
  max-width: 300px; 
  height: auto; 
  object-fit: cover; 
  border-radius: 8px; 
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); 
}
</style>
