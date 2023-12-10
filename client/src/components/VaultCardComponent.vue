<template>
    <div class="component">
        <!-- TODO make this look pretty and then make it clickable to go on a details page -->
        <p> {{ vaultProp.name }}</p>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Vault } from '../models/Vault';
export default {
    props: {
        vaultProp: { type: Vault, required: true },
    },
    setup() {
        return {
            account: computed(() => AppState.account),
            vaults: computed(() => AppState.vaults)
        }
    }
};
</script>


<style lang="scss" scoped></style>