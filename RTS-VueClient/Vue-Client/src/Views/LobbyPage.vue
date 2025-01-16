<template>
  <div>
    <h1>Lobby: {{ lobby?.name }}</h1>
    <p>Players: {{ lobby?.currentPlayers }}/{{ lobby?.playerLimit }}</p>

    <h2>Players List</h2>
    <ul>
      <li v-for="player in lobby?.players" :key="player.username">
        {{ player.username }} - {{ player.isReady ? "Ready" : "Not Ready" }}
      </li>
    </ul>

    <button @click="toggleReadyStatus">
      {{ currentPlayer?.isReady ? "Unready" : "Ready" }}
    </button>

    <button @click="leaveLobby">Leave Lobby</button>
    <button @click="startGame" v-if="isHost">Start Game</button>

    <h2>Chat</h2>
    <div id="chatBox">
      <div v-for="message in chatMessages" :key="message.timestamp">
        <strong>{{ message.user }}:</strong> {{ message.message }}
      </div>
    </div>
    <input v-model="newMessage" placeholder="Type a message..." />
    <button @click="sendMessage">Send</button>
  </div>
</template>

<script>
import api from "../api";
import * as signalR from "@microsoft/signalr";

export default {
  data() {
    return {
      lobby: null,
      currentPlayer: null,
      isHost: false,
      newMessage: "",
      chatMessages: [],
      connection: null,
    };
  },
  props: ["id"],

  async created() {
    await this.fetchLobbyData();
    await this.setupSignalRConnection();
  },

  beforeUnmount() {
    this.closeConnection();
  },

  methods: {
    async fetchLobbyData() {
      try {
        const response = await api.getLobbyById(this.id);
        this.lobby = response.data;

        const username = localStorage.getItem("username");
        this.currentPlayer = this.lobby.players.find(
          (player) => player.username === username
        );
        this.isHost = this.currentPlayer?.role === "Host";
      } catch (error) {
        console.error("Failed to fetch lobby data:", error);
      }
    },

    async setupSignalRConnection() {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5288/chatHub", {
          accessTokenFactory: () => localStorage.getItem("token"),
        })
        .withAutomaticReconnect()
        .build();

      this.connection.on("ReceiveMessage", (user, message) => {
        this.chatMessages.push({ user, message, timestamp: Date.now() });
      });

      try {
        await this.connection.start();
        await this.connection.invoke("JoinLobby", parseInt(this.id));
      } catch (error) {
        console.error("Error connecting to chat hub:", error);
      }
    },

    async closeConnection() {
      if (this.connection) {
        await this.connection.invoke("LeaveLobby", parseInt(this.id));
        await this.connection.stop();
      }
    },

    async sendMessage() {
      if (this.newMessage.trim() === "") return;

      try {
        const username = localStorage.getItem("username");
        await this.connection.invoke(
          "SendMessageToLobby",
          parseInt(this.id),
          username,
          this.newMessage
        );
        this.newMessage = "";
      } catch (error) {
        console.error("Error sending message:", error);
      }
    },

    async toggleReadyStatus() {
      try {
        await api.toggleReadyStatus(this.id);
        await this.fetchLobbyData();
      } catch (error) {
        console.error("Error toggling ready status:", error);
      }
    },

    async leaveLobby() {
      try {
        await api.leaveLobby(this.id);
        this.$router.push("/"); // Redirect to the homepage after leaving
      } catch (error) {
        console.error("Error leaving lobby:", error);
      }
    },

    async startGame() {
      try {
        await api.startGame(this.id);
        alert("Game started!");
      } catch (error) {
        console.error("Error starting game:", error);
      }
    },
  },
};
</script>
