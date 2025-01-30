<script setup>
import { RouterView } from 'vue-router'
import NavigationBar from '@/components/NavigationBar.vue'
</script>

<template>
  <header>
    <NavigationBar title="App Registro Horario"></NavigationBar>
  </header>

  <RouterView />
</template>

<style scoped>
header {
  line-height: 1.5;
  max-height: 100vh;
}
</style>
