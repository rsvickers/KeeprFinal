<template>
    <div v-if="vault" class="container">
        <section class="row">

            <div class="backgroundImg mb-5 mt-4 col-12 col-md-8 rounded text-light d-flex flex-column justify-content-center"
                title="Vault Name" :style="{ backgroundImage: `url(${vault.img})` }">

                <div>
                    <h3 class="p-3 box rounded text-center">{{ vault.name }}</h3>
                </div>
                <div>
                    <p class="p-3 box rounded text-center">by: {{ vault.creator.name }}</p>
                </div>
                <div v-if="vault.creatorId == account.id">
                    <button @click="removeVault()" class="btn btn-danger mt-2" title="Delete Vault" type="button"><i
                            class="mdi mdi-delete"></i></button>
                </div>

            </div>
            <div>
                <p class="box rounded-pill w-25 text-center text-light">Keeps {{ keeps.length }}</p>
            </div>


        </section>
        <section class="row">
            <div v-for="keep in keeps" :key="keep.id" class="col-md-6 col-12">
                <KeepCardComponent :keepProp="keep" />
            </div>
        </section>
    </div>

    <div v-else class="container">
        <section class="row">
            <div class="col-12">
                Loading...
            </div>
        </section>
    </div>
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watch } from 'vue';
import { vaultsService } from '../services/VaultsService';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { keepsService } from '../services/KeepsService';
import KeepCardComponent from '../components/KeepCardComponent.vue';
export default {
    setup() {
        const route = useRoute();
        const router = useRouter();
        const watchableVaultId = computed(() => route.params.vaultId);
        async function getVaultById() {
            try {
                const vaultId = route.params.vaultId;
                logger.log('VAULT ID', vaultId);
                await vaultsService.getVaultById(vaultId);
            }
            catch (error) {
                Pop.error(error);
                if (error.response.data.includes('🧙‍♂️')) {
                    router.push({ name: 'Home' });
                }
            }
        }
        async function getKeepsInVault() {
            try {
                const vaultId = route.params.vaultId;
                await keepsService.getKeepsInVault(vaultId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        watch(watchableVaultId, () => {
            vaultsService.clearAppState();
            getVaultById();
            getKeepsInVault();
        }, { immediate: true });
        return {
            vault: computed(() => AppState.activeVault),
            account: computed(() => AppState.account),
            keeps: computed(() => AppState.vaultKeeps),
            async removeVault() {
                try {
                    const vault = AppState.activeVault;
                    const yes = await Pop.confirm("Are you sure you want to delete this vault?");
                    if (!yes) {
                        return;
                    }
                    await vaultsService.removeVault(vault.id);
                    Pop.success("Vault has been deleted");
                    router.push({ name: 'Home' });
                }
                catch (error) {
                    Pop.error(error);
                }
            }
        };
    },
    components: { KeepCardComponent }
};
</script>


<style lang="scss" scoped>
.backgroundImg {
    height: 20rem;
    width: 36%;
    background-position: center;
    background-size: cover;
    box-shadow: 1px 1px 15px;
}

.box {
    // border: solid rgb(255, 255, 255);
    box-shadow: 1px 1px rgba(215, 215, 215, 0.759);
    background-color: rgba(38, 38, 38, 0.421);
}

.avatar {
    height: 8dvh;
    width: 8dvh;
    object-fit: cover;
    object-position: center;
}
</style>