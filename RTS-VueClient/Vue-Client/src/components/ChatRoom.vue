<template>
    <div>
      <h3>Chat</h3>
      <div class="chat-box">
        <div v-for="message in messages" :key="message.id">
          <strong>{{ message.user }}:</strong> {{ message.text }}
        </div>
      </div>
      <form @submit.prevent="sendMessage">
        <input type="text" v-model="newMessage" placeholder="Type a message..." />
        <button type="submit">Send</button>
      </form>
    </div>
  </template>
  
  <script>
  import * as signalR from "@microsoft/signalr";
  
  export default {
    data() {
      return {
        messages: [],
        newMessage: "",
        connection: null,
      };
    },
    async mounted() {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub") // Replace with your backend's SignalR endpoint
        .build();
  
      this.connection.on("ReceiveMessage", (user, message) => {
        this.messages.push({ user, text: message });
      });
  
      await this.connection.start();
    },
    methods: {
      async sendMessage() {
        if (this.newMessage.trim() !== "") {
          await this.connection.invoke("SendMessage", "Me", this.newMessage);
          this.newMessage = "";
        }
      },
    },
  };
  </script>
  