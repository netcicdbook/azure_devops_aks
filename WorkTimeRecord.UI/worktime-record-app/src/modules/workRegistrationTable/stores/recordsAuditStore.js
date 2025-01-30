import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useRecordsAuditStore = defineStore('recordsAudit', () => {
  const records = ref([])
  const apiUrl = import.meta.env.VITE_API_AUDITORY_URL // Accede a la URL de la API desde el .env
  const userName = import.meta.env.VITE_USER_NAME

  const mockedItems = [
    {
      userName: 'rserrano',
      firstName: 'Ramón',
      lastName: 'Serrano Valero',
      lastRecord: new Date('2024-05-01T08:00:00').toLocaleString(),
      mode: 'Entrada'
    }
  ]
  /*const FakeAPI = {
    async fetch() {
      return new Promise((resolve) => {
        setTimeout(() => {
          resolve({ items: mockedItems })
        }, 1500)
      })
    }
  }*/
  /*async function getRecords() {
    const result = await FakeAPI.fetch()
    records.value = result.items
  }*/
  const API = {
    async fetch() {
      try {
        const response = await fetch(`${apiUrl}/UserRecordsHistory/${userName}`)
        if (!response.ok) throw new Error('Error al obtener el histórico de registros del empleado')
        const result = await response.json()
        return { items: result }
      } catch (error) {
        console.error('Error al obtener el histórico de registros del empleado:', error)
        return { recordDate: null, lastMode: null }
      }
    }
  }
  async function getRecords() {
    const result = await API.fetch()
    records.value = result.items.map((record) => ({
      ...record,
      lastRecord: new Date(record.lastRecord).toLocaleString()
    }))
  }

  return {
    //State
    records,
    //Actions
    getRecords
  }
})
