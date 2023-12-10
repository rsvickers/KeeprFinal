export class Profile {
    constructor(data) {
        this.id = data.id
        this.name = data.name
        this.picture = data.picture
        this.coverImg = data.coverImg ? data.coverImg : "https://images.unsplash.com/photo-1614850715649-1d0106293bd1?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Y292ZXJ8ZW58MHx8MHx8fDA%3D"
    }
}