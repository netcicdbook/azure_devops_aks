import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useActivityStore = defineStore('activity', () => {
  const lastRecordDate = ref()
  const lastMode = ref()

  const apiUrl = import.meta.env.VITE_API_WORK_TIME_URL // Accede a la URL de la API desde el .env
  const userName = import.meta.env.VITE_USER_NAME
  const firstName = import.meta.env.VITE_FIRST_NAME
  const lastName = import.meta.env.VITE_LAST_NAME

  const formatDateTime = (datetime) => {
    const dateOptions = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' }
    const timeOptions = { hour: '2-digit', minute: '2-digit' }
    const localeDate = datetime.toLocaleDateString('es-ES', dateOptions)
    const localeTime = datetime.toLocaleTimeString('es-ES', timeOptions)
    return { date: localeDate, time: localeTime }
  }
  /*const FakeAPI = {
    async fetchGetLastActivity() {
      return new Promise((resolve) => {
        setTimeout(() => {
          resolve({ recordDate: new Date(), lastMode: 'Entrada' })
        }, 500)
      })
    },
    async fetchRegisterEntry() {
      return new Promise((resolve) => {
        setTimeout(() => {
          resolve({ recordDate: new Date(), lastMode: 'Entrada' })
        }, 500)
      })
    },
    async fetchRegisterExit() {
      return new Promise((resolve) => {
        setTimeout(() => {
          resolve({ recordDate: new Date(), lastMode: 'Salida' })
        }, 500)
      })
    }
  }*/
  /*function getCurrentUserActivity() {
    FakeAPI.fetchGetLastActivity().then((result) => {
      lastMode.value = result.lastMode
      lastRecordDate.value = formatDateTime(result.recordDate)
    })
  }
  function registerEntry() {
    FakeAPI.fetchRegisterEntry().then((result) => {
      lastMode.value = result.lastMode
      lastRecordDate.value = formatDateTime(result.recordDate)
    })
  }
  function registerExit() {
    FakeAPI.fetchRegisterExit().then((result) => {
      lastMode.value = result.lastMode
      lastRecordDate.value = formatDateTime(result.recordDate)
    })
  }
  */
  const API = {
    async fetchGetLastActivity() {
      try {
        const response = await fetch(`${apiUrl}/UserWorkTimeRecord/${userName}`)
        if (!response.ok) throw new Error('Error al obtener último registro')
        const result = await response.json()
        return { recordDate: new Date(result.lastRecord), lastMode: result.mode }
      } catch (error) {
        console.error('Error al obtener último registro:', error)
        return { recordDate: null, lastMode: null }
      }
    },
    async fetchRegisterLastActivity(payload) {
      const response = await fetch(`${apiUrl}/UserWorkTimeRecord`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
      })

      if (!response.ok) throw new Error(`POST Error: ${response.statusText}`)

      return { recordDate: new Date(payload.lastRecord), lastMode: payload.mode }
    }
  }

  function getCurrentUserActivity() {
    API.fetchGetLastActivity().then((result) => {
      lastMode.value = result.lastMode
      lastRecordDate.value = formatDateTime(result.recordDate)
    })
  }
  function register(mode) {
    var payload = {
      userName: userName,
      firstName: firstName,
      lastName: lastName,
      lastRecord: new Date(),
      mode: mode
    }
    API.fetchRegisterLastActivity(payload).then(() => {
      lastMode.value = payload.mode
      lastRecordDate.value = formatDateTime(payload.lastRecord)
    })
  }
  function registerEntry() {
    register('Entrada')
  }
  function registerExit() {
    register('Salida')
  }

  return {
    //State
    lastRecordDate,
    lastMode,

    //Actions
    getCurrentUserActivity,
    registerEntry,
    registerExit
  }
})
