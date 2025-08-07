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

export class WatcherProfile extends Watcher {
  constructor(data) {
    super(data)
    this.profile = new Profile(data.profile)
  }
}

export class WatcherAlbum extends Watcher {
  constructor(data) {
    super(data)
    this.album = data.album ? new Album(data.album) : null
  }
}