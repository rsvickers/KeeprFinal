import { Logger } from "sass"
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

    async removeVaultKeep(keepId) {
        const vaultKeep = AppState.vaultKeeps.find((vk) => vk.id == keepId);

        if (vaultKeep) {
            const { vaultKeepId } = vaultKeep;
            AppState.vaultKeeps = AppState.vaultKeeps.filter((vk) => vk.id != keepId);
            await api.delete(`api/vaultKeeps/${vaultKeepId}`);
        }
    }
}

export const vaultKeepsService = new VaultKeepsService()