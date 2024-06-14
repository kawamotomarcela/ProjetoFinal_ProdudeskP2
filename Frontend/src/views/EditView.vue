<script setup>
import { ref, onMounted } from 'vue';
import productService from '@/services/productService';
import categoryService from '@/services/categoryService';
import { useRouter, useRoute } from 'vue-router';

const produto = ref({
  id: '',
  nome: '',
  fornecedor: '',
  preco: '',
  quantidade: '',
  categoria: {
    nome: ''
  },
  descricao: '',
  imagemUrl: ''  // Novo campo para URL da imagem
});
const erroIdInvalido = ref(false);
const produtoValido = ref(false);
const router = useRouter();
const route = useRoute();
const categorias = ref([]);
const salvo = ref(false);
const erroSalvar = ref(false);

const getCategorias = async () => {
  try {
    categorias.value = await categoryService.getAll();
  } catch (error) {
    console.error('Erro ao carregar categorias:', error);
  }
};

const verificarProduto = async () => {
  try {
    const produtoId = produto.value.id.toString();
    const response = await productService.get(produtoId);
    produto.value = response.data;
    produtoValido.value = true;
    erroIdInvalido.value = false;
  } catch (error) {
    console.error('Erro ao verificar o produto:', error);
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
      categoriaId: categorias.value.find(cat => cat.nome === produto.value.categoria.nome)?.id,
      imagemUrl: produto.value.imagemUrl  
    };

    const response = await productService.update(produtoId, post);

    if (response.status === 204 || response.data) {
      salvo.value = true;
      erroSalvar.value = false;
      console.log('Produto salvo com sucesso.');
    } else {
      salvo.value = false;
      erroSalvar.value = true;
      console.error('Produto não encontrado para salvar.');
    }
  } catch (error) {
    salvo.value = false;
    erroSalvar.value = true;
    console.error('Erro ao salvar o produto:', error);
  }
};

const resetForm = () => {
  produto.value = {
    id: '',
    nome: '',
    fornecedor: '',
    preco: '',
    quantidade: '',
    categoria: {
      nome: ''
    },
    descricao: '',
    imagemUrl: ''  
  };
  produtoValido.value = false;
  erroIdInvalido.value = false;
  salvo.value = false; 
  erroSalvar.value = false; 
};

onMounted(() => {
  const productId = route.params.id;
  if (productId) {
    produto.value.id = productId;
    verificarProduto();
  }
  getCategorias();
});
</script>

<template>
  <div class="editar-curso m-4 position-relative">
    <h1><i class="fas fa-edit"></i> Editar Produtos</h1>
    <form @submit.prevent="salvarProduto">
      <div class="formulario">
        <div class="form-group">
          <label for="id-produto">ID do Produto: {{ produto.id }}</label>
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
          <button
            type="button"
            class="btn btn-close position-absolute top-0 end-0"
            @click="resetForm"
            aria-label="Close"
          ></button>
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
    
          <div class="form-group">
            <label for="imagem-url">URL da Imagem:</label>
            <input
              type="text"
              id="imagem-url"
              v-model="produto.imagemUrl"
              class="form-control"
            />
          </div>
       
          <div class="form-group text-center" v-if="produto.imagemUrl">
            <label>Pré-visualização da Imagem:</label>
            <img :src="produto.imagemUrl" alt="Imagem do Produto" class="img-thumbnail img-fluid mx-auto d-block" style="max-width: 300px;" />
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

<style scoped>
.editar-curso .formulario {
  background-color: #f8f9fa;
  padding: 20px;
  border-radius: 5px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}
</style>


