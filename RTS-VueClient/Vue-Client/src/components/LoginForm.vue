<template>
    <div>
      <h2>Login</h2>
      <form @submit.prevent="login">
        <input type="text" placeholder="Username" v-model="username" />
        <input type="password" placeholder="Password" v-model="password" />
        <button type="submit">Login</button>
      </form>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        username: "",
        password: "",
      };
    },
    methods: {
      login() {
        // Implement login logic here
      },
    },
  };
  </script>
  