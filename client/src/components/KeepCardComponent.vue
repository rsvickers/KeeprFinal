<template>
    <div class="backgroundImg mb-5 mt-4  rounded text-light d-flex" role="button" title="click to see details"
        :style="{ backgroundImage: `url(${keepProp?.img})` }">

        <div class="d-flex justify-content-between align-items-end">
            <p class="p-3 box rounded">{{ keepProp?.name }}</p>
            <img class="avatar" :src="keepProp?.creator.picture" alt="">
        </div>

    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from '../models/Keep';
export default {
    props: {
        keepProp: { type: Keep, required: true },
    },
    setup() {
        return {
            account: computed(() => AppState.account),
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
</style>