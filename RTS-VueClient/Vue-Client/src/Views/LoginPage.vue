<template>
    <div>
      <h1>Login</h1>
      <form @submit.prevent="login">
        <input v-model="username" placeholder="Username" required />
        <input v-model="password" type="password" placeholder="Password" required />
        <button type="submit">Login</button>
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
      async login() {
        try {
          const response = await api.post('/users/login', {
            Username: this.username,
            PasswordHash: this.password,
          });
          localStorage.setItem('token', response.data.token); // Store the JWT token
          localStorage.setItem('username', this.username); // Store the username
          alert("Login successful! Redirecting to dashboard...");
          this.$router.push('/'); // Redirect to the homepage on success
        } catch (error) {
          this.errorMessage = error.response?.data || "Login failed. Please check your credentials.";
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
  