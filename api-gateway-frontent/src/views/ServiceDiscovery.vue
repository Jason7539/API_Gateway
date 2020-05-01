<template>
  <div>
    <el-table
      :data="
        availableServices.filter(
          data =>
            !search ||
            data.username.toLowerCase().includes(search.toLowerCase()) ||
            data.endpoint.toLowerCase().includes(search.toLowerCase()) ||
            data.input.toLowerCase().includes(search.toLowerCase()) ||
            data.output.toLowerCase().includes(search.toLowerCase()) ||
            data.dataformat.toLowerCase().includes(search.toLowerCase())
        )
      "
      :default-sort="{ prop: 'endpoint', order: 'ascending' }"
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
      <el-table-column prop="endpoint" label="EndPoint" width="180" sortable>
      </el-table-column>
      <el-table-column prop="username" label="Team" width="180">
      </el-table-column>
      <el-table-column prop="input" label="Input Type" width="180">
      </el-table-column>
      <el-table-column prop="output" label="Output Type" width="180">
      </el-table-column>
      <el-table-column prop="dataFormat" label="Data Format" width="180">
      </el-table-column>
      <el-table-column align="right">
        <template slot="header" slot-scope="{}">
          <el-input v-model="search" size="mini" placeholder="Type to filter" />
        </template>
      </el-table-column>
    </el-table>
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
      search: "",
      DialogHeadline: "",
      dialog: false,
      DialogMessage: ""
    };
  },
  methods: {},
  created() {
    // Submit post request
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
        // Proccess response status code
        if (!response.ok) {
          throw Error("response error");
        }
        // Process response as json
        return response.json();
      })
      .then(data => {
        data.forEach(service => {
          this.availableServices.push(service);
        });
      })
      .catch(error => {
        // For unexpected errors display error page.
        console.log(error);
        this.DialogHeadline = "Unexpected error has occurred";
        this.DialogMessage = "Please contact system admin";
        this.dialog = true;
      });
  }
};
</script>

<style>
@import url("//unpkg.com/element-ui@2.13.1/lib/theme-chalk/index.css");
</style>
