import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

import vuetify from './plugins/vuetify' // Import Vuetify

const app = createApp(App)

console.log('VITE_API_WORK_TIME_URL:' + import.meta.env.VITE_API_WORK_TIME_URL) // API WorkTime
console.log('VITE_API_AUDITORY_URL:' + import.meta.env.VITE_API_AUDITORY_URL) // API Auditory
console.log('VITE_USER_NAME:' + import.meta.env.VITE_USER_NAME) // UserName
console.log('VITE_FIRST_NAME:' + import.meta.env.VITE_FIRST_NAME) // FirstName
console.log('VITE_LAST_NAME:' + import.meta.env.VITE_LAST_NAME) // LastName

app.use(createPinia())
app.use(router)
app.use(vuetify)
app.mount('#app')
