<template>
  <v-dialog v-model="dialog" max-width="800" :persistent="true">
    <v-card>
      <v-card-title class="headline">{{ HeadLine }}</v-card-title>

      <v-card-text>
        {{ Message }}
      </v-card-text>

      <v-card-actions>
        <v-spacer></v-spacer>

        <v-btn color="green darken-1" text @click.stop="Result()">
          Accept
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  props: {
    HeadLine: {
      type: String,
      default: "",
    },
    Message: {
      type: String,
      default: "",
    },
    Result: {
      type: Function,
      default: function() {
        this.$emit("CloseDialog");
      },
    },
    ButtonMessage: {
      type: String,
      default: "",
    },
    dialog : {
        type: Boolean,
        default: false
    }
  },
};
</script>

<style></style>
