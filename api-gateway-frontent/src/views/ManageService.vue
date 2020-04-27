<template>
  <div>
    <div class="box has-text-centered">
      <h1 class="is-size-5">Manage Services</h1>
    </div>

    <v-pagination
      value="1"
      v-model="CurrentPage"
      :length="PaginationLength"
      color="light-green darken-2"
    ></v-pagination>

    <div v-for="(service, index) in Services" :key="index">
      <Services @DeleteService="RemoveService" :Service="service"></Services>
    </div>
    <v-divider></v-divider>
    <ErrorStatus
      :HeadLine="DialogHeadline"
      :Message="DialogMessage"
      :dialog="dialog"
      @CloseDialog="dialog = false"
    ></ErrorStatus>
  </div>
</template>

<script>
import ErrorStatus from "@/components/ErrorStatus.vue";
import Services from "@/components/Services.vue";
import * as global from "../globalExports.js";
export default {
  components: {
    ErrorStatus,
    Services,
  },
  watch: {
    // Update pagination based on model.
    CurrentPage(newValue) {
      // Reset the list of services when pagination changes 
       fetch(
      `${global.ApiDomainName}/api/ServiceManagement/GetOwnedServices/${this.$store.state.ClientId}/${newValue}`,
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
        this.Services = [];
        data.forEach((element) => {
          this.Services.push(element);
        });
      })
      .catch(() => {
        this.DialogHeadline = global.ErrorMessage;
        this.DialogMessage = "";
        this.dialog = true;
      });

      
    }
  },
  data() {
    return {
      // Dialog Data
      DialogMessage: "",
      dialog: false,
      DialogHeadline: "",

      // Pagination Data
      CurrentPage: 1,
      PaginationLength: 1,

      // Service Data
      Services: [],
    };
  },
  methods: {
    RemoveService(service) {
      this.Services = this.Services.filter(s => s.endpoint !== service.endpoint)
    },
  },
  created() {
    // When Create Fetch pagination for services
    fetch(
      `${global.ApiDomainName}/api/ServiceManagement/GetOwnedServicePagination/${this.$store.state.ClientId}`,
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
              this.$router.replace("/login").catch(() => {});
            }
            
            // Throw exception if status code is above 401.
            if (response > 401) {
              throw Error("response error");
            }

        return response.json();
      })
      .then((data) => {
        this.PaginationLength = data;
      })
      .catch(() => {
        this.DialogHeadline = global.ErrorMessage;
        this.DialogMessage = "";
        this.dialog = true;
      });

    // Fetch all Owned Services
    fetch(
      `${global.ApiDomainName}/api/ServiceManagement/GetOwnedServices/${this.$store.state.ClientId}/1`,
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
          this.Services.push(element);
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
