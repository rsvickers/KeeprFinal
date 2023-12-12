import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {
    async getKeeps() {
        const res = await api.get('api/keeps')
        // logger.log('GOT KEEPS', res.data)
        const newKeeps = res.data.map((k) => new Keep(k))
        AppState.keeps = newKeeps
    }

    async getKeepsInVault(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log('GOT KEEPS IN VAULTS', res.data)
        const newVaultKeeps = res.data.map((vk) => new VaultKeep(vk))
        // AppState.activeKeep.kept++
        AppState.vaultKeeps = newVaultKeeps

    }

    async createKeep(keepData) {
        const res = await api.post('api/keeps', keepData)
        // logger.log('created a keep', res.data)
        const newKeep = new Keep(res.data)
        // AppState.activeKeep.kept++
        AppState.keeps.push(newKeep)
        return newKeep
    }

    async updateKeep(keepId) {
        const res = await api.get(`api/keeps/${keepId}`)
        const newKeep = new Keep(res.data)
        AppState.activeKeep = newKeep
    }

    async removeKeep(keepId) {
        {
            AppState.activeKeep = {}
            await api.delete(`api/keeps/${keepId}`)
            const keepIndex = AppState.keeps.findIndex(keep => keep.id == keepId)
            AppState.keeps.splice(keepIndex, 1)
        }

        // this.clearAppState()
    }

    async openKeepDetails(Keep) {
        // AppState.activeKeep.views++
        AppState.activeKeep = Keep
    }

    clearAppState() {
        AppState.activeKeep = null
    }
}

export const keepsService = new KeepsService()