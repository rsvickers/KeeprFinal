<template>
    <div @click.prevent="openKeepDetails(keepProp)" class=" mb-5 mt-4 img-shadow rounded backgroundImg text-light d-flex"
        role="button" title="click to see details" :style="{ backgroundImage: `url(${keepProp?.img})` }">
        <!-- <div @click="updateKeep()"> -->

        <div>
            <p v-if="keepProp?.creatorId == account.id"><i @click.stop="removeKeep()"
                    class="mdi mdi-delete-circle text-danger fs-1 box p-2 m-3 rounded" title="delete keep" role="button"
                    type="button"></i>
            </p>
        </div>
        <div class=" d-flex justify-content-between align-items-end">
            <p class="p-2 box rounded">{{ keepProp?.name }}</p>

            <img class="avatar rounded-circle mb-2" :src="keepProp?.creator.picture || keepProp?.creator.img" alt=""
                role="button" :title="`${keepProp?.creator.name}`">

        </div>
        <!-- </div> -->

    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watch, watchEffect } from 'vue';
import { Keep } from '../models/Keep';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService.js'
import { Modal } from 'bootstrap';
import { useRouter } from 'vue-router';
import { vaultsService } from '../services/VaultsService';

export default {
    props: {
        keepProp: { type: Keep, required: true },
    },
    setup(props) {
        const router = useRouter();

        watchEffect(() => {
            keepsService.clearAppState()
        })

        // watch(() => {
        //     keepsService.clearAppState()
        // })
        return {
            account: computed(() => AppState.account),

            async updateKeep() {
                try {
                    const keep = AppState.activeKeep
                    // const keepData =
                    await keepsService.updateKeep(keep.id)
                } catch (error) {
                    Pop.error(error)
                }

            },

            async openKeepDetails(keepProp) {
                try {
                    // AppState.activeKeep = {}
                    await keepsService.openKeepDetails(keepProp)
                    Modal.getOrCreateInstance('#keepDetailsModal').show()
                    this.updateKeep()
                    vaultsService.getMyVaults()
                    // keepProp.views++
                    // this.removeKeep(keepId)
                } catch (error) {
                    Pop.error(error)
                }
            },

            async removeKeep() {
                try {
                    const keepId = props.keepProp.id
                    const yes = await Pop.confirm(`Are you sure you want to delete?`)
                    if (!yes) {
                        return
                    }
                    await keepsService.removeKeep(keepId)
                    // Modal.getInstance('#keepDetailsModal').hide()
                    Pop.success(`Your keep has been deleted.`)
                    router.push({ name: 'Home' });
                } catch (error) {
                    Pop.error(error)
                }
            },


        }
    }
};
</script>


<style lang="scss" scoped>
.backgroundImg {
    height: 25rem;
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
    height: 5dvh;
    width: 5dvh;
    object-fit: cover;
    object-position: center;
}

.img-shadow {
    border: 1px solid black;
    box-shadow: 2px 2px white;
    transition: ease-in-out 0.4s;
}

.img-shadow:hover {
    box-shadow: 2px 2px 5px white;
}
</style>