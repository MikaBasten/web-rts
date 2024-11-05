<template>
  <div id="app">
    <nav>
      <router-link to="/">Home</router-link> |
      <router-link v-if="!isLoggedIn" to="/login">Login</router-link>
      <router-link v-if="!isLoggedIn" to="/register">Register</router-link>
      <button v-if="isLoggedIn" @click="logout">Logout</button>
    </nav>
    <router-view />
  </div>
</template>

<script>
import { ref } from 'vue';
import { useRouter } from 'vue-router';

export default {
  setup() {
    const router = useRouter();
    const isLoggedIn = ref(!!localStorage.getItem("token"));

    const logout = () => {
      localStorage.removeItem("token");
      localStorage.removeItem("username");
      isLoggedIn.value = false;
      router.push('/');
    };

    return { isLoggedIn, logout };
  },
};
</script>

<style>
/* Basic styling */
nav {
  padding: 10px;
  background-color: #f4f4f4;
}
nav a {
  margin-right: 10px;
  text-decoration: none;
}
</style>
