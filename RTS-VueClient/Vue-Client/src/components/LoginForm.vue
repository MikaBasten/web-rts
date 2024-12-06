<template>
  <form @submit.prevent="handleLogin">
    <div>
      <input type="text" v-model="username" placeholder="Username" required />
    </div>
    <div>
      <input type="password" v-model="password" placeholder="Password" required />
    </div>
    <div>
      <button type="submit">Login</button>
    </div>
    <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
  </form>
</template>

<script>
import api from "../api";

export default {
  data() {
    return {
      username: "",
      password: "",
      errorMessage: null,
    };
  },
  methods: {
    async handleLogin() {
      try {
        const response = await api.login({
          Username: this.username,
          PasswordHash: this.password,
        });
        localStorage.setItem("token", response.data.token);
        window.location.reload()
      } catch (error) {
        this.errorMessage = "Login failed. Please check your credentials.";
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