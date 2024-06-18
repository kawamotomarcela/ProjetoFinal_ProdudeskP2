<script setup>
import { ref, computed, onMounted } from "vue";
import { useRouter } from "vue-router";
import productService from "../services/productService";
import categoryService from "../services/categoryService";
import Cardsinfo from "@/components/Cardsinfo.vue"
import ProductTable from "@/components/ProductTable.vue";
import Pagination from "@/components/Pagination.vue";
import Popup from "@/components/Popup.vue";

const popupType = ref(null);
const fileName = ref("");
const uploadSuccessProduct = ref(false);
const uploadSuccessCategory = ref(false);
const uploadSuccess = ref(false);
const newProduct = ref({
  name: "",
  supplier: "",
  stock: "",
  value: "",
  category: null,
  description: "",
  imageUrl: ""
});

const newCategory = ref({
  name: "",
  description: "",
});

const categorias = ref([]);
const products = ref([]);
const categories = ref([]);
const currentPage = ref(1);
const itemsPerPage = ref(6);

const totalPages = computed(() => Math.ceil(products.value.length / itemsPerPage.value));

const getCategorias = async () => {
  try {
    categorias.value = await categoryService.getAll();
  } catch (error) {
    console.log(error);
  }
};

const showPopup = (type) => {
  popupType.value = type;
};

const closePopup = () => {
  popupType.value = null;
  fileName.value = "";
  uploadSuccess.value = false;
  uploadSuccessProduct.value = false;
  uploadSuccessCategory.value = false;
};

const updateFileName = (event) => {
  const file = event.target.files[0];
  fileName.value = file ? file.name : "";
};

const uploadFile = () => {
  uploadSuccess.value = true;
};

const closeAddCategoryPopup = () => {
  popupType.value = null;
  resetNewCategory();
  uploadSuccessCategory.value = false;
};

const closeAddProductPopup = () => {
  popupType.value = null;
  resetNewProduct();
  uploadSuccessProduct.value = false;
};

const showAddProductPopup = () => {
  popupType.value = "addProduct";
};

const showAddCategoryPopup = () => {
  popupType.value = "addCategory";
};

const changePage = async (page) => {
  currentPage.value = page;
  products.value = await productService.getAll(page);
};

const router = useRouter();
const editProduct = (id) => {
  router.push({ name: "edit", params: { id } });
};

const resetNewProduct = () => {
  newProduct.value = {
    name: "",
    supplier: "",
    stock: "",
    value: "",
    category: null,
    description: "",
    imageUrl: ""
  };
};

const resetNewCategory = () => {
  newCategory.value = {
    name: "",
    description: "",
  };
};

const addCategory = async () => {
  try {
    await categoryService.create({
      nome: newCategory.value.name
    });
    resetNewCategory();
    uploadSuccessCategory.value = true;
    categories.value = await categoryService.getAll();
  } catch (error) {
    console.error("Erro ao cadastrar categoria:", error);
  }
};

const addProduct = async () => {
  try {
    await productService.create({
      nome: newProduct.value.name,
      fornecedor: newProduct.value.supplier,
      quantidade: Number(newProduct.value.stock),
      preco: Number(newProduct.value.value),
      categoriaId: newProduct.value.category,
      descricao: newProduct.value.description,
      imagemUrl: newProduct.value.imageUrl,
    });
    resetNewProduct();
    uploadSuccessProduct.value = true;
    products.value = await productService.getAll(currentPage.value);
  } catch (error) {
    console.error("Erro ao cadastrar produto:", error);
    console.log(error.response?.data || error.message);
  }
};

const showRemovePopup = async (productId) => {
  try {
    await productService.remove(productId);
  } catch (error) {
    if (error.response && error.response.status === 404) {
      console.warn(`Produto com ID ${productId} não encontrado.`);
    } else {
      console.error("Erro ao remover o produto:", error);
    }
  } finally {
    products.value = products.value.filter((product) => product.id !== productId);
  }
};

onMounted(async () => {
  try {
    products.value = await productService.getAll(1);
    categories.value = await categoryService.getAll();
    console.log(categories.value);
  } catch (error) {
    console.error("Erro ao buscar dados:", error);
  }
});
</script>

<template>

  <Cardsinfo />

  <div class="container mt-5">
    <h1 class="mb-4">Gestão de Produtos</h1>
    <div class="d-flex justify-content-between mb-3">
      <button class="btn btn-primary" @click="showPopup('import')">
        Importar <i class="fas fa-file-import"></i>
      </button>
      <div class="d-flex">
        <button
          class="btn btn-secondary me-2 rounded-end"
          @click="showAddCategoryPopup()"
        >
          Adicionar Categoria <i class="fas fa-plus"></i>
        </button>
        <button class="btn btn-success rounded-start" @click="showAddProductPopup()">
          Adicionar Produto <i class="fas fa-plus"></i>
        </button>
      </div>
    </div>

    <Popup :popupType="popupType"
           :fileName="fileName"
           :uploadSuccessProduct="uploadSuccessProduct"
           :uploadSuccessCategory="uploadSuccessCategory"
           :uploadSuccess="uploadSuccess"
           :newProduct="newProduct"
           :newCategory="newCategory"
           :categories="categories"
           @closePopup="closePopup"
           @closeAddCategoryPopup="closeAddCategoryPopup"
           @closeAddProductPopup="closeAddProductPopup"
           @updateFileName="updateFileName"
           @uploadFile="uploadFile"
           @addCategory="addCategory"
           @addProduct="addProduct"
    />

    <ProductTable :products="products" @edit="editProduct" @remove="showRemovePopup" />

    <Pagination
      :currentPage="currentPage"
      :totalPages="totalPages"
      @change="changePage"
    />

  </div>
</template>

<style lang="scss" src="../assets/scss/styleDash.scss" />
