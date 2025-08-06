export class Watcher {
  constructor(data) {
    this.id = data.id
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
    this.accountId = data.accountId
    this.albumId = data.albumId
  }
}