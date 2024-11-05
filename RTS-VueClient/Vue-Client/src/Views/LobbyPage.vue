<template>
    <div>
      <h1>Lobby: {{ lobby.name }}</h1>
      <button @click="leaveLobby">Leave Lobby</button>
  
      <h2>Players</h2>
      <ul>
        <li v-for="player in lobby.players" :key="player.username">
          {{ player.username }} - {{ player.isReady ? 'Ready' : 'Not Ready' }}
        </li>
      </ul>
  
      <button @click="toggleReadyStatus">
        {{ currentPlayer?.isReady ? 'Unready' : 'Ready' }}
      </button>
  
      <h2>Chat</h2>
      <div id="chatBox">
        <div v-for="message in messages" :key="message.id">
          <strong>{{ message.user }}:</strong> {{ message.text }}
        </div>
      </div>
      <input v-model="chatMessage" placeholder="Type a message" @keyup.enter="sendMessage" />
      <button @click="sendMessage">Send</button>
    </div>
  </template>
  
  <script>
  import * as signalR from '@microsoft/signalr';
  
  export default {
    data() {
      return {
        lobby: {},
        messages: [],
        chatMessage: '',
        connection: null,
      };
    },
    computed: {
      currentPlayer() {
        return this.lobby.players.find(player => player.username === localStorage.getItem('username'));
      }
    },
    async created() {
      await this.fetchLobbyDetails();
      this.connectToChat();
    },
    beforeDestroy() {
      this.disconnectFromChat();
    },
    methods: {
      async fetchLobbyDetails() {
        const lobbyId = this.$route.params.id;
        try {
          const response = await api.get(`/lobbies/${lobbyId}`);
          this.lobby = response.data;
        } catch (error) {
          alert('Error fetching lobby details: ' + error.response?.data || 'Unknown error');
        }
      },
      async toggleReadyStatus() {
        const lobbyId = this.$route.params.id;
        try {
          await api.post(`/lobbies/ready/${lobbyId}`);
          await this.fetchLobbyDetails();
        } catch (error) {
          alert('Error toggling ready status: ' + error.response?.data || 'Unknown error');
        }
      },
      async leaveLobby() {
        const lobbyId = this.$route.params.id;
        try {
          await api.post(`/lobbies/leave/${lobbyId}`);
          alert("Successfully left the lobby!");
          this.$router.push('/');
        } catch (error) {
          alert('Error leaving lobby: ' + error.response?.data || 'Unknown error');
        }
      },
      connectToChat() {
        const lobbyId = this.$route.params.id;
        this.connection = new signalR.HubConnectionBuilder()
          .withUrl('http://localhost:5288/chatHub', {
            accessTokenFactory: () => localStorage.getItem('token')
          })
          .build();
  
        this.connection.start()
          .then(() => {
            console.log("Connected to chat");
            this.connection.invoke("JoinLobby", lobbyId);
            this.connection.on("ReceiveMessage", (user, message) => {
              this.messages.push({ user, text: message });
            });
          })
          .catch(error => console.error("Error connecting to chat hub:", error));
      },
      disconnectFromChat() {
        if (this.connection) {
          this.connection.stop();
        }
      },
      sendMessage() {
        const lobbyId = this.$route.params.id;
        const username = localStorage.getItem("username");
        if (this.chatMessage.trim()) {
          this.connection.invoke("SendMessageToLobby", lobbyId, username, this.chatMessage)
            .then(() => { this.chatMessage = ''; })
            .catch(error => console.error("Error sending message:", error));
        }
      }
    }
  };
  </script>
  
  <style scoped>
  /* Add your lobby-specific styles here */
  </style>
  