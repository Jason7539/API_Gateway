<template>
  <div>
    <v-form>
      <v-text-field
        v-model="Username"
        label="Username"
        :rules="UsernameRules"
        required
      ></v-text-field>
      <v-text-field
        type="password"
        v-model="Password"
        label="Password"
        :rules="PasswordRules"
        required
      ></v-text-field>
  
    <v-btn class="button" @click="Login">Login</v-btn>
    </v-form>
  </div>
</template>

<script>
// import * as global from "../globalExports.js";
export default {
  data() {
    return {
      Username: "",
      Password: "",

      UsernameRules: [
        (v) => !!v || "Username is required",
        (v) =>
          v.length >= 4 || "Username must be greater or equal to 4 characters",
      ],
      PasswordRules: [
        (v) => !!v || "Password is required",
        (v) => v.length >= 12 || "Password must be greater or equal to 12",
        (v) => v.length < 2000 || "Password  must be less than 2000",
      ],
    };
  },
  methods: {
    Login() {
      // Check if form is valid 
      // Submit post request if it is.
    }
  },
};
</script>

<style></style>
