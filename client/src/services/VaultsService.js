import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
    async createVault(vaultData) {
        const res = await api.post('api/vaults', vaultData)
        // logger.log("Created a vault", res.data)
        const newVault = new Vault(res.data)
        AppState.vaults.push(newVault)
        return newVault
    }

    async getVaultById(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}`)
        // logger.log('VAULTid', res.data)
        const newVault = new Vault(res.data)
        AppState.activeVault = newVault
    }

    async getMyVaults() {
        const res = await api.get('account/vaults')
        AppState.vaults = res.data.map((v) => new Vault(v))
    }

    async removeVault(vaultId) {
        const res = await api.delete(`api/vaults/${vaultId}`)
        logger.log('Removed Vault', res.data)
        AppState.activeVault = null
    }



    clearAppState() {
        AppState.activeVault = null
    }
}

export const vaultsService = new VaultsService()