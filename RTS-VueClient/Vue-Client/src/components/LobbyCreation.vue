<template>
  <div>
    <h3>Create a Lobby</h3>
    <form @submit.prevent="createLobby">
      <input 
        type="text" 
        v-model="lobbyName" 
        placeholder="Lobby Name" 
        required 
      />
      <input 
        type="number" 
        v-model="playerLimit" 
        placeholder="Player Limit" 
        required 
        min="1"
      />
      <button type="submit">Create</button>
    </form>
    <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
  </div>
</template>

<script>
import api from "../api"; // Adjust the import path as needed

export default {
  data() {
    return {
      lobbyName: "",
      playerLimit: 2,  // Default value for player limit
      errorMessage: null,
    };
  },
  methods: {
    async createLobby() {
      try {
        // Call the createLobby API with the name and playerLimit as parameters
        await api.createLobby(this.lobbyName, this.playerLimit);
        alert("Lobby created successfully!");
        this.lobbyName = ""; // Reset the form
        this.playerLimit = 2; // Reset to default player limit
      } catch (error) {
        this.errorMessage = "Failed to create lobby. Try again.";
      }
    },
  },
};
</script>

<style scoped>
/* You can add your styles here */
.error {
  color: red;
  font-size: 14px;
}
</style>
