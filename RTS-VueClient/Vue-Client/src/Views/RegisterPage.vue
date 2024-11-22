<template>
    <div>
      <h1>Register</h1>
      <form @submit.prevent="register">
        <input v-model="username" placeholder="Username" required />
        <input v-model="password" type="password" placeholder="Password" required />
        <button type="submit">Register</button>
      </form>
      <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
    </div>
  </template>
  
  <script>
  import api from '../api'; // Adjust the path as necessary
  
  export default {
    data() {
      return {
        username: '',
        email: '',
        password: '',
        errorMessage: '',
      };
    },
    methods: {
      async handleRegister() {
        try {
          const response = await api.register({
            username: this.username,
            email: this.email,
            password: this.password,
          });
          // Handle success, e.g., redirect, show success message, etc.
          console.log('Registration successful:', response.data);
        } catch (error) {
          this.errorMessage = error.response?.data?.message || 'An error occurred during registration.';
          console.error('Registration error:', error);
        }
      },
    },
  };
  </script>
  
  <style scoped>
  .error {
    color: red;
    margin-top: 10px;
  }
  </style>
  