<template>
  <div class="login-form">
    <h2>Login</h2>
    <form @submit.prevent="handleLogin">
      <div>
        <input type="username" placeholder="Username" v-model="username" id="username" required />
      </div>
      <div>
        <input type="password" placeholder="Password" v-model="password" id="password" required />
      </div>
      <button type="submit">Login</button>
    </form>
    <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
  </div>
</template>

<script>
import api from '../api';

export default {
  data() {
    return {
      username: '',
      password: '',
      errorMessage: null,
    };
  },
  methods: {
    async handleLogin() {
      try {
        const response = await api.login({ username: this.username, password: this.password });
        const token = response.data.token;
        localStorage.setItem('token', token);
        this.$router.push('/home'); // Navigate to home after successful login
      } catch (error) {
        this.errorMessage = 'Login failed. Please check your credentials.';
      }
    },
  },
};
</script>

<style scoped>
.error {
  color: red;
}
</style>
