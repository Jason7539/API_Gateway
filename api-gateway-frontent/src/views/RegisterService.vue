<template>
  <div>
    <div class="box has-text-centered">
      <h1 class="is-size-5">Service Registration</h1>
    </div>

    <v-form ref="form" :lazy-validation="false">
      <v-text-field v-model="RouteToAccess" label="Route To Access" :rules="RouteToAccessRules" required>
        <p slot="prepend">{{ Domain }}/</p>
        <!-- <v-icon slot="prepend" color="green">mdi-minus</v-icon> -->
      </v-text-field>
      <v-row>
        <v-col cols="8" sm="4">
          <v-text-field
            v-model="Input"
            label="Input"
            filled
            shaped
            :rules="InputRules"
          ></v-text-field>
        </v-col>

        <v-col cols="8" sm="4">
          <v-text-field
            v-model="Output"
            label="Output"
            filled
            shaped
            :rules="OutputRules"
          ></v-text-field>
        </v-col>
        <v-col cols="8" sm="4">
          <v-text-field
            v-model="DataFormat"
            label="Data Format"
            filled
            shaped
            :rules="DataFormatRules"
          ></v-text-field>
        </v-col>
      </v-row>

      <v-textarea
        v-model="Description"
        label="Service Description"
        hint="Must Be less than 200 characters"
        counter="true"
        rows="3"
        :rules="DescriptionRules"
      >
      </v-textarea>

      <v-row>
        <v-col>
          <v-select label="Steps" v-model="StepsDefault" :items="StepsList">
          </v-select>
        </v-col>
        <v-col>
          <v-select
            multiple
            label="Open To"
            v-model="OpentToDefault"
            :items="TeamList"
            :rules="TeamListRules"
          >
          </v-select>
        </v-col>
      </v-row>

      <div v-if="StepsDefault >= 1">
        <v-row>
          <v-col>
            <v-text-field v-model="RouteOne" label="Https url For 1st action" ></v-text-field>
          </v-col>
          <v-col>
            <v-text-field v-model="ParameterNameOne" label="Parameter Name" hint="StoreId, IngredientId"></v-text-field>
          </v-col>
          <v-col>
            <v-text-field v-model="ParameterTypeOne" label="Parameter DataType" hint="string, int"></v-text-field>
          </v-col>
          <v-col>
            <v-select label="Http Method" v-model="HttpMethodOne" :items="HttpMethodList" ></v-select>
          </v-col>
          <v-col>
            <v-select label="Async" v-model="AsyncOne" :items="AsyncBoolList" ></v-select>
          </v-col>
        </v-row>
      </div>

      <div v-if="StepsDefault >= 2">
        <v-row>
          <v-col>
            <v-text-field v-model="RouteTwo" label="Https url For 2nd action" ></v-text-field>
          </v-col>
          <v-col>
            <v-text-field v-model="ParameterNameTwo" label="Parameter Name" hint="StoreId, IngredientId"></v-text-field>
          </v-col>
          <v-col>
            <v-text-field v-model="ParameterTypeTwo" label="Parameter DataType" hint="string, int"></v-text-field>
          </v-col>
          <v-col>
            <v-select label="Http Method" v-model="HttpMethodTwo" :items="HttpMethodList" ></v-select>
          </v-col>
          <v-col>
            <v-select label="Async" v-model="AsyncTwo" :items="AsyncBoolList" ></v-select>
          </v-col>
        </v-row>
      </div>

      <div v-if="StepsDefault >= 3">
        <v-row>
          <v-col>
            <v-text-field v-model="RouteThree" label="Https url For 3rd action" ></v-text-field>
          </v-col>
          <v-col>
            <v-text-field v-model="ParameterNameThree" label="Parameter Name" hint="StoreId, IngredientId"></v-text-field>
          </v-col>
          <v-col>
            <v-text-field v-model="ParameterTypeThree" label="Parameter DataType" hint="string, int"></v-text-field>
          </v-col>
          <v-col>
            <v-select label="Http Method" v-model="HttpMethodThree" :items="HttpMethodList" ></v-select>
          </v-col>
          <v-col>
            <v-select label="Async" v-model="AsyncThree" :items="AsyncBoolList" ></v-select>
          </v-col>
        </v-row>
      </div>

      <div v-if="StepsDefault >= 4">
        <v-row>
          <v-col>
            <v-text-field v-model="RouteFour" label="Https url For 4th action" ></v-text-field>
          </v-col>
          <v-col>
            <v-text-field v-model="ParameterNameFour" label="Parameter Name" hint="StoreId, IngredientId"></v-text-field>
          </v-col>
          <v-col>
            <v-text-field v-model="ParameterTypeFour" label="Parameter DataType" hint="string, int"></v-text-field>
          </v-col>
          <v-col>
            <v-select label="Http Method" v-model="HttpMethodFour" :items="HttpMethodList" ></v-select>
          </v-col>
          <v-col>
            <v-select label="Async" v-model="AsyncFour" :items="AsyncBoolList" ></v-select>
          </v-col>
        </v-row>
      </div>

      <v-select label="Service Step Returned" v-model="ServiceReturned" :items="StepsList.slice(0, StepsDefault)"></v-select>

    </v-form>
    <v-btn @click="CreateService">Create</v-btn>

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
import ErrorStatus from '@/components/ErrorStatus.vue';
export default {
  components: {
    ErrorStatus,
  },
  data() {
    return {
      DialogMessage: "",
      dialog: false,
      DialogHeadline: "Error",

      RouteToAccess: "",
      Input: "",
      Output: "",
      DataFormat: "",
      Description: "",
      StepsList: [1, 2, 3, 4],
      StepsDefault: 1,
      OpentToDefault: "",
      TeamList: [],

      // Select list for routing steps
      HttpMethodList: ["GET", "POST", "DELETE", "PUT"],
      AsyncBoolList: [true, false],

      // Inputs for routing steps
      RouteOne: "",
      ParameterNameOne: "",
      ParameterTypeOne: "",
      HttpMethodOne: "GET",
      AsyncOne: false,

      RouteTwo: "",
      ParameterNameTwo: "",
      ParameterTypeTwo: "",
      HttpMethodTwo: "GET",
      AsyncTwo: false,

      RouteThree: "",
      ParameterNameThree: "",
      ParameterTypeThree: "",
      HttpMethodThree: "GET",
      AsyncThree: false,

      RouteFour: "",
      ParameterNameFour: "",
      ParameterTypeFour: "",
      HttpMethodFour: "GET",
      AsyncFour: false,

      ServiceReturned: 1,

      Domain: global.ApiDomainName,

      // Rules for form inputs.
      RouteToAccessRules: [v => !!v || "Route To Access is required"],
      InputRules : [v => !!v || "Input is required"],
      OutputRules:  [v => !!v || "Output is required"],
      DataFormatRules: [v => !!v || "DataFormat is required"],
      DescriptionRules: [v => !!v || "Description is required"],
      TeamListRules: [v => !!v || "Team selection needed"],


    };
  },
  methods: {
    CreateService() {
      // Validate that all inputs in the form is filled.
      let formValid = this.$refs.form.validate();

      // If form is valid sumbit post request.
      if(formValid)
      {
        fetch(`${global.ApiDomainName}/api/ServiceManagement/CreateService`,
        {
          method: "POST",
          mode: "cors",
          headers: {
            "Authorization": "Bearer " + this.$store.state.AccessToken,
            Accept: "application/json",
            "Content-Type": "application/json",
          },
          
          body: JSON.stringify({
              Username : this.$store.state.Username,
              ClientId : this.$store.state.ClientId,
              RouteToAccess : this.RouteToAccess,
              ServiceDescription : this.Description,
              OpenTo : this.OpentToDefault, 
              Configurations : JSON.stringify({
                Steps: this.StepsDefault,
                ReturnStep: this.ServiceReturned,
                Configurations: [
                  {
                    Action: this.RouteOne, 
                    ParameterNames: this.ParameterNameOne,
                    ParameterDataTypes: this.ParameterTypeOne,
                    HttpMethod: this.HttpMethodOne,
                    Async: this.AsyncOne
                  },
                  {
                    Action: this.RouteTwo, 
                    ParameterNames: this.ParameterNameTwo,
                    ParameterDataTypes: this.ParameterTypeTwo,
                    HttpMethod: this.HttpMethodTwo,
                    Async: this.AsyncTwo
                  },
                  {
                    Action: this.RouteThree, 
                    ParameterNames: this.ParameterNameThree,
                    ParameterDataTypes: this.ParameterTypeThree,
                    HttpMethod: this.HttpMethodThree,
                    Async: this.AsyncThree
                  },
                  {
                    Action: this.RouteThree, 
                    ParameterNames: this.ParameterNameThree,
                    ParameterDataTypes: this.ParameterTypeThree,
                    HttpMethod: this.HttpMethodThree,
                    Async: this.AsyncThree
                  }
                ]
              }),
              Input: this.Input,
              Output: this.Output, 
              DataFormat: this.DataFormat 
          }),
        })
        .then((response) =>
        {
          // If unauthorized log them out.
          if(response.status === 401)
          {
              this.$store.dispatch("ResetState");
              this.$router.replace("/login");
          }
          // Throw exception if status code is above 401.

          if(response > 401)
          {
            throw Error("response error");
          }
          
          // Process response as json.
          return response.json();
        })
        .then((data) => {
          console.log(data);

          // Display result dialog based on server response.
          if(data.status)
          {
            this.DialogHeadline = "Service successfully created";
            this.DialogMessage = "";
            this.dialog = true;
            this.$refs.form.reset();
          }
          else
          {
            // If service creation failed craft message about why.
            let errorMessage = "";
            
            if(!data.endpointResult)
            {
              errorMessage += "Route to access is taken. \n";
            }
            if(!data.websiteValid)
            {
              errorMessage += "One of the action url is not valid or not https. \n";
            }

            // Display dialog.
            this.DialogHeadline = "Failed to create service";
            this.DialogMessage = errorMessage;
            this.dialog = true;
          }

        })
        .catch(() => {
          this.DialogHeadline = "Unexpected exception Please try again later";
          this.dialog = true;
          this.DialogMessage = "";
        })
          
  
        
      }
      else {
        // Inform use that form is invalid.
        this.DialogHeadline = "Form Invalid";
        this.DialogMessage = "Fields are missing are incorrect"
        this.dialog = true;
      }

      // Submit fetch request for

    },
  },
  created () {
    // Fetch the all the teams username. Used to select who the service is open to.
    fetch(`${global.ApiDomainName}/api/ServiceManagement/GetTeams`, {
      method: "GET",
      mode: "cors",
      headers: {
        "Authorization": "Bearer " + this.$store.state.AccessToken,
        Accept: "application/json",
        "Content-Type": "application/json",
      },
    })
    .then((response) =>
    {
      // If unauthorized log them out.
      if(response.status === 401)
      {
        this.$store.dispatch("ResetState");
        this.$router.replace("/login");
      }

      // Throw exception if status code is above 401.
      if(response > 401)
      {
        throw Error("response error");
      }

      return response.json()
    })
    .then((data) => 
    {
      // Process json response
      this.TeamList = data.teams;
    })
    .catch(() => {
      this.DialogHeadline = "Unexpected exception Please try again later";
      this.DialogMessage = "";
      this.dialog = true;
    });

  },
};
</script>

<style></style>
