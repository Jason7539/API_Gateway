import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/about",
    name: "About",
    component: () => import("@/views/About.vue"),
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/views/Login.vue'),
  },
  {
    path: '/registerTeam',
    name: 'RegisterTeam',
    component: () => import('@/views/RegisterTeam.vue'),
  },
  {
    path: '/registerService',
    name: 'RegisterService',
    component: () => import('@/views/RegisterService.vue'),
  },
  {
    path: '/manageService',
    name: 'ManageService',
    component: () => import('@/views/ManageService.vue'),
  },
  {
    path: '/manageTeam',
    name: 'ManageTeam',
    component: () => import('@/views/ManageTeam.vue'),
  },
  {
    path: '/serviceDiscovery',
    name: 'ServiceDiscovery',
    component: () => import('@/views/ServiceDiscovery.vue'),
  },

];

const router = new VueRouter({
  routes,
});

export default router;
