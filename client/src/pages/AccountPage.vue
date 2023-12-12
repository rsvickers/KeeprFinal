<template>
  <div class="container-fluid">
    <section class="row">


      <div class="text-center col-10 col-md-8 text-center">
        <img :src="account.coverImg" alt="">
        <img class="rounded-circle" :src="account.picture" alt="" />
        <p>{{ account.name }}</p>
        <p><i onclick.prevent="update" class="mdi mdi-dots-horizontal fs-3" role="button" title="edit account"
            data-bs-toggle="modal" data-bs-target="#editAccountModal"></i></p>
      </div>

      <div>
        <p>{{ vaults.length }} Vaults || {{ keeps.length }} Keeps</p>

      </div>
      <h2>Vaults</h2>
      <div v-for="vault in vaults" :key="vault.id" class="col-md-3 col-6">
        <VaultCardComponent :vaultProp="vault" />
      </div>
      <h2>Keeps</h2>
      <div v-for="keep in keeps" :key="keep.id" class="col-md-3 col-6">
        <KeepCardComponent :keepProp="keep" />
      </div>



    </section>
  </div>
</template>

<script>
import { computed, watchEffect } from 'vue';
import { AppState } from '../AppState';
import VaultCardComponent from '../components/VaultCardComponent.vue';
import KeepCardComponent from '../components/KeepCardComponent.vue';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService';
import { profilesService } from '../services/ProfilesService';
export default {
  setup() {
    watchEffect(() => {
      AppState.account;
      getMyVaults();
      getMyKeeps()
    })

    async function getMyVaults() {
      try {
        await vaultsService.getMyVaults()
        const userId = AppState.account.id
        await profilesService.getKeepsByProfile(userId)
      } catch (error) {
        Pop.error(error)
      }
    }

    async function getMyKeeps() {
      try {
        await profilesService.getKeepsByProfile()
      } catch (error) {
        Pop.error(error)
      }
    }



    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),
    };
  },
  components: { VaultCardComponent, KeepCardComponent }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
