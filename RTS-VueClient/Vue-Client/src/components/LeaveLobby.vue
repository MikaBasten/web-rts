<template>
    <div>
      <h3>Are you sure you want to leave the lobby?</h3>
      <button @click="leaveLobby">Yes</button>
    </div>
  </template>
  
  <script>
  import api from "../api";
  
  export default {
    methods: {
      async leaveLobby() {
        try {
          await api.leaveLobby();
          alert("You left the lobby.");
          this.$emit("left");
        } catch {
          alert("Failed to leave the lobby.");
        }
      },
    },
  };
  </script>
  