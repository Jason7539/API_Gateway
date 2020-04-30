<template>
  <div>
    <el-table
      :data="
        registeredServices.filter(
          data =>
            !search ||
            data.team_name.toLowerCase().includes(search.toLowerCase()) ||
            data.url.toLowerCase().includes(search.toLowerCase()) ||
            data.input_type.toLowerCase().includes(search.toLowerCase()) ||
            data.output_type.toLowerCase().includes(search.toLowerCase()) ||
            data.data_format.toLowerCase().includes(search.toLowerCase())
        )
      "
      :default-sort="{ prop: 'url', order: 'ascending' }"
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
      <el-table-column prop="url" label="URL" width="180" sortable>
      </el-table-column>
      <el-table-column prop="team_name" label="Team" width="180">
      </el-table-column>
      <el-table-column prop="input_type" label="Input Type" width="180">
      </el-table-column>
      <el-table-column prop="output_type" label="Output Type" width="180">
      </el-table-column>
      <el-table-column prop="data_format" label="Data Format" width="180">
      </el-table-column>
      <el-table-column align="right">
        <template slot="header" slot-scope="{}">
          <el-input v-model="search" size="mini" placeholder="Type to filter" />
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
import Vue from "vue";
import Element from "element-ui";
Vue.use(Element, { size: "small", zIndex: 3000 });
export default {
  name: "ServiceDiscovery",
  data() {
    return {
      registeredServices: [
        {
          url: "www.testurl1.com",
          team_name: "TeamA",
          input_type: "int",
          output_type: "int",
          data_format: "xml",
          description: "Testing description for this service"
        },
        {
          url: "www.testurl2.com",
          team_name: "SixFold",
          input_type: "string",
          output_type: "string",
          data_format: "json",
          description: "Testing description for this service"
        },
        {
          url: "www.testurl3.com",
          team_name: "TheBoiz",
          input_type: "string",
          output_type: "string",
          data_format: "xml",
          description: "Testing description for this service"
        }
      ],
      search: ""
    };
  },
  methods: {}
};
</script>

<style>
@import url("//unpkg.com/element-ui@2.13.1/lib/theme-chalk/index.css");
</style>
