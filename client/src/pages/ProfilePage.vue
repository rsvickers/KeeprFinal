<template>
    <div class="container-fluid">
        <section class="row justify-content-center">
            <div class="col-10 col-md-6 text-center">
                <!-- TODO add coverImg after allowing update to profile -->
                <img class="coolImg" :src="profile.coverImg" alt="">
            </div>

            <div class="absolute ">
                <div class="d-flex justify-content-center">
                    <img class="profile" :src="profile.picture" alt="" title="profile picture">
                </div>
                <div class="text-center mt-3">
                    <h3>{{ profile.name }}</h3>
                    <p>{{ vaults.length }} Vaults || {{ keeps.length }} Keeps</p>
                </div>
            </div>

            <h2>Vaults</h2>
            <div v-for="vault in vaults" :key="vault.id" class="col-md-3 col-6">
                <VaultCardComponent :vaultProp="vault" />
            </div>
            <h2>Keeps</h2>
            <div v-for="keep in keeps" :key="keep.id" class="col-md-3 col-6">
                <KeepCardComponent :keepProp="keep" />
            </div>
        </section>
    </div>
</template>


<script>
import { useRoute } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watchEffect } from 'vue';
import Pop from '../utils/Pop';
import { profilesService } from '../services/ProfilesService'
import KeepCardComponent from '../components/KeepCardComponent.vue';
import VaultCardComponent from '../components/VaultCardComponent.vue'
import { Modal } from 'bootstrap';


export default {
    setup() {
        const route = useRoute();
        onMounted(() => {
            getProfileById();
            getKeepsByProfile();
            getVaultsByProfile();
        });
        async function getProfileById() {
            try {
                const profileId = route.params.profileId;
                await profilesService.getProfileById(profileId);
                Modal.getOrCreateInstance('#keepDetailsModal').hide()
            }
            catch (error) {
                Pop.error(error);
            }
        }

        async function getKeepsByProfile() {
            try {
                const profileId = route.params.profileId;
                await profilesService.getKeepsByProfile(profileId)
            } catch (error) {
                Pop.error(error)
            }
        }

        async function getVaultsByProfile() {
            try {
                const profileId = route.params.profileId;
                await profilesService.getVaultsByProfile(profileId)
            } catch (error) {
                Pop.error(error)
            }
        }

        return {
            profile: computed(() => AppState.activeProfile),
            keeps: computed(() => AppState.keeps),
            vaults: computed(() => AppState.vaults)
        };
    },
    components: { KeepCardComponent, VaultCardComponent }
};
</script>


<style lang="scss" scoped>
.profile {
    height: 15dvh;
    width: 15dvh;
    border-radius: 5rem;
}

h2 {
    margin-top: 10dvh;
    margin-left: 5vw;
}

.coolImg {
    height: 25dvh;
    width: 100%;
}

.absolute {
    position: absolute;
    left: 1%;
    top: 23%;
}
</style>