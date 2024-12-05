<template>
  <div>
    <h3>Available Lobbies</h3>
    <ul>
      <li v-for="lobby in lobbies" :key="lobby.id" @click="joinLobby(lobby.id)">
        {{ lobby.name }} - {{ lobby.players.length }} Players
      </li>
    </ul>
  </div>
</template>

<script>
import api from "../api";

export default {
  data() {
    return {
      lobbies: [],
    };
  },
  async mounted() {
    this.lobbies = await api.getLobbies();
  },
  methods: {
    async joinLobby(lobbyId) {
      try {
        await api.joinLobby(lobbyId);
        alert("Joined lobby successfully!");
      } catch {
        alert("Failed to join lobby.");
      }
    },
  },
};
</script>
