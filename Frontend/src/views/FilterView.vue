<script setup>
import productService from "../services/productService";
import { ref, onMounted } from "vue";
import categoryService from "@/services/categoryService.js";

const searchQuery = ref("");
const priceMin = ref("");
const priceMax = ref("");
const sortBy = ref("preco-crescente");
const category = ref("");
const itemsPerPage = ref(10);

const products = ref([]);

const filteredProducts = ref([]);

const categorias = ref([]);

const getCategorias = async () => {
  try {
    categorias.value = await categoryService.getAll();
  } catch (error) {
    console.log(error);
  }
};

const applyFilter = () => {
  filteredProducts.value = products.value
    .filter((product) => {
      let matchesSearch = true;

      if (searchQuery.value) {
        const query = searchQuery.value.toLowerCase();
        matchesSearch =
          product.nome.toLowerCase().includes(query) || product.id.toString() === query;
      }

      return (
        matchesSearch &&
        (!priceMin.value || product.preco >= parseFloat(priceMin.value)) &&
        (!priceMax.value || product.preco <= parseFloat(priceMax.value)) &&
        (!category.value || product.categoria.nome === category.value)
      );
    })
    .sort((a, b) => {
      if (sortBy.value === "preco-crescente") {
        return a.preco - b.preco;
      } else {
        return b.preco - a.preco;
      }
    })
    .slice(0, itemsPerPage.value);
};

filteredProducts.value = products.value;

onMounted(async () => {
  try {
    products.value = await productService.getAll(1, 99);
    getCategorias();
  } catch (error) {
    console.error("Erro ao buscar dados:", error);
  }
});
</script>

<template>
  <div class="container my-4">
    <div class="filtro p-3 mb-3 bg-white rounded shadow-sm">
      <h2>Filtrar Produtos</h2>
      <form @submit.prevent="applyFilter">
        <div class="form-group mb-3">
          <input
            type="text"
            v-model="searchQuery"
            class="form-control"
            placeholder="Nome ou ID do produto"
          />
        </div>
        <div class="form-group mb-3">
          <label for="price">Faixa de Preço:</label>
          <div class="d-flex">
            <input
              type="number"
              v-model.number="priceMin"
              class="form-control mr-2"
              placeholder="Mínimo"
              min="0"
            />
            <input
              type="number"
              v-model.number="priceMax"
              class="form-control"
              placeholder="Máximo"
              min="0"
            />
          </div>
        </div>
        <div class="form-group mb-3">
          <label for="sort-by">Ordenar por Preço e Categoria:</label>
          <div class="d-flex">
            <select v-model="sortBy" class="form-control mr-2">
              <option value="preco-crescente">Preço: Crescente</option>
              <option value="preco-decrescente">Preço: Decrescente</option>
            </select>
            <select v-model="category" class="form-control">
              <option value="">Todas as Categorias</option>
              <option
                v-for="categoria in categorias"
                :key="categoria.id"
                :value="categoria.nome"
              >
                {{ categoria.nome }}
              </option>
            </select>
          </div>
        </div>
        <div class="form-group mb-3">
          <label for="items-per-page">Itens por Página:</label>
          <div class="d-flex">
            <input
              type="number"
              v-model.number="itemsPerPage"
              min="1"
              class="form-control mr-2"
            />
            <button type="submit" class="btn btn-primary">Pesquisar</button>
          </div>
        </div>
      </form>
    </div>
    <div class="container-produto row">
      <div
        v-for="product in filteredProducts"
        :key="product.id"
        class="produto col-md-3 mb-4"
      >
        <div class="card">
          <img src="@/assets/img/iphome.jpg" class="card-img-top" :alt="product.nome" />
          <div class="card-body">
            <h5 class="card-title">{{ product.nome }}</h5>
            <p class="card-text">Categoria: {{ product.categoria.nome }}</p>
            <p class="card-text">Quantidade: {{ product.quantidade }}</p>
            <p class="card-text">Preço: R$ {{ product.preco.toFixed(2) }}</p>
            <p class="card-text">Marca: {{ product.fornecedor }}</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style>
.produto .card-img-top {
  max-width: 80%;
  margin: 0 auto;
  display: block;
}
</style>
