<template>
  <div class="hex-grid" @click="closeHexagon">
    <Hexagon
      v-if="!isLoggedIn"
      :isSelected="selectedHexagon === 'login'"
      @select="selectHexagon('login')"
    >
      <template v-if="selectedHexagon === 'login'">
        <LoginForm />
      </template>
      <template v-else>
        Login
      </template>
    </Hexagon>

    <Hexagon
      v-if="!isLoggedIn"
      :isSelected="selectedHexagon === 'register'"
      @select="selectHexagon('register')"
    >
      <template v-if="selectedHexagon === 'register'">
        <RegisterForm />
      </template>
      <template v-else>
        Register
      </template>
    </Hexagon>

    <Hexagon v-if="isLoggedIn" @click="navigateTo('feature1')">Feature 1</Hexagon>
    <Hexagon v-if="isLoggedIn" @click="navigateTo('feature2')">Feature 2</Hexagon>
    <Hexagon v-if="isLoggedIn" @click="navigateTo('feature3')">Feature 3</Hexagon>
    <Hexagon v-if="isLoggedIn" @click="navigateTo('feature4')">Feature 4</Hexagon>
  </div>
</template>

<script>
import Hexagon from "../components/Hexagon.vue";
import LoginForm from "../components/LoginForm.vue";
import RegisterForm from "../components/RegisterForm.vue";

export default {
  components: { Hexagon, LoginForm, RegisterForm },
  data() {
    return {
      selectedHexagon: null,
    };
  },
  computed: {
    isLoggedIn() {
      return !!localStorage.getItem("token");
    },
  },
  methods: {
    selectHexagon(type) {
      this.selectedHexagon = this.selectedHexagon === type ? null : type;
    },
    closeHexagon(event) {
      if (!event.target.closest(".hexagon")) {
        this.selectedHexagon = null;
      }
    },
    navigateTo(feature) {
      this.$router.push(`/${feature}`);
    },
  },
};
</script>

<style scoped>
.hex-grid {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
  gap: 20px;
  max-width: 600px;
  margin: 0 auto;
}

/* Mobile view */
@media (max-width: 768px) {
  .hex-grid {
    flex-direction: column;
    align-items: center;
  }
}
</style>
