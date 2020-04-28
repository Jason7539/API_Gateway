<template>
  <div>
    <v-form ref="form" :lazy-validation="false">
      <v-text-field
        id="Username"
        v-model="Username"
        label="Username"
        :rules="UsernameRules"
        v-on:keydown.enter="Login"
        required
      ></v-text-field>

      <v-text-field
        id="Password"
        type="password"
        v-model="Password"
        label="Password"
        :rules="PasswordRules"
        v-on:keydown.enter="Login"
        required
      ></v-text-field>

      <ErrorStatus
        :HeadLine="DialogHeadline"
        :Message="DialogMessage"
        :dialog="dialog"
        @CloseDialog="dialog = false"
      ></ErrorStatus>

      <v-btn id="Login" class="button" @click="Login">Login</v-btn>
      <div v-if="Loading" class="text-center">
        <v-progress-circular
          :size="100"
          color="primary"
          indeterminate
        ></v-progress-circular>
      </div>
    </v-form>
  </div>
</template>

<script>
import * as global from "../globalExports.js";
import ErrorStatus from "@/components/ErrorStatus.vue";
export default {
  components: {
    ErrorStatus,
  },
  data() {
    return {
      Loading: false,
      Username: "",
      Password: "",
      DialogMessage: "",
      dialog: false,
      DialogHeadline: "",

      // Input form validation rules
      UsernameRules: [
        (v) => !!v || "Username is required",
        (v) =>
          v.length >= 4 || "Username must be greater or equal to 4 characters",
        (v) => v.length < 200 || "Username must be less than 200 characters",
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
      let formValid = this.$refs.form.validate();

      // If the form is valid post to backend.
      if (formValid) {
        // Display loading animation.
        this.Loading = true;

        // Submit post request
        fetch(`${global.ApiDomainName}/api/Authenticate/Login`, {
          method: "POST",
          mode: "cors",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
          },

          body: JSON.stringify({
            Username: this.$data.Username,
            Password: this.$data.Password,
          }),
        })
          .then((response) => {
            // Remove loading on response.
            this.Loading = false;

            // Proccess response status code
            // If unauthorized log them out.
            if (response.status === 401) {
              this.$store.dispatch("ResetState");
              this.$router.replace("/login").catch((err) => err);
            }

            // Throw exception if status code is above 401.
            if (response > 401) {
              throw Error("response error");
            }
            // Process response as json
            return response.json();
          })
          .then((data) => {
            // If we get a successful response back.
            if (data.status === true) {
              // Login was successful: store access token, username and updated loggedIn status.
              this.$store.dispatch("UpdateUsername", data.username);
              this.$store.dispatch("UpdateAccessToken", data.accessToken);
              this.$store.dispatch("UpdateClientId", data.clientId);
              this.$store.dispatch("UpdateLoggedIn", true);

              // Take User to login page when they successful log in.
              this.$router.replace("/").catch((err) => err);
            } else {
              // Login failed: Display error message.
              this.DialogHeadline = "Login Failed";
              this.DialogMessage = "One of the above fields is incorrect";
              this.dialog = true;
            }
          })
          .catch(() => {
            // Remove loading.
            this.Loading = false;

            // For unexpected errors display error page.
            this.DialogHeadline = global.ErrorMessage;
            this.DialogMessage = "Please contact system admin";
            this.dialog = true;
          });
      } else {
        // Display error dialog when form is invalid.
        this.DialogMessage = "Input is not valid or missing fields";
        this.dialog = true;
      }
    },
  },
};
</script>

<style></style>
