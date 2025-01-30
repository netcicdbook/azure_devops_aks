<template>
  <v-data-table
    :headers="headers"
    :items="records"
    @update:options="loadItems"
    :loading="loading"
  ></v-data-table>
</template>

<script setup>
import { ref } from 'vue'
// PINIA - Gestor Estados
import { storeToRefs } from 'pinia'
import { useRecordsAuditStore } from '../stores/recordsAuditStore'

// Definición del Store
const recordsAuditStore = useRecordsAuditStore()
const { records } = storeToRefs(recordsAuditStore) // Variables de estado
const { getRecords } = recordsAuditStore // Métodos

const loading = ref(true)
const headers = [
  { title: 'Usuario', align: 'start', sortable: false, key: 'userName' },
  { title: 'Nombre', key: 'firstName', align: 'end', sortable: false },
  { title: 'Apellidos', key: 'lastName', align: 'end', sortable: false },
  { title: 'Último Registro', key: 'lastRecord', align: 'end', sortable: true },
  { title: 'Modo', key: 'mode', align: 'end', sortable: false }
]

const loadItems = async () => {
  loading.value = true //La barra de carga se inicia
  await getRecords()
  loading.value = false //Cuando termina la llamada asíncrona, se detiene la barra.
}
</script>

<style scoped></style>
