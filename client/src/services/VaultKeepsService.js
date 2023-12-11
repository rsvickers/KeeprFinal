import { logger } from "../utils/Logger"

class VaultKeepsService {
    async createKeepVault(vaultKeepData) {
        logger.log('Anything?', vaultKeepData)
    }
}

export const vaultKeepsService = new VaultKeepsService()