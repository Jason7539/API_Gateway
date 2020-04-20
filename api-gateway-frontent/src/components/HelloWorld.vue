<template>
<div> 
  <router-link :to="About"> click to change</router-link>
  </div>
</template>

<script>
export default {
  name: "HelloWorld",

  data() {
    return {
      key: ""
    }
  },
 
};
</script>
