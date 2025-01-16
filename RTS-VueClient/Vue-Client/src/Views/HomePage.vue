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

    <div>
      <h2>Lobby Management</h2>
      <!-- Use the CreateLobby component -->
      <LobbyCreation />
    </div>

    <LobbyList />
    <LobbyInfo />

    <Hexagon v-if="isLoggedIn" @click="navigateTo('feature1')">Feature 1</Hexagon>
    <Hexagon v-if="isLoggedIn" @click="navigateTo('feature2')">Feature 2</Hexagon>
    <Hexagon v-if="isLoggedIn" @click="navigateTo('feature3')">Feature 3</Hexagon>




    <!-- Logout Hexagon -->
    <Hexagon 
      v-if="isLoggedIn"
      :isSelected="selectedHexagon === 'Logout'"
      @select="selectHexagon('Logout')"
    >
      <template v-if="selectedHexagon === 'Logout'">
        <button class="logout-button" @click.stop="logout">Confirm Logout</button>
      </template>
      <template v-else>
        Logout
      </template>
    </Hexagon>
  </div>
</template>

<script>
import Hexagon from "../components/Hexagon.vue";
import LoginForm from "../components/LoginForm.vue";
import RegisterForm from "../components/RegisterForm.vue";
import LobbyCreation from "@/components/LobbyCreation.vue";
import LobbyList from "@/components/LobbyList.vue";
import LobbyInfo from "@/components/LobbyInfo.vue";

export default {
  components: { Hexagon, LoginForm, RegisterForm, LobbyCreation, LobbyList, LobbyInfo },
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
    logout() {
      localStorage.removeItem("token");
      this.expandedHexagons = [];
      this.inLobby = false;
      window.location.reload() 
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


.logout-button {
  background-color: #d9534f; /* Bootstrap Danger Red */
  color: white;
  border: none;
  padding: 12px 20px;
  font-size: 16px;
  font-weight: bold;
  text-transform: uppercase;
  border-radius: 8px;
  cursor: pointer;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  transition: background-color 0.3s ease, transform 0.2s ease, box-shadow 0.3s ease;
}

.logout-button:hover {
  background-color: #c9302c; /* Darker Red on Hover */
  transform: translateY(-2px);
  box-shadow: 0 6px 10px rgba(0, 0, 0, 0.15);
}

.logout-button:active {
  background-color: #a92e2a; /* Even Darker Red on Click */
  transform: translateY(0); /* Reset transform */
  box-shadow: 0 3px 5px rgba(0, 0, 0, 0.1);
}

.logout-button:focus {
  outline: 2px solid #ffbaba; /* Highlight for accessibility */
  outline-offset: 2px;
}
</style>