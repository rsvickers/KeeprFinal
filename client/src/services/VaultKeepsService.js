import { AppState } from "../AppState"
import { VaultKeep } from "../models/VaultKeep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {
    async createKeepVault(vaultKeepData) {
        // logger.log('Anything?', vaultKeepData)
        const res = await api.post('api/vaultKeeps', vaultKeepData)
        const newVaultKeep = new VaultKeep(res.data)
        AppState.activeKeep.kept++
        AppState.vaultKeeps.push(newVaultKeep)
        return newVaultKeep
    }
}

export const vaultKeepsService = new VaultKeepsService()