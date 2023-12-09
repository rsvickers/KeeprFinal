<template>
    <div class="modal fade" id="createKeepModal" tabindex="-1" role="dialog" aria-labelledby="createKeepModalLabel"
        aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="createKeepModal">Add your keep</h5>
                </div>
                <div class="modal-body">
                    <form @submit="createKeep()">
                        <div class="col-12">
                            <label for="name">Name...</label>
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
                        <div class="col-4 text-center mt-3">
                            <button class="btn btn-success" type="submit" title="Create The Keep">Create Keep</button>
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
import { keepsService } from '../services/KeepsService.js';
import { Modal } from 'bootstrap';
import { useRouter } from 'vue-router';
export default {
    setup() {
        const editable = ref({})
        const router = useRouter()
        return {
            editable,

            async createKeep() {
                try {
                    const keepData = editable.value
                    await keepsService.createKeep(keepData)
                    Pop.success('Keep has been created!')
                    editable.value = {}
                    Modal.getOrCreateInstance('#createKeepModal').hide()
                    // Modal.getOrCreateInstance(#keepDetailsModal).show()
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