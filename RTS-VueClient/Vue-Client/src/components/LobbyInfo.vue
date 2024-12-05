<template>
    <div>
      <h3>Lobby Info</h3>
      <ul>
        <li v-for="player in players" :key="player.id">
          {{ player.name }} - {{ player.ready ? "Ready" : "Not Ready" }}
        </li>
      </ul>
    </div>
  </template>
  
  <script>
  export default {
    props: {
      players: Array, // Pass players as a prop
    },
  };
  </script>
  