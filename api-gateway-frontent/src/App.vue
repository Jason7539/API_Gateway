<template>
  <v-app>
    <v-app-bar :app="true" color="deep-purple accent-4" :dark="true">
      <v-btn @click="GoToHome()" class="clear">
      <v-toolbar-title>Web API Gateway</v-toolbar-title>
      </v-btn>
    </v-app-bar>

    <v-navigation-drawer :app="true" :expand-on-hover="true">
      <div v-if="!this.$store.state.LoggedIn">
        <v-list-item v-for="item in DefaultList" :key="item.title" :to="item.link">
          <v-list-item-content>
            <v-list-item :to="item.link">{{ item.title }}</v-list-item>
          </v-list-item-content>
        </v-list-item>
      </div>
      <div v-if="this.$store.state.LoggedIn">
        <v-list-item v-for="item in LoggedInList" :key="item.title" :to="item.link">
          <v-list-item-content>
            <v-list-item :to="item.link">{{ item.title }}</v-list-item>
          </v-list-item-content>  
        </v-list-item>

        <v-list-item>
          <v-list-item-content>
            <v-list-item @click="LogOut()">Log Out</v-list-item>
          </v-list-item-content> 
        </v-list-item>

      </div>
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
      DefaultList: [
        { title: "Login", link: "Login" },
        { title: "Register Team", link: "RegisterTeam" },
        { title: "About", link: "About" },
      ],
      LoggedInList: [
        { title: "Register Services", link: "RegisterService" },
        { title: "Manage Services", link: "ManageService" },
        { title: "Manage Team", link: "ManageTeam" },
        { title: "Service Discovery", link: "ServiceDiscovery" },
        { title: "About", link: "About" },
      ],
    };
  },
  methods: {
    GoToHome() {
      this.$router.push("/")
      .catch(err => err);
    },
    LogOut() {
      this.$store.dispatch("ResetState");
      this.$router.replace("/login");
    }
  },
};
</script>
