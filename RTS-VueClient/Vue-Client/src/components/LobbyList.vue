<template>
  <div>
    <h3>Available Lobbies</h3>
    <button @click="refreshLobbies" class="refresh-button">Refresh</button>
    <ul v-if="lobbies && lobbies.length > 0">
      <li v-for="lobby in lobbies" :key="lobby.id" @click="joinLobby(lobby.id)">
        {{ lobby.name }} - {{ lobby.players.length }} Players
      </li>
    </ul>
    <p v-else>No lobbies available</p>
  </div>
</template>

<script>
import api from "../api";

export default {
  data() {
    return {
      lobbies: [], // Initialize lobbies as an empty array
    };
  },
  async mounted() {
    await this.fetchLobbies(); // Fetch lobbies when the component is mounted
  },
  methods: {
    async fetchLobbies() {
      try {
        const response = await api.getLobbies();
        console.log("API Response:", response); // Log the response for debugging
        this.lobbies = response.data || []; // Handle cases where `data` might be undefined
      } catch (error) {
        console.error("Failed to fetch lobbies:", error);
        this.lobbies = []; // Fallback to an empty array on error
      }
    },
    async joinLobby(lobbyId) {
      try {
        await api.joinLobby(lobbyId);
        alert("Joined lobby successfully!");
      } catch (error) {
        console.error("Failed to join lobby:", error);
        alert("Failed to join lobby.");
      }
    },
    async refreshLobbies() {
      await this.fetchLobbies(); // Call the fetch method to refresh the lobby list
    },
  },
};
</script>

<style scoped>
.refresh-button {
  background-color: #007bff;
  color: white;
  border: none;
  padding: 8px 16px;
  font-size: 14px;
  border-radius: 4px;
  cursor: pointer;
  margin-bottom: 10px;
  transition: background-color 0.3s;
}

.refresh-button:hover {
  background-color: #0056b3;
}

.refresh-button:active {
  background-color: #004080;
}
</style>
