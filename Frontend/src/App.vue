<script setup>
import Navbar from './components/Navbar.vue';
import Sidebar from './components/Sidebar.vue';

</script>

<template>
  <div class="wrapper d-flex">
    <Sidebar />

    <div class="content w-100">
      <Navbar />
      <main>
        <RouterView />
      </main>
    </div>
  </div>
</template>

<style>
.content {
  margin-left: 280px;
}
::-webkit-scrollbar {
  width: 8px; 
}


::-webkit-scrollbar-track {
  background: transparent; 
}

::-webkit-scrollbar-thumb {
  background-color: rgba(0, 0, 0, 0.2); 
  border-radius: 10px; 
}

::-webkit-scrollbar-thumb:hover {
  background-color: rgba(0, 0, 0, 0.3);
}

@media (max-width: 768px) {
  .content {
    margin-left: 0;
  }
}
</style>