<template>
    <div>
      <h3>Create a Lobby</h3>
      <form @submit.prevent="createLobby">
        <input type="text" v-model="lobbyName" placeholder="Lobby Name" required />
        <button type="submit">Create</button>
      </form>
      <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
    </div>
  </template>
  
  <script>
  import api from "../api";
  
  export default {
    data() {
      return {
        lobbyName: "",
        errorMessage: null,
      };
    },
    methods: {
      async createLobby() {
        try {
          await api.createLobby({ name: this.lobbyName });
          alert("Lobby created successfully!");
        } catch (error) {
          this.errorMessage = "Failed to create lobby. Try again.";
        }
      },
    },
  };
  </script>
  