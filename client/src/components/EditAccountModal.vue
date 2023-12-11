<template>
    <div class="modal fade" id="editAccountModal" tabindex="-1" role="dialog" aria-labelledby="editAccountModalLabel"
        aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editAccountModalLabel">Edit Account</h5>

                </div>
                <div class="modal-body">
                    <form @submit.prevent="editAccount()" class="container-fluid">
                        <!-- <section class="row justify-content-center"> -->
                        <div class="col-12">
                            <label for="name">Name</label>
                            <input v-model="editable.name" type="text" class="form-control" max-length="255">
                        </div>
                        <div class="col-12">
                            <label for="img">Profile Picture</label>
                            <input v-model="editable.picture" type="url" class="form-control" max-length="255">
                        </div>
                        <div class="col-12">
                            <label for="img">Cover Picture</label>
                            <input v-model="editable.coverImg" type="url" class="form-control" max-length="1000">
                        </div>
                        <div class="w-100 d-flex justify-content-end pt-3">
                            <button class="btn" title="Save Changes" type="submit">Save Changes<i
                                    class="mdi mdi-check"></i></button>
                        </div>
                        <!-- <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                            <button type="button" class="btn btn-primary">Save changes</button>
                        </div> -->
                        <!-- </section> -->
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
import { accountService } from '../services/AccountService';
import { Modal } from 'bootstrap';
export default {
    setup() {
        const editable = ref({})
        return {
            account: computed(() => AppState.account),
            editable,

            async editAccount() {
                try {
                    await accountService.editAccount(editable.value)
                    editable.value = {}
                    Modal.getOrCreateInstance('#editAccountModal').hide()
                    Pop.success('Edited your account!')
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped></style>