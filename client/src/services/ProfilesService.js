import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { Profile } from "../models/Profile"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService {
    async getProfileById(profileId) {
        const res = await api.get(`api/profiles/${profileId}`)
        // logger.log('got profile', res.data)
        AppState.activeProfile = new Profile(res.data)
    }

    async getKeepsByProfile(profileId) {
        const res = await api.get(`api/profiles/${profileId}/keeps`)
        AppState.keeps = res.data.map((k) => new Keep(k))
    }
}

export const profilesService = new ProfilesService()