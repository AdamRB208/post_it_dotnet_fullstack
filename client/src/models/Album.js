import { Profile } from "./Account.js"

export class Album {
  constructor(data) {
    this.id = data.id
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
    this.title = data.title
    this.coverImg = data.coverImg
    this.archived = data.archived
    this.description = data.description
    this.category = data.category
    this.creatorId = data.creatorId
    this.creator = new Profile(data.creator)
  }
}