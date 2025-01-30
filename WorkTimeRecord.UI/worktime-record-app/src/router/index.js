import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/home_record',
      name: 'home_record',
      component: () => import('@/modules/workRegistration/views/WorkRegistrationView.vue')
    },
    {
      path: '/records',
      name: 'records',
      component: () => import('@/modules/workRegistrationTable/views/WorkRegistrationTable.vue')
    },
    { path: '/:pathMatch(.*)*', redirect: '/home_record' }
  ]
})

export default router
