import axios from 'axios';

// Create an instance of axios with default configurations
const api = axios.create({
  baseURL: 'http://localhost:8080/api', // Update with your server's base URL
  headers: {
    'Content-Type': 'application/json',
  },
});

// Attach an interceptor to include the Authorization token in requests
api.interceptors.request.use(config => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
}, error => {
  return Promise.reject(error);
});

// Response interceptor to handle errors globally
api.interceptors.response.use(
  response => response,
  error => {
    // Handle common errors (e.g., 401 Unauthorized)
    if (error.response && error.response.status === 401) {
      console.error('Unauthorized! Redirecting to login...');
      // Optional: redirect to the login page or clear token
      localStorage.removeItem('token');
    }
    return Promise.reject(error);
  }
);

// Export functions for API calls
export default {
  login(credentials) {
    return api.post('/users/login', credentials);
  },
  register(userDetails) {
    return api.post('/users/register', userDetails);
  },
  
  // Lobby Management
  getLobbies() {
    return api.get('/lobbies');
  },
  getLobbyById(lobbyId) {
    return api.get(`/lobbies/${lobbyId}`);
  },
  createLobby(name, playerLimit) {
    return api.post('/lobbies/create', null, {
      params: { name, playerLimit },
    });
  },
  joinLobby(lobbyId) {
    return api.post(`/lobbies/join/${lobbyId}`);
  },
  leaveLobby(lobbyId) {
    return api.post(`/lobbies/leave/${lobbyId}`);
  },
  toggleReadyStatus(lobbyId) {
    return api.post(`/lobbies/ready/${lobbyId}`);
  },
  startGame(lobbyId) {
    return api.post(`/lobbies/start/${lobbyId}`);
  },
  // You can add other API endpoints here
};
