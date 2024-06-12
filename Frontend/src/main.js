import { createApp } from 'vue';
import App from './App.vue';
import router from './router';

import 'boxicons';
import 'bootstrap';
import '@/assets/scss/main.scss';

const app = createApp(App);

app.use(router);

app.mount('#app');
