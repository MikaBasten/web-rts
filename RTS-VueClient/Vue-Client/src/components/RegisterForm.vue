<template>
    <div>
      <h2>Register</h2>
      <form @submit.prevent="register">
        <input type="text" placeholder="Username" v-model="username" />
        <input type="password" placeholder="Password" v-model="password" />
        <button type="submit">Register</button>
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
      register() {
        // Implement register logic here
      },
    },
  };
  </script>
  