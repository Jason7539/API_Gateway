<template>
  <div>
    <v-row>
      <v-col
        ><p>{{ Service.endpoint }}</p></v-col
      >
      <v-col
        ><p>{{ Service.input }}</p></v-col
      >
      <v-col
        ><p>{{ Service.output }}</p></v-col
      >
      <v-col
        ><p>{{ Service.dataFormat }}</p></v-col
      >
      <v-col
        ><p>{{ Service.description }}</p></v-col
      >
      <v-col
        ><v-select
          label="Open To"
          v-model="OpenTo"
          :items="Teams"
          multiple
        ></v-select
      ></v-col>
      <v-col
        ><v-btn @click="Delete">
          <v-icon>mdi-delete</v-icon>
        </v-btn></v-col
      >

      <v-col
        ><v-btn @click="Save">
          <v-icon>mdi-content-save</v-icon>
        </v-btn></v-col
      >
    </v-row>
      <div v-if="Loading" class="text-center">
        <v-progress-circular
          :size="100"
          color="primary"
          indeterminate
        ></v-progress-circular>
      </div>


        <ErrorStatus
      :HeadLine="DialogHeadline"
      :Message="DialogMessage"
      :dialog="dialog"
      @CloseDialog="dialog = false"
    ></ErrorStatus>
  </div>
</template>

<script>
import * as global from "../globalExports.js";
import ErrorStatus from "@/components/ErrorStatus.vue";

export default {
  components: {
    ErrorStatus,
  },
  props: {
    Service: {
      type: Object,
    },
  },
  data() {
    return {
      Loading: false,
      // Dialog Data
      DialogMessage: "",
      dialog: false,
      DialogHeadline: "",

      // Privacy change data
      Teams: [],
      OpenTo: [],
    };
  },
  methods: {
    Delete() {
      // Display loading
      this.Loading = true;

      fetch(
        `${global.ApiDomainName}/api/ServiceManagement/DeleteService/${this.Service.endpoint}`,
        {
          method: "DELETE",
          mode: "cors",
          headers: {
            Authorization: "Bearer " + this.$store.state.AccessToken,
            Accept: "application/json",
            "Content-Type": "application/json",
          },
        }
      )
        .then((response) => {
          // Remove loading.
          this.Loading = false;


            // If unauthorized log them out.
            if (response.status === 401) {
              this.$store.dispatch("ResetState");
              this.$router.replace("/login").catch(err => err);
            }

            // Throw exception if status code is above 401.
            if (response > 401) {
              throw Error("response error");
            }

          return response.json();
        })
        .then((data) => {
          if(!data)
          {
            throw Error();
          }
          // Emit event to delete this component in the parent vue.
          this.$emit("DeleteService", this.Service);
        })
        .catch(() => {
          // Remove loading.
          this.Loading = false;

          this.DialogHeadline = global.ErrorMessage;
          this.DialogMessage = "";
          this.dialog = true;
        });
    },
    Save() {
          // Display loading.
          this.Loading = true;
            fetch(
        `${global.ApiDomainName}/api/ServiceManagement/UpdateServicePrivacy`,
        {
          method: "PATCH",
          mode: "cors",
          headers: {
            Authorization: "Bearer " + this.$store.state.AccessToken,
            Accept: "application/json",
            "Content-Type": "application/json",
          },

          body: JSON.stringify({
            ClientId: this.$store.state.ClientId,
            EndPoint: this.Service.endpoint,
            OpenTo: this.OpenTo
          }),
        }
      )
        .then((response) => {
          // Remove loading.
          this.Loading = false;
            // If unauthorized log them out.
            if (response.status === 401) {
              this.$store.dispatch("ResetState");
              this.$router.replace("/login").catch(err => err);
            }
            
            // Throw exception if status code is above 401.
            if (response > 401) {
              throw Error("response error");
            }
          return response.json();
        })
        .then((data) => {
          if(!data)
          {
            throw Error();
          }


          this.DialogHeadline = "Service Updated";
          this.DialogMessage = "";
          this.dialog = true;

        })
        .catch(() =>{
          // Remove loading.
          this.Loading = false;

          this.DialogHeadline = global.ErrorMessage;
          this.DialogMessage = "";
          this.dialog = true;
        })

    },
  },
  created() {
    /////////////////////// FETCH WHO THIS SERVICE IS OPEN TO /////////////////////////////
    fetch(
      `${global.ApiDomainName}/api/ServiceManagement/GetAllowedUsers/${this.Service.endpoint}`,
      {
        method: "GET",
        mode: "cors",
        headers: {
          Authorization: "Bearer " + this.$store.state.AccessToken,
          Accept: "application/json",
          "Content-Type": "application/json",
        },
      }
    )
      .then((response) => {
        // Display error message if response isn't valid.
            // If unauthorized log them out.
            if (response.status === 401) {
              this.$store.dispatch("ResetState");
              this.$router.replace("/login").catch(err => err);
            }
            
            // Throw exception if status code is above 401.
            if (response > 401) {
              throw Error("response error");
            }

        return response.json();
      })
      .then((data) => {
        data.forEach((element) => {
          // Loop through team list and add to data.
          this.OpenTo.push(element);
        });
      })
      .catch(() => {
        this.DialogHeadline = global.ErrorMessage;
        this.DialogMessage = "";
        this.dialog = true;
      });

    ////////////////////////////////// FETCH ALL TEAMS////////////////////////
    fetch(`${global.ApiDomainName}/api/ServiceManagement/GetTeams`, {
      method: "GET",
      mode: "cors",
      headers: {
        Authorization: "Bearer " + this.$store.state.AccessToken,
        Accept: "application/json",
        "Content-Type": "application/json",
      },
    })
      .then((response) => {
        // Display error message if response isn't valid.
            // If unauthorized log them out.
            if (response.status === 401) {
              this.$store.dispatch("ResetState");
              this.$router.replace("/login").catch(err => err);
            }
            
            // Throw exception if status code is above 401.
            if (response > 401) {
              throw Error("response error");
            }

        return response.json();
      })
      .then((data) => {
        data.teams.forEach((element) => {
          // Loop through team list and add to data.
          this.Teams.push(element);
        });
      })
      .catch(() => {
        this.DialogHeadline = global.ErrorMessage;
        this.DialogMessage = "";
        this.dialog = true;
      });
  },
};
</script>

<style></style>
