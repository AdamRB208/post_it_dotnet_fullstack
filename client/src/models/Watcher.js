import { Profile } from "./Account.js"
import { Album } from "./Album.js"

class Watcher {
  constructor(data) {
    this.id = data.id
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
    this.accountId = data.accountId
    this.albumId = data.albumId
  }
}

export class WatcherProfile extends Profile {
  constructor(data) {
    super(data)
    this.watcherId = data.watcherId
    this.albumId = data.albumId
  }
}

export class WatcherAlbum extends Album {
  constructor(data) {
    super(data)
    this.watcherId = data.watcherId
    this.accountId = data.accountId
  }
}