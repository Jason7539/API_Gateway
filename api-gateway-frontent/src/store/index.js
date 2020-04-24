import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    Username: "",
    AccessToken: "",
    LoggedIn: false,
  },
  mutations: {
    UpdateUsername(state, newUsername) {
      state.Username = newUsername;
    },
    UpdateAccessToken(state, newAccessToken) {
      state.AccessToken = newAccessToken;
    },
    UpdateLoggedIn(state, newStatus) {
      state.LoggedIn = newStatus;
    },
  },
  actions: {
    UpdateUsername({ commit }, newUsername) {
      commit("UpdateUsername", newUsername);
    },
    UpdateAccessToken({ commit }, newAccessToken) {
      commit("UpdateAccessToken", newAccessToken);
    },
    UpdateLoggedIn({ commit }, newStatus) {
      commit("UpdateLoggedIn", newStatus);
    },
  },
  modules: {},
  getters: {
    Username: (state) => {
      return state.Username;
    },
  },
});
