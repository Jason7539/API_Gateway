<template>
  <div>
    <!-- Filter will search all column based on user input-->
    <el-table
      :data="
        availableServices.filter(
          data =>
            !filter ||
            data.username.toLowerCase().includes(filter.toLowerCase()) ||
            data.endpoint.toLowerCase().includes(filter.toLowerCase()) ||
            data.input.toLowerCase().includes(filter.toLowerCase()) ||
            data.output.toLowerCase().includes(filter.toLowerCase()) ||
            data.dataformat.toLowerCase().includes(filter.toLowerCase())
        )
      "
      style="width: 100% "
      stripe
    >
      <el-table-column type="expand">
        <template slot-scope="props">
          <el-form label-position="top" inline class="table-expand">
            <el-form-item label="Description">
              <span>{{ props.row.description }}</span>
            </el-form-item>
          </el-form>
        </template>
      </el-table-column>
      <el-table-column prop="endpoint" label="EndPoint" width="300" sortable>
      </el-table-column>
      <el-table-column prop="username" label="Team" width="180">
      </el-table-column>
      <el-table-column prop="input" label="Input Type" width="180">
      </el-table-column>
      <el-table-column prop="output" label="Output Type" width="180">
      </el-table-column>
      <el-table-column prop="dataformat" label="Data Format" width="180">
      </el-table-column>
      <el-table-column align="top">
        <template slot="header" slot-scope="{}">
          <el-input
            id="filterInput"
            v-model="filter"
            size="mini"
            placeholder="Type to filter"
          />
        </template>
      </el-table-column>
    </el-table>
    <v-btn @click="Refresh">Refresh Data</v-btn>
    <ErrorStatus
      :HeadLine="DialogHeadline"
      :Message="DialogMessage"
      :dialog="dialog"
      @CloseDialog="dialog = false"
    ></ErrorStatus>
  </div>
</template>

<script>
import Vue from "vue";
import Element from "element-ui";
import ErrorStatus from "@/components/ErrorStatus.vue";
import * as global from "../globalExports.js";

Vue.use(Element, { size: "small", zIndex: 3000 });

export default {
  name: "ServiceDiscovery",
  components: {
    ErrorStatus
  },
  data() {
    return {
      availableServices: [],
      filter: "",
      DialogHeadline: "",
      dialog: false,
      DialogMessage: ""
    };
  },
  methods: {
    DisplayServices() {
      //Fetch data using the current clientId
      fetch(
        `${global.ApiDomainName}/api/ServiceDiscovery/DisplayServices/${this.$store.state.ClientId}`,
        {
          method: "GET",
          mode: "cors",
          headers: {
            Authorization: "Bearer " + this.$store.state.AccessToken,
            Accept: "application/json",
            "Content-Type": "application/json"
          }
        }
      )
        .then(response => {
          // Check 401 Unauthorized Error
          // If unauthorized logout current user.
          if (response.status === 401) {
            this.$store.dispatch("ResetState");
            this.$router.replace("/login").catch(error => error);

            this.DialogHeadline = "Warning!";
            this.DialogMessage = "Please login first!";
            this.dialog = true;
          }

          // Proccess response status code
          else if (!response.ok) {
            throw Error("response error");
          }
          // Process response as json
          return response.json();
        })
        .then(data => {
          //Add all objects fron json to availableServices
          data.forEach(service => {
            this.availableServices.push(service);
          });
        })
        .catch(error => {
          //Display error message for unexpected error
          console.log(error);
          this.DialogHeadline = "Unexpected error has occurred";
          this.DialogMessage = "Please contact system admin";
          this.dialog = true;
        });
    },

    //When click the refresh button, will reset the filter and reload data from backend
    //If user apllied sort order, the sort order will be kept
    Refresh() {
      this.filter = "";
      this.availableServices = [];
      this.DisplayServices();
    }
  },

  //Call display services when webpage loaded
  created() {
    this.DisplayServices();
  }
};
</script>

<style>
@import url("//unpkg.com/element-ui@2.13.1/lib/theme-chalk/index.css");
</style>
