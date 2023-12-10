export class Vault {
    constructor(data) {
        this.id = data.id || data._id
        this.creatorId = data.creatorId
        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.isPrivate = data.isPrivate
        this.creator = data.creator
    }
}