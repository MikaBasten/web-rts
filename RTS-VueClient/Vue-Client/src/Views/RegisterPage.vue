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
  export default {
    data() {
      return {
        username: '',
        password: '',
        errorMessage: ''
      };
    },
    methods: {
      async register() {
        try {
          await api.post('/users/register', {
            Username: this.username,
            PasswordHash: this.password,
          });
          alert("Registration successful! Redirecting to login...");
          this.$router.push('/login'); // Redirect to login page on success
        } catch (error) {
          this.errorMessage = error.response?.data || "Registration failed. Try again.";
        }
      }
    }
  };
  </script>
  
  <style scoped>
  .error {
    color: red;
    margin-top: 10px;
  }
  </style>
  