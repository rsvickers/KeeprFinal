<template>
    <div class="modal modal-xl fade" id="keepDetailsModal" tabindex="-1" aria-labelledby="keepDetailsModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content modal-xl">
                <!-- <div class="modal-header">
                    <h1 class="modal-title fs-5" id="keepDetailsModalLabel">Modal title</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div> -->
                <div class="modal-body modal-xl">
                    <div class="container">

                        <section v-if="keep?.id" class="row">
                            <div class="col-6">
                                <img class="img-fluid" :src="keep?.img" alt="">
                            </div>
                            <div class="col-6 d-flex flex-column justify-content-between text-center">
                                <div>
                                    <!-- TODO add the view and kept count -->
                                    <div>
                                        <p><i class="mdi mdi-eye"></i>{{ keep.views }}</p>
                                    </div>
                                    <div>
                                        <p><i class="mdi mdi-alpha-k-box-outline"></i> {{ keep?.kept }}</p>
                                    </div>
                                    <div>
                                        <p v-if="keep?.creatorId == account.id"><i @click="removeKeep()"
                                                class="mdi mdi-delete-circle text-danger fs-3" title="delete keep"
                                                role="button" type="button"></i></p>
                                    </div>
                                </div>
                                <div>
                                    <h3>{{ keep?.name }}</h3>
                                    <p>{{ keep?.description }}</p>
                                </div>

                                <!-- TODO work on dropdown to save vaults and such..! -->
                                <div v-if="account.id == keep?.creatorId">
                                    <div v-if="isKeptInVault" v-for="vault in vaults" :key="vault.id">
                                        <button @click.prevent="removeKeepVault(isKeptInVault.vaultKeepId)" type="button"
                                            :title="`Remove keep from ${vault.name}`">Remove</button>
                                    </div>
                                </div>
                                <div v-if="account.id == keep?.creatorId">
                                    <div v-if="!isKeptInVault">

                                        <button class="btn btn-secondary dropdown-toggle" type="button" title="my vaults"
                                            data-bs-toggle="dropdown" aria-expanded="false">
                                            Vaults
                                        </button>
                                        <ul class="dropdown-menu">
                                            <li v-for="vault in vaults" :key="vault.id">
                                                <button @click="createKeepVault(vault.id)" type="button"
                                                    :title="`Add keep to ${vault.name}`" class="dropdown-item">{{
                                                        vault.name }}</button>
                                            </li>
                                        </ul>
                                    </div>
                                </div>


                                <div v-if="account.id == keep?.creatorId">
                                    <router-link :to="{ name: 'Account' }">
                                        <img class="avatar rounded-circle" :src="keep?.creator.picture" alt="" role="button"
                                            title="Go to there profile!">
                                    </router-link>
                                    <p>{{ keep?.creator.name }}</p>
                                </div>
                                <div v-else>
                                    <router-link :to="{ name: 'Profile', params: { profileId: keep?.creatorId } }">
                                        <img class="avatar rounded-circle" :src="keep?.creator.picture" alt="" role="button"
                                            title="Go to there profile!">
                                    </router-link>
                                    <p>{{ keep?.creator.name }}</p>
                                </div>
                            </div>
                        </section>
                    </div>
                </div>
                <div>
                    <!-- <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button> -->
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState.js';
import { computed, reactive, onMounted } from 'vue';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { useRouter } from 'vue-router';
import { Modal } from 'bootstrap';
import { vaultKeepsService } from '../services/VaultKeepsService';
export default {
    setup() {
        const router = useRouter()
        return {
            keep: computed(() => AppState.activeKeep),
            vaults: computed(() => AppState.vaults),
            account: computed(() => AppState.account),
            isKeptInVault: computed(() => AppState.vaultKeeps.find((vaultKeep) => vaultKeep.id == AppState.account.id)),

            async removeKeep() {
                try {
                    const keep = AppState.activeKeep;
                    const yes = await Pop.confirm(`Are you sure you want to delete ${keep.name}`)
                    if (!yes) {
                        return
                    }
                    await keepsService.removeKeep(keep.id)
                    Modal.getInstance('#keepDetailsModal').hide()
                    Pop.success(`${keep.name} has been deleted.`)
                    AppState.activeKeep = null
                    router.push({ name: 'Home' });
                } catch (error) {
                    Pop.error(error)
                }
            },

            async createKeepVault(vaultId) {
                try {
                    const keepId = AppState.activeKeep.id
                    // const vaultId = AppState.vaults.id
                    const vaultKeepData = { vaultId, keepId }
                    await vaultKeepsService.createKeepVault(vaultKeepData)
                    Pop.success("Saved this keep!")
                } catch (error) {
                    Pop.error(error)
                }
            }
        }

    }
};
</script>


<style lang="scss" scoped>
.avatar {
    height: 8dvh;
    width: 8dvh;
    object-fit: cover;
    object-position: center;
}
</style>