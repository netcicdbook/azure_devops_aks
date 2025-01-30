<template>
  <div class="work-registration">
    <div class="date-time">
      <p>
        <label class="date">{{ currentDate }}</label>
      </p>
      <p>
        <label class="time">{{ currentTime }}</label>
      </p>
    </div>
    <div class="buttons">
      <button class="register-button" @click="registerEntryClick" :disabled="lastMode == 'Entrada'">
        Registrar Entrada
      </button>
      <button class="close-button" @click="registerExitClick" :disabled="lastMode == 'Salida'">
        Registrar Salida
      </button>
    </div>
    <p>Último Registro: {{ lastMode }} - {{ lastRecordDate?.time }} - {{ lastRecordDate?.date }}</p>
  </div>
</template>

<script setup>
// PINIA - Gestor Estados
import { storeToRefs } from 'pinia'
import { useActivityStore } from '../stores/activityStore'
import { onMounted } from 'vue'

const activityStore = useActivityStore() // Definición del Store
const { lastRecordDate, lastMode } = storeToRefs(activityStore) // Variables de estado
const { getCurrentUserActivity, registerEntry, registerExit } = activityStore // Métodos

onMounted(() => {
  getCurrentUserActivity() // Inicializar llamada del Store
})

// getCurrentDateTime : Función que exporta fecha y hora actual.
const getCurrentDateTime = () => {
  const now = new Date()
  const dateOptions = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' }
  const timeOptions = { hour: '2-digit', minute: '2-digit' }
  const currentDate = now.toLocaleDateString('es-ES', dateOptions)
  const currentTime = now.toLocaleTimeString('es-ES', timeOptions)
  return { currentDate, currentTime }
}
// Declaración de propiedades reactivas para fecha y hora actual
const { currentDate, currentTime } = getCurrentDateTime()
// registerEntry: Función para registrar entrada del empleado
const registerEntryClick = () => {
  registerEntry()
}
// registerExit: Función para registrar salida del em.pleado
const registerExitClick = () => {
  registerExit()
}
</script>

<style scoped>
.date-time {
  margin-top: 2rem;
  text-align: center;
  box-shadow: 0px 1px 1px #888;
  padding: 2rem;
  border-radius: 15px;
  display: inline-block;
}
.work-registration {
  text-align: center;
  margin: 50px auto;
}
.date {
  font-size: 20px;
}
.time {
  font-size: 40px;
  font-weight: bold;
}
.buttons {
  margin-top: 20px;
}
button {
  margin: 0 10px;
  padding: 10px 20px;
  font-size: 16px;
  border: none;
  cursor: pointer;
  border-radius: 5px;
  transition: background-color 0.3s ease;
}
.register-button {
  background-color: green;
  color: white;
}
.close-button {
  background-color: red;
  color: white;
}
button:hover {
  filter: brightness(1.2);
}
button:disabled {
  background-color: gray;
  cursor: not-allowed;
  opacity: 0.5;
}
</style>
