<template>
    <div>
        manage team

        <v-pagination
        v-model="current"
        :length="length"
        ></v-pagination>
    <div v-if="Loading" class="text-center">
      <v-progress-circular
        :size="100"
        color="primary"
        indeterminate
      ></v-progress-circular>
    </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            length: 10,
            current: 1,
            Loading: true,
        }
    },
    watch: {
        current(newValue) {
            alert("new " + newValue)
        }
    },
}
</script>

<style>

</style>