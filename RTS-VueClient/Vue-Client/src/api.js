import axios from 'axios';

// Create an instance of axios with default configurations
const api = axios.create({
  baseURL: 'http://localhost:5288/api', // Update with your server's base URL
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
      window.location.href = '/login'; // Adjust the path as needed
    }
    return Promise.reject(error);
  }
);

// Export functions for API calls
export default {
  login(credentials) {
    return api.post('/Users/login', credentials);
  },
  register(userDetails) {
    return api.post('/Users/register', userDetails);
  },
  // Add more API methods as needed
  getUserProfile() {
    return api.get('/user/profile');
  },
  updateProfile(profileData) {
    return api.put('/user/profile', profileData);
  },
  // You can add other API endpoints here
};
