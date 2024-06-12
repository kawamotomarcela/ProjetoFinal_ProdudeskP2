<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";
import productService from "@/services/productService.js";
import categoryService from "@/services/categoryService.js";

const produto = ref({
  id: "",
  nome: "",
  fornecedor: "",
  preco: "",
  quantidade: "",
  categoria: {},
  descricao: "",
});
const salvo = ref(false);
const erroSalvar = ref(false);
const produtoValido = ref(false);
const erroIdInvalido = ref(false);
const categorias = ref([]);

const getCategorias = async () => {
  try {
    categorias.value = await categoryService.getAll();
  } catch (error) {
    console.log(error);
  }
};

onMounted(() => {
  getCategorias();
});

const verificarProduto = async () => {
  try {
    const produtoId = produto.value.id.toString();
    let response;

    response = await productService.get(produtoId);

    produto.value = response.data;
    produtoValido.value = true;
    erroIdInvalido.value = false;
  } catch (error) {
    console.error("Erro ao verificar o produto:", error);
    produtoValido.value = false;
    erroIdInvalido.value = true;
  }
};

const salvarProduto = async () => {
  try {
    const produtoId = produto.value.id.toString();

    const post = {
      nome: produto.value.nome,
      descricao: produto.value.descricao,
      fornecedor: produto.value.fornecedor,
      quantidade: produto.value.quantidade,
      preco: produto.value.preco,
      categoriaId: produto.value.categoria.id,
    };
    console.log(post);

    const response = await productService.update(produtoId, post);

    if (response.status === 204 || response.data) {
      salvo.value = true;
      erroSalvar.value = false;
      console.log("Produto salvo com sucesso.");
    } else {
      salvo.value = false;
      erroSalvar.value = true;
      console.error("Produto não encontrado para salvar.");
    }
  } catch (error) {
    salvo.value = false;
    erroSalvar.value = true;
    console.error("Erro ao salvar o produto:", error);
  }
};
</script>

<template>
  <div class="editar-curso m-4">
    <h1><i class="fas fa-edit"></i> Editar Produto Produdesk</h1>
    <form @submit.prevent="salvarProduto">
      <div class="formulario">
        <div class="form-group">
          <label for="id-produto">ID do Produto:</label>
          <div class="input-group">
            <input
              type="number"
              id="id-produto"
              v-model.number="produto.id"
              class="form-control"
              min="0"
              required
            />
            <button
              type="button"
              class="btn btn-outline-primary"
              @click="verificarProduto"
            >
              Verificar
            </button>
          </div>
          <span v-if="erroIdInvalido" class="text-danger"
            >ID do produto inválido. Insira o ID correto e verifique novamente.</span
          >
        </div>
        <div v-if="produtoValido && !erroIdInvalido">
          <div class="form-group">
            <label for="nome-produto">Nome do Produto:</label>
            <input
              type="text"
              id="nome-produto"
              v-model="produto.nome"
              class="form-control"
              required
            />
          </div>
          <div class="form-group">
            <label for="fornecedor">Fornecedor:</label>
            <input
              type="text"
              id="fornecedor"
              v-model="produto.fornecedor"
              class="form-control"
              required
            />
          </div>
          <div class="form-group">
            <label for="categoria">Categoria:</label>
            <select
              id="categoria"
              v-model="produto.categoria.nome"
              class="form-control"
              required
            >
              <option
                v-for="categoria in categorias"
                :key="categoria.id"
                :value="categoria.nome"
              >
                {{ categoria.nome }}
              </option>
            </select>
          </div>
          <div class="form-group">
            <label for="valor-produto">Valor do Produto:</label>
            <input
              type="number"
              id="valor-produto"
              v-model.number="produto.preco"
              class="form-control"
              min="0"
              step="0.01"
              required
            />
          </div>
          <div class="form-group">
            <label for="estoque">Estoque:</label>
            <input
              type="number"
              id="estoque"
              v-model.number="produto.quantidade"
              class="form-control"
              min="0"
              required
            />
          </div>
          <div class="form-group">
            <label for="descricao">Descrição:</label>
            <textarea
              id="descricao"
              v-model="produto.descricao"
              rows="4"
              class="form-control"
              required
            ></textarea>
          </div>
          <button type="submit" class="btn btn-primary mt-4">Salvar Alterações</button>
          <div v-if="salvo" id="mensagem-salvo" class="alert alert-success mt-3">
            Salvo com sucesso
          </div>
          <div v-if="erroSalvar" id="mensagem-erro" class="alert alert-danger mt-3">
            Erro ao salvar o produto. Tente novamente.
          </div>
        </div>
      </div>
    </form>
  </div>
</template>


<style></style>
