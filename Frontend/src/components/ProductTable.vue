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
          <tr v-for="(product, index) in products" :key="index">
            <td>{{ product.id }}</td>
            <td>{{ product.nome }}</td>
            <td>{{ product.fornecedor }}</td>
            <td>{{ product.categoria.nome }}</td>
            <td>{{ product.quantidade }}</td>
            <td>{{ product.preco }}</td>
            <td>
              <div class="d-flex justify-content-evenly" role="group">
                <router-link to="/Edit" class="btn btn-edit edit-buttons">
                  Editar <i class="fas fa-edit"></i>
                </router-link>
                <button
                  type="button"
                  class="btn btn-remove remove-buttons"
                  @click="showRemovePopup(product.id)"
                >
                  Remover <i class="fas fa-trash"></i>
                </button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
</template>

<script>
export default {
  props: {
    products: {
      type: Array,
      required: true
    }
  },
  methods: {
    showRemovePopup(productId) {
      this.$emit('remove', productId);
    }
  }
};
</script>



