import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
    async createVault(vaultData) {
        const res = api.post('api/vaults', vaultData)
        const newVault = new Vault(res.data)
        AppState.activeVault.push(newVault)
        return newVault
    }
}

export const vaultsService = new VaultsService()