<template>
  <div class="container-fluid">
    <section class="row justify-content-center">


      <div class="text-center col-10 col-md-6 text-center">
        <img class="coolImg" :src="account.coverImg" alt="">
      </div>

      <div class="absolute">
        <div class="d-flex justify-content-center align-items-end">
          <img class="profile" :src="account.picture" alt="" />
          <p><i onclick.prevent="update" class="mdi mdi-dots-horizontal fs-3" role="button" title="edit account"
              data-bs-toggle="modal" data-bs-target="#editAccountModal"></i></p>
        </div>

        <div class="text-center mt-3">
          <h3>{{ account.name }}</h3>
          <p>{{ vaults.length }} Vaults || {{ keeps.length }} Keeps</p>
        </div>
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
import { Modal } from 'bootstrap';
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
        Modal.getOrCreateInstance('#keepDetailsModal').hide()
      } catch (error) {
        Pop.error(error)
      }
    }

    async function getMyKeeps() {
      try {
        const accountId = AppState.account.id
        await profilesService.getKeepsByProfile(accountId)
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
.profile {

  height: 15dvh;
  width: 15dvh;
  border-radius: 5rem;
}

h2 {
  margin-top: 10dvh;
  margin-left: 5vw;
}

.coolImg {
  height: 25dvh;
  width: 100%;
}

.absolute {
  position: absolute;
  left: 1%;
  top: 23%;
}
</style>
