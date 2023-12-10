<!-- <template>
    <div class="container-fluid">
        <section class="row">
            <div class="col-10 col-md-6">
                hi
                <p>
                    {{ profile }}
                </p>
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

export default {
    setup() {
        const route = useRoute();
        onMounted(() => {
            getProfileById();
        });
        async function getProfileById() {
            try {
                const profileId = route.params.profileId
                await profilesService.getProfileById(profileId)
            } catch (error) {
                Pop.error(error)
            }
        }
        return {
            profile: computed(() => AppState.activeProfile),
        }
    }
};
</script>


<style lang="scss" scoped></style> -->