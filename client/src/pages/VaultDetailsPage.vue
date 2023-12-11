<template>
    <div v-if="vault" class="container">

        <p>{{ vault.name }}</p>
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


<style lang="scss" scoped></style>