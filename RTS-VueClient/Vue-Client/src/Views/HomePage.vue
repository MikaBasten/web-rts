<template>
  <div class="hex-grid" @click="closeHexagon">
    <!-- Login Hexagon -->
    <Hexagon
      v-if="!isLoggedIn"
      :isDisabled="false"
      :initiallyExpanded="selectedHexagon === 'login'"
      @toggle="handleToggle('login', $event)"
    >
      <template v-if="selectedHexagon === 'login'">
        <LoginForm />
      </template>
      <template v-else>
        Login
      </template>
    </Hexagon>

    <!-- Register Hexagon -->
    <Hexagon
      v-if="!isLoggedIn"
      :isDisabled="false"
      :initiallyExpanded="selectedHexagon === 'register'"
      @toggle="handleToggle('register', $event)"
    >
      <template v-if="selectedHexagon === 'register'">
        <RegisterForm />
      </template>
      <template v-else>
        Register
      </template>
    </Hexagon>

    <!-- Lobby Creation -->
    <Hexagon
      v-if="isLoggedIn && !inLobby"
      :isDisabled="false"
      :initiallyExpanded="selectedHexagon === 'createLobby'"
      @toggle="handleToggle('createLobby', $event)"
    >
      <template v-if="selectedHexagon === 'createLobby'">
        <LobbyCreation />
      </template>
      <template v-else>
        Create Lobby
      </template>
    </Hexagon>

    <!-- Lobby Info -->
    <Hexagon
      v-if="isLoggedIn && inLobby"
      :isDisabled="false"
      :initiallyExpanded="selectedHexagon === 'lobbyInfo'"
      @toggle="handleToggle('lobbyInfo', $event)"
    >
      <template v-if="selectedHexagon === 'lobbyInfo'">
        <LobbyInfo :players="lobbyPlayers" />
      </template>
      <template v-else>
        Lobby Info
      </template>
    </Hexagon>

    <!-- Chat Room -->
    <Hexagon
      v-if="isLoggedIn && inLobby"
      :isDisabled="false"
      :initiallyExpanded="selectedHexagon === 'chatRoom'"
      @toggle="handleToggle('chatRoom', $event)"
    >
      <template v-if="selectedHexagon === 'chatRoom'">
        <ChatRoom />
      </template>
      <template v-else>
        Chat Room
      </template>
    </Hexagon>

    <!-- Logout -->
    <Hexagon
      v-if="isLoggedIn"
      :isDisabled="false"
      :initiallyExpanded="selectedHexagon === 'logout'"
      @toggle="handleToggle('logout', $event)"
    >
      <template v-if="selectedHexagon === 'logout'">
        <button @click="logout">Logout</button>
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
import LobbyCreation from "../components/LobbyCreation.vue";
import LobbyInfo from "../components/LobbyInfo.vue";
import ChatRoom from "../components/ChatRoom.vue";

export default {
  components: { Hexagon, LoginForm, RegisterForm, LobbyCreation, LobbyInfo, ChatRoom },
  data() {
    return {
      selectedHexagon: null,
      inLobby: false, // Tracks whether the user is in a lobby
      lobbyPlayers: [], // Players in the current lobby
    };
  },
  computed: {
    isLoggedIn() {
      return !!localStorage.getItem("token");
    },
  },
  methods: {
    handleToggle(hexagon, isExpanded) {
      if (isExpanded) {
        this.selectedHexagon = hexagon;
      } else if (this.selectedHexagon === hexagon) {
        this.selectedHexagon = null;
      }
    },
    closeHexagon(event) {
      if (!event.target.closest(".hexagon")) {
        this.selectedHexagon = null;
      }
    },
    logout() {
      localStorage.removeItem("token");
      this.$router.push("/"); // Navigate back to the login/register page
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
  max-width: 800px;
  margin: 0 auto;
}

@media (max-width: 768px) {
  .hex-grid {
    flex-direction: column;
    align-items: center;
  }
}
</style>
