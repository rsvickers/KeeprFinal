<template>
  <div class="container-fluid">
    <section class="row">
      <div v-for="keep in keeps" :key="keep.id" class="col-md-3 col-6">
        <KeepCardComponent :keepProp="keep" />
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop';
import { AppState } from '../AppState.js'
import KeepCardComponent from '../components/KeepCardComponent.vue';

export default {
  setup() {
    onMounted(() => {
      getKeeps();
    });
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      }
      catch (error) {
        Pop.error(error);
      }
    }
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { KeepCardComponent }
}
</script>

<style scoped lang="scss"></style>
