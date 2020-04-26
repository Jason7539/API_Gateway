<template>
  <div>
    <v-form ref="form" :lazy-validation="false">
      <v-text-field
        v-model="Username"
        label="Username"
        :rules="UsernameRules"
        v-on:keydown.enter="Login"
        required
      ></v-text-field>
      <v-text-field
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

      <v-btn class="button" @click="Login">Login</v-btn>
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
      Username: "",
      Password: "",
      DialogMessage: "",
      dialog: false,
      DialogHeadline: "Error",

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
            Password: this.$data.Password
          }),
        })
          .then((response) =>{
            // Proccess response status code
            if(!response.ok)
            {
              throw Error("response error");
            }
            // Process response as json
            return response.json()
          })
          .then((data) =>{
            // If we get a successful response back.
            if (data.status === true)
            {
              // Login was successful: store access token, username and updated loggedIn status.
              this.$store.dispatch("UpdateUsername", data.username);
              this.$store.dispatch("UpdateAccessToken", data.accessToken);
              this.$store.dispatch("UpdateClientId", data.clientId);
              this.$store.dispatch("UpdateLoggedIn", true);

              // Take User to login page when they successful log in.
              this.$router.replace("/");
            }else{
              // Login failed: Display error message.
              this.DialogHeadline = "Login Failed";
              this.DialogMessage = "One of the above fields is incorrect";
              this.dialog = true;
            }
          })
          .catch((error) =>{            
            // For unexpected errors display error page.
            console.log(error);
            this.DialogHeadline = "Unexpected error has occurred";
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
