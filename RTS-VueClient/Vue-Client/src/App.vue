<template>
  <div id="app">
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
