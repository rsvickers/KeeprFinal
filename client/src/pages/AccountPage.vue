<template>
  <div class="about text-center">
    <img :src="account.coverImg" alt="">
    <img class="rounded-circle" :src="account.picture" alt="" />
    <p>{{ account.name }}</p>
    <p><i onclick.prevent="update" class="mdi mdi-dots-horizontal fs-3" role="button" title="edit account"
        data-bs-toggle="modal" data-bs-target="#editAccountModal"></i></p>

  </div>
</template>

<script>
import { computed } from 'vue';
import { AppState } from '../AppState';
export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
