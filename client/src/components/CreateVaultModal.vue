<template>
    <div></div>
    <div class="modal fade" id="createVaultModal" tabindex="-1" role="dialog" aria-labelledby="createVaultModalLabel"
        aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="createVaultModal">Create Vault</h5>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createVault()">
                        <div class="col-12">
                            <label for="name">Title...</label>
                            <input v-model="editable.name" type="text" class="form-control" max-length="255" id="name"
                                required>
                        </div>
                        <div class="col-12">
                            <label for="img">Image...</label>
                            <input v-model="editable.img" type="url" class="form-control" max-length="1000" id="img"
                                required>
                        </div>

                        <div class="col-12">
                            <label for="description">Description...</label>
                            <textarea v-model="editable.description" type="text" class="form-control" id="description"
                                maxlength="1000" rows="4" required></textarea>
                        </div>

                        <div class="form-check">
                            <label class="isPrivate" for="exampleCheck1">Make Vault Private?</label>
                            <input v-model="editable.isPrivate" type="checkbox" class="form-check-input" id="isPrivate">
                        </div>

                        <div class="modal-footer col-4 text-center mt-3">
                            <button class="btn btn-success" type="submit" title="Create The Keep">Create Vault</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService.js'
import { Modal } from 'bootstrap';

export default {
    setup() {
        const editable = ref({})
        return {
            editable,

            async createVault() {
                try {
                    const vaultData = editable.value
                    await vaultsService.createVault(vaultData)
                    Pop.success('Vault has been created!')
                    Modal.getOrCreateInstance('#createVaultModal').hide()
                    editable.value = {}
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped></style>