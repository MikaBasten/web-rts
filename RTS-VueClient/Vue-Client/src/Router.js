import { createRouter, createWebHistory } from 'vue-router';
import HomePage from './Views/HomePage.vue';
import LobbyPage from './Views/LobbyPage.vue';


const routes = [
  { path: '/', component: HomePage },
  { path: '/lobby/:id', component: LobbyPage, props: true },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
