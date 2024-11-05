import { createRouter, createWebHistory } from 'vue-router';
import HomePage from './views/HomePage.vue';
import LoginPage from './views/LoginPage.vue';
import RegisterPage from './views/RegisterPage.vue';
import LobbyPage from './views/LobbyPage.vue';

const routes = [
  { path: '/', component: HomePage },
  { path: '/login', component: LoginPage },
  { path: '/register', component: RegisterPage },
  { path: '/lobby/:id', component: LobbyPage, props: true },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
