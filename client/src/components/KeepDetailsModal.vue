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
                    <section class="row">
                        <div class="col-6">
                            <img class="img-fluid" :src="keep?.img" alt="">
                        </div>
                        <div class="col-6 d-flex flex-column justify-content-between text-center">
                            <div>
                                <!-- TODO add the view and kept count -->
                                <div>
                                    <p><i class="mdi mdi-eye"></i>0</p>
                                </div>
                                <div>
                                    <p><i class="mdi mdi-alpha-k-box-outline"></i> 0</p>
                                </div>
                            </div>
                            <div>
                                <h3>{{ keep?.name }}</h3>
                            </div>
                            <div>
                                <p>Vaults</p>
                                <button type="button" class="btn btn-primary">Save changes</button>
                                <img class="avatar rounded-circle" :src="keep?.creator.picture" alt="" role="button"
                                    title="Go to there profile!">
                                <p>{{ keep?.creator.name }}</p>
                            </div>
                        </div>
                    </section>
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
export default {
    setup() {
        return {
            keep: computed(() => AppState.activeKeep)
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