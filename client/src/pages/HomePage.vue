<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-12">
        <div class="masonry">
          <div v-for="keep in keeps" :key="keep.id">
            <KeepCardComponent :keepProp="keep" />
          </div>
        </div>
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

<style scoped lang="scss">
.masonry {
  gap: 1.25rem;
  column-gap: gap;
  columns: 20dvw;

  div {
    margin-bottom: gap;
  }

  p {
    margin: 0;
  }

  .height-100 {
    max-height: 100dvh;
  }
}

@media screen and (max-width: 768px) {
  .masonry {
    columns: 30dvw;
  }
}
</style>
