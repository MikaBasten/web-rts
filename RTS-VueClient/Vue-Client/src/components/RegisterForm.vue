<template>
  <form @submit.prevent="handleRegister">
    <div>
      <input type="text" v-model="username" placeholder="Username" required />
    </div>
    <div>
      <input type="password" v-model="passwordHash" placeholder="Password" required />
    </div>
    <div>
      <button type="submit">Register</button>
    </div>
    <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
    <p v-if="successMessage" class="success">{{ successMessage }}</p>
  </form>
</template>

<script>
import api from "../api";

export default {
  data() {
    return {
      username: "",
      passwordHash: "",
      errorMessage: null,
      successMessage: null,
    };
  },
  methods: {
    async handleRegister() {
      try {
        await api.register({
          username: this.username,
          passwordHash: this.passwordHash,
        });
        this.successMessage = "Registration successful! You can now log in.";
        this.errorMessage = null;
      } catch (error) {
        this.errorMessage = "Registration failed. Please try again.";
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

