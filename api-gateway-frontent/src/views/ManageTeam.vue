<template>
  <div>
    manage team
  </div>
</template>

<script>
export default {
  data() {
    return {
      length: 10,
      current: 1,
      Loading: true,
    };
  },
  watch: {
    current(newValue) {
      alert("new " + newValue);
    },
  },
};
</script>

<style></style>
