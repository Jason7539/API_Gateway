<template>
  <v-app>
    <v-app-bar :app="true" color="deep-purple accent-4" :dark="true">
      <v-toolbar-title>Web API Gateway</v-toolbar-title>
    </v-app-bar>


    <v-navigation-drawer :app="true" :expand-on-hover="true">
      <v-list-item v-for="item in list" :key="item.title" :to="item.link">
        <v-list-item-content>
          <v-list-item :to="item.link">{{ item.title }}</v-list-item>
        </v-list-item-content>
      </v-list-item>
    </v-navigation-drawer>


    <v-content>
      <router-view></router-view>
    </v-content>
  </v-app>
</template>

<script>
export default {
  name: "App",

  components: {},

  data() {
    return {
      list: [
        { title: "Home", link: "/" },
        { title: "Login", link: "Login" },
        { title: "Register Team", link: "RegisterTeam" },
        { title: "Register Services", link: "RegisterService" },
        { title: "Manage Services", link: "ManageService" },
        { title: "Manage Team", link: "ManageTeam" },
        { title: "Service Discovery", link: "ServiceDiscovery" },
        { title: "About", link: "About" },
      ],
    };
  },
};
</script>
