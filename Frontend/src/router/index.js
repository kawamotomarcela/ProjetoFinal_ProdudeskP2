import { createRouter, createWebHistory } from 'vue-router'
import DashboardView from '@/views/DashboardView.vue'
import EditView from '@/views/EditView.vue'
import FilterView from '@/views/FilterView.vue'
import AboutView from '@/views/AboutView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: DashboardView
    },
    {
      path: '/edit',
      name: 'edit',
      component: EditView
    },
    {
        path: '/filter',
        name: 'filter',
        component: FilterView
    },
    {
      path: '/about',
      name: 'about',
      component: AboutView
  },
  ]
})

export default router
