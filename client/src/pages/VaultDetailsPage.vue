<template>
    <div v-if="vault" class="container">
        <div class="backgroundImg mb-5 mt-4 col-12 col-md-8 rounded text-light d-flex justify-content-center"
            title="Vault Name" :style="{ backgroundImage: `url(${vault?.img})` }">
            <div class="d-flex align-self-center justify-content-center">
                <h3 class="p-3 box rounded">{{ vault.name }}</h3>
            </div>
        </div>

    </div>
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watch } from 'vue';
import { vaultsService } from '../services/VaultsService';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
export default {
    setup() {
        const route = useRoute();
        const router = useRouter();
        const watchableVaultId = computed(() => route.params.vaultId)

        async function getVaultById() {
            try {
                const vaultId = route.params.vaultId;
                logger.log('VAULT ID', vaultId)
                await vaultsService.getVaultById(vaultId)
            } catch (error) {
                Pop.error(error)
                if (error.response.data.includes('🧙‍♂️')) {
                    router.push({ name: 'Home' })
                }
            }
        }


        watch(watchableVaultId, () => {
            vaultsService.clearAppState();
            getVaultById();
        }, { immediate: true });
        return {
            vault: computed(() => AppState.activeVault),
            account: computed(() => AppState.account),
        }
    }
};
</script>


<style lang="scss" scoped>
.backgroundImg {
    height: 20rem;
    width: 50%;
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