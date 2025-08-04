import { Profile } from "./Account.js"

export class Picture {
  constructor(data) {
    this.id = data.id
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
    this.imgUrl = data.imgUrl
    this.albumId = data.albumId
    this.creatorId = data.creatorId
    this.creator = new Profile(data.creator)
  }
}