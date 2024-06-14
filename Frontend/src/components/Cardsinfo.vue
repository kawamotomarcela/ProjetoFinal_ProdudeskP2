<script setup>
import { ref, onMounted, computed } from 'vue';
import productService from '../services/productService';

const allProducts = ref([]);

const loadAllProducts = async () => {
  try {
    allProducts.value = [];
    let page = 1;
    let products = [];
    
    do {
      products = await productService.getAll(page);
      allProducts.value.push(...products);
      page++;
    } while (products.length > 0); 
    
  } catch (error) {
    console.error('Erro ao buscar todos os produtos:', error);
  }
};

onMounted(loadAllProducts);

const totalUniqueProductIds = computed(() => {
  const uniqueIds = new Set();
  allProducts.value.forEach(product => {
    uniqueIds.add(product.id);
  });
  return uniqueIds.size;
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
        <h4 class="mb-2">Produtos Cadastrados</h4>
        <div class="product-count">
          <h2 class="m-2 d-flex align-items-center justify-content-center">
            {{ totalUniqueProductIds }}
            <box-icon name="box" size="lg" class="box-icon m-1"></box-icon>
          </h2>
        </div>
      </div>
    </div>
  </div>
</template>





