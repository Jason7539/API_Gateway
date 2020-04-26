<template>
  <div>
    <h1>Team Registration</h1>
    <v-form ref="form" :lazy-validation="false">
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
      <v-text-field
        type="password"
        v-model="RepeatPassword"
        label="Repeat Password"
        :rules="RepeatPasswordRules"
        required
      ></v-text-field>
      <v-text-field
        v-model="WebsiteUrl"
        label="Website URL"
        :rules="WebsiteUrlRules"
        required
      ></v-text-field>
      <v-text-field
        v-model="CallbackUrl"
        label="Callback URL"
        :rules="CallbackUrlRules"
        required
      ></v-text-field>
    </v-form>

    <v-btn class="button" @click="Submit">Submit</v-btn>
    <!-- Dialog to display the status of the form submission -->
    <v-dialog v-model="dialog" max-width="800" :persistent="true">
      <v-card>
        <v-card-title class="headline">Registration Status</v-card-title>

        <v-card-text>
          {{ DialogMessage }}
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn color="green darken-1" text @click.stop="GoToLogin()">
            Accept
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import * as global from "../globalExports.js";
export default {
  data() {
    return {
      Username: "",
      Password: "",
      RepeatPassword: "",
      WebsiteUrl: "",
      CallbackUrl: "",
      FormStatus: false,
      dialog: false,
      DialogMessage: "",

      // Rules callback
      UsernameRules: [
        (v) => !!v || "Username is required",
        (v) =>
          v.length >= 4 || "Username must be greater or equal to 4 characters",
          (v) => v.length < 200 || "Username must be less than 200 characters"
      ],
      PasswordRules: [
        (v) => !!v || "Password is required",
        (v) => v.length >= 12 || "Password must be greater or equal to 12",
        (v) => v ==this.$data.RepeatPassword || "Passwords are not equal",
        (v) => v.length < 2000 || "Password  must be less than 2000",
      ],
      RepeatPasswordRules: [
        (v) => !!v || "Password is required",
        (v) => v.length >= 12 || "Password must be greater or equal to 12",
        (v) => v === this.$data.Password || "Passwords are not equal",
        (v) => v.length < 2000 || "Password  must be less than 2000",
      ],
      WebsiteUrlRules: [(v) => !!v || "Website url is required"],
      CallbackUrlRules: [(v) => !!v || "Callback url is required"],
    };
  },
  methods: {
    Submit() {
      // Test if the form is valid.
      let formValid = this.$refs.form.validate();

      // If the form is valid submit to backend.
      if (formValid) {
        // Submit post request.
        fetch(`${global.ApiDomainName}/api/TeamRegistration/CreateTeam`, {
          method: "POST",
          mode: "cors",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
          },

          body: JSON.stringify({
            Username: this.$data.Username,
            Password: this.$data.Password,
            RepeatPassword: this.$data.RepeatPassword,
            WebsiteUrl: this.$data.WebsiteUrl,
            CallbackUrl: this.$data.CallbackUrl,
          }),
        })
          .then((response) => {
            // Throw exception if status code is above 400.
            if(!response.ok)
            {
              throw Error("response error");
            }
            // Process response as json.
            return response.json()})
          .then((data) => {
            // If we get a successful response back.
            if (data.teamCreate === true) {
              // Created display the clientId and clientSecret to be saved.
              this.DialogMessage =
                "Registration was successfull: Please save your clientId and secret in your backend code " +
                "\n" +
                "clientId: " +
                data.clientId +
                "\n" +
                "clientSecret: " +
                data.clientSecret;

              // Display dialog. 
              this.dialog = true;
              this.FormStatus = true;
            } else {
              // If the team creation is unsuccessful tell user what was wrong.
              var errorMesssage = "";

              // Proccess json response to form error message.
              if (!data.nameUnique) {
                errorMesssage +=
                  "name may not be unique or does not meet length requirements. length has to be less than 200 and greater than 4." +
                  "\n";
              }

              if (!data.passwordValid) {
                errorMesssage +=
                  "password is not valid or does not match." + "\n";
              }

              if (!data.websiteUnique) {
                errorMesssage += "Website url is already in use." + "\n";
              }

              if (!data.websiteValid) {
                errorMesssage += "Website is not valid or not https." + "\n";
              }

              if (!data.websiteAlive) {
                errorMesssage += "Website is not responding" + "\n";
              }

              if (!data.callbackUnique) {
                errorMesssage += "Callback is not unique" + "\n";
              }

              if (!data.callbackValid) {
                errorMesssage += "Callback is not valid" + "\n";
              }

              if (!data.callbackAlive) {
                errorMesssage += "Callback is not alive" + "\n";
              }

              // Display the dialog to display.
              this.dialog = true;
              this.DialogMessage = errorMesssage;
            }
          })
          .catch((error) => {
            // If we get an error for fetch. Open dialog to contact system admin.
            console.log(error);
            this.dialog = true;
            this.DialogMessage = "Operation failed: contact system admin";
          });
      } else {
        // If the form is invalid display message when they submit.
        this.DialogMessage = "Form is not valid or missing fields";
        this.dialog = true;
      }
    },
    GoToLogin() {
      this.dialog = false;
      // if formstatus true take to login page.
      if (this.FormStatus === true) {
        this.$router.push("Login");
      }
    },
  },
};
</script>

<style></style>
