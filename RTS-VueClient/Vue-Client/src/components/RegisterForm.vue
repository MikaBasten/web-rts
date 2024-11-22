<template>
  <div class="register-form">
    <h2>Register</h2>
    <form @submit.prevent="handleRegister">
      <div>
        <input type="text" placeholder="Username" v-model="username" id="username" required />
      </div>
      <div>
        <input type="password" placeholder="Password" v-model="password" id="password" required />
      </div>
      <div>
        <button type="submit">Register</button>
      </div>
    </form>
    <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
    <p v-if="successMessage" class="success">{{ successMessage }}</p>
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
      successMessage: null,
    };
  },
  methods: {
    async handleRegister() {
      try {
        await api.register({ username: this.username, password: this.password });
        this.successMessage = 'Registration successful! You can now log in.';
        this.errorMessage = null;
      } catch (error) {
        this.errorMessage = 'Registration failed. Please try again.';
        this.successMessage = null;
      }
    },
  },
};
</script>

<style scoped>
.error {
  color: red;
}
.success {
  color: green;
}
</style>
