import Vue from "vue";
import Vuex from "vuex";
import createPersistedState from "vuex-persistedstate";

Vue.use(Vuex);

export default new Vuex.Store({
  plugins: [createPersistedState()],
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
    ResetState({commit})
    {
      commit("UpdateUsername", "");
      commit("UpdateAccessToken", "");
      commit("UpdateLoggedIn", false);
    }
  },
  modules: {},
  getters: {
    Username: (state) => {
      return state.Username;
    },
  },
});
