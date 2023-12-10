// import { AppState } from "../AppState"
// import { Profile } from "../models/Profile"
// import { logger } from "../utils/Logger"
// import { api } from "./AxiosService"

// class ProfilesService {
//     async getProfileById(profileId) {
//         const res = await api.get(`api/profiles/${profileId}`)
//         logger.log('got profile', res.data)
//         const newProfile = res.data.map((p) => new Profile(p))
//         AppState.activeProfile = new Profile
//     }
// }

// export const profilesService = new ProfilesService()