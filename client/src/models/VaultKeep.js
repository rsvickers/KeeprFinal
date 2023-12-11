import { Keep } from "./Keep";

export class VaultKeep extends Keep {
    constructor(data) {
        super(data)
        this.vaultKeepId = data.vaultKeepId
    }
}