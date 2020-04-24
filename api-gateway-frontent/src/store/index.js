import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    Username: "",
    AccessToken: "",
  },
  mutations: {
    UpdateUsername(state, newUsername) {
      state.Username = newUsername;
    },
    UpdateAccessToken(state, newAccessToken) {
      state.AccessToken = newAccessToken;
    },
  },
  actions: {
    UpdateUsername({ commit }, newUsername) {
      commit("UpdateUsername", newUsername);
    },
    UpdateAccessToken({ commit }, newAccessToken) {
      commit("UpdateAccessToken", newAccessToken);
    },
  },
  modules: {},
  getters: {
    value: (state) => {
      return state.value;
    },
  },
});
