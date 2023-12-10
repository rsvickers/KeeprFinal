import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {
    async getKeeps() {
        const res = await api.get('api/keeps')
        logger.log('GOT KEEPS', res.data)
        const newKeeps = res.data.map((k) => new Keep(k))
        AppState.keeps = newKeeps
    }

    async createKeep(keepData) {
        const res = api.post('api/keeps', keepData)
        logger.log('created a keep', res.data)
        const newKeep = new Keep(res.data)
        AppState.keeps.push(newKeep)
        return newKeep
    }

    async removeKeep(keepId) {
        const res = api.delete(`api/keeps/${keepId}`)
        AppState.activeKeep = null
    }

    async openKeepDetails(Keep) {
        AppState.activeKeep = Keep
    }

    clearAppState() {
        AppState.activeKeep = null
    }
}

export const keepsService = new KeepsService()