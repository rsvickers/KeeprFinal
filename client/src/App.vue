<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>

  <KeepDetailsModal />
  <CreateKeepModal />
  <CreateVaultModal />
  <EditAccountModal />
  <!-- <VaultKeepComponent /> -->
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import KeepDetailsModal from './components/KeepDetailsModal.vue'
import CreateKeepModal from './components/CreateKeepModal.vue'
import EditAccountModal from './components/EditAccountModal.vue'
import CreateVaultModal from './components/CreateVaultModal.vue'
// import VaultKeepComponent from './components/VaultKeepComponent.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, KeepDetailsModal, CreateKeepModal, EditAccountModal, CreateVaultModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
