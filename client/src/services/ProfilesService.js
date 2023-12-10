import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { Profile } from "../models/Profile"
import { Vault } from "../models/Vault"
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

    async getVaultsByProfile(profileId) {
        const res = await api.get(`api/profiles/${profileId}/vaults`)
        logger.log("GOT VAULTS", res.data)
        AppState.vaults = res.data.map((v) => new Vault(v))
    }
}

export const profilesService = new ProfilesService()