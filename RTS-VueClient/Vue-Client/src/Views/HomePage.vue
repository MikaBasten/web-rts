<template>
  <div class="hex-grid" @click="closeHexagon">
    <!-- Login Hexagon -->
    <Hexagon
      v-if="!isLoggedIn"
      :isDisabled="false"
      :isSelected="expandedHexagons.includes('login')"
      @toggle="handleToggle('login', $event)"
    >
      <template v-if="expandedHexagons.includes('login')">
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
      :isSelected="expandedHexagons.includes('register')"
      @toggle="handleToggle('register', $event)"
    >
      <template v-if="expandedHexagons.includes('register')">
        <RegisterForm />
      </template>
      <template v-else>
        Register
      </template>
    </Hexagon>

    <!-- Lobby Creation Hexagon -->
    <Hexagon
      v-if="isLoggedIn && !inLobby"
      :isDisabled="false"
      :isSelected="expandedHexagons.includes('createLobby')"
      @toggle="handleToggle('createLobby', $event)"
    >
      <template v-if="expandedHexagons.includes('createLobby')">
        <LobbyCreation />
      </template>
      <template v-else>
        Create Lobby
      </template>
    </Hexagon>

    <!-- Lobby Info Hexagon -->
    <Hexagon
      v-if="isLoggedIn && inLobby"
      :isDisabled="false"
      :isSelected="expandedHexagons.includes('lobbyInfo')"
      @toggle="handleToggle('lobbyInfo', $event)"
    >
      <template v-if="expandedHexagons.includes('lobbyInfo')">
        <LobbyInfo :players="lobbyPlayers" />
      </template>
      <template v-else>
        Lobby Info
      </template>
    </Hexagon>

    <!-- Chat Room Hexagon -->
    <Hexagon
      v-if="isLoggedIn && inLobby"
      :isDisabled="false"
      :isSelected="expandedHexagons.includes('chatRoom')"
      @toggle="handleToggle('chatRoom', $event)"
    >
      <template v-if="expandedHexagons.includes('chatRoom')">
        <ChatRoom />
      </template>
      <template v-else>
        Chat Room
      </template>
    </Hexagon>

    <!-- Logout Hexagon -->
    <Hexagon
      v-if="isLoggedIn"
      :isDisabled="false"
      :isSelected="expandedHexagons.includes('logout')"
      @toggle="handleToggle('logout', $event)"
    >
      <template v-if="expandedHexagons.includes('logout')">
        <button @click.stop="logout">Logout</button>
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
      expandedHexagons: [], // Array to track which hexagons are expanded
      inLobby: false,       // Indicates if the user is in a lobby
      lobbyPlayers: [],     // List of players in the current lobby
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
        if (!this.expandedHexagons.includes(hexagon)) {
          if (!this.inLobby && ['login', 'register'].includes(hexagon)) {
            // When not in lobby, only allow one of login/register to be expanded
            this.expandedHexagons = [hexagon];
          } else {
            // When in lobby or other hexagons, allow multiple expansions
            this.expandedHexagons.push(hexagon);
          }
        }
      } else {
        // Remove the hexagon from the expanded list
        this.expandedHexagons = this.expandedHexagons.filter((h) => h !== hexagon);
      }
    },
    closeHexagon(event) {
      if (!event.target.closest(".hexagon")) {
        this.expandedHexagons = [];
      }
    },
    logout() {
      localStorage.removeItem("token");
      this.expandedHexagons = [];
      this.inLobby = false;
      this.$router.push("/"); 
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
