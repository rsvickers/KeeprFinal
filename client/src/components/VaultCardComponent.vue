<template>
    <div>
        <router-link :to="{ name: 'VaultDetails', params: { vaultId: vaultProp?.id } }" title="open the vault page">
            <div class="backgroundImg mb-5 mt-4  rounded text-light d-flex" role="button" title="click to see details"
                :style="{ backgroundImage: `url(${vaultProp?.img})` }">
                <div class="d-flex align-items-end">
                    <p class="p-2 box rounded"> {{ vaultProp.name }}</p>
                </div>
            </div>
        </router-link>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Vault } from '../models/Vault';
import { useRoute, useRouter } from 'vue-router';
export default {
    props: {
        vaultProp: { type: Vault, required: true },
    },
    setup() {

        return {
            account: computed(() => AppState.account),
            // vaults: computed(() => AppState.vaults),

        }
    }
};
</script>


<style lang="scss" scoped>
.backgroundImg {
    height: 20rem;
    width: 100%;
    background-position: center;
    background-size: cover;
    box-shadow: 1px 1px 15px;
}

.box {
    // border: solid rgb(255, 255, 255);
    box-shadow: 1px 1px rgba(215, 215, 215, 0.759);
    background-color: rgba(38, 38, 38, 0.421);
}
</style>