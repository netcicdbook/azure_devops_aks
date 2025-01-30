<template>
  <nav>
    <h1>
      <img alt="Vue logo" class="logo" src="@/assets/logo.svg" width="25" height="25" />
      <span>{{ title }}</span>
    </h1>
    <p>
      <RouterLink to="/home_record">Registro Horario</RouterLink>
      <RouterLink to="/records">Mis Registros Horarios</RouterLink>
    </p>
  </nav>
</template>

<script setup>
defineProps({
  title: String
})
</script>

<style scoped>
nav {
  width: 100%;
  font-size: 12px;
  text-align: left;
}

nav a.router-link-exact-active {
  color: var(--color-text);
}

nav a.router-link-exact-active:hover {
  background-color: transparent;
}

nav a {
  display: inline-block;
  padding: 0 1rem;
  border-left: 1px solid var(--color-border);
}

nav a:first-of-type {
  border: 0;
}
</style>
