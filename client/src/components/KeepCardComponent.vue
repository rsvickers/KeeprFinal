<template>
    <div @click.prevent="openKeepDetails(keepProp)" class="backgroundImg mb-5 mt-4  rounded text-light d-flex" role="button"
        title="click to see details" :style="{ backgroundImage: `url(${keepProp?.img})` }">

        <div class="d-flex justify-content-between align-items-end">
            <p class="p-2 box rounded mx-4">{{ keepProp?.name }}</p>
            <img class="avatar rounded-circle" :src="keepProp?.creator.picture" alt="" role="button"
                title="Go to there profile!">
        </div>

    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from '../models/Keep';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService.js'
import { Modal } from 'bootstrap';

export default {
    props: {
        keepProp: { type: Keep, required: true },
    },
    setup(props) {

        // watch(() => {
        //     keepsService.clearAppState()
        // })
        return {
            account: computed(() => AppState.account),

            async openKeepDetails(keepProp) {
                try {
                    // AppState.activeKeep = {}
                    await keepsService.openKeepDetails(keepProp)
                    Modal.getOrCreateInstance('#keepDetailsModal').show()
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped>
// TODO eventually change this to masonry
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

.avatar {
    height: 8dvh;
    width: 8dvh;
    object-fit: cover;
    object-position: center;
}
</style>