import { createRouter, createWebHistory } from 'vue-router'
import DashboardView from '@/views/DashboardView.vue'
import EditView from '@/views/EditView.vue'
import FilterView from '@/views/FilterView.vue'
import CategorysView from '@/views/CategorysView.vue' 
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
      path: '/edit/:id',
      name: 'editid',
      component: EditView
    },
    {
        path: '/filter',
        name: 'filter',
        component: FilterView
    },
    {
      path: '/categorys',
      name: 'categorys',
      component: CategorysView
  },
    {
      path: '/about',
      name: 'about',
      component: AboutView
  },
  ]
})

export default router
