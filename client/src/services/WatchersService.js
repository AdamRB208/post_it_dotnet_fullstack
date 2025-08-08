import { AppState } from "@/AppState.js"
import { api } from "./AxiosService.js"
import { logger } from "@/utils/Logger.js"
import { WatcherAlbum, WatcherProfile } from "@/models/Watcher.js"

class WatchersService {
  async deleteWatcher(watcherId) {
    const response = await api.delete(`/api/watchers/${watcherId}`)
    logger.log('Deleted Watcher!', response.data)
    const watcherAlbums = AppState.watcherAlbums
    const index = watcherAlbums.findIndex(watcher => watcher.id == watcherId)
    watcherAlbums.splice(index, 1)
  }

  async getMyWatchedAlbums() {
    AppState.watcherAlbums = []
    const response = await api.get('account/watchers')
    logger.log('Got Albums I am Watching', response.data)
    logger.log('Fist item response data', response.data[0])
    const watcherAlbums = response.data.map(pojo => new WatcherAlbum(pojo))
    AppState.watcherAlbums = watcherAlbums
    logger.log('Watcher Id', watcherAlbums.map(watcher => watcher.id))
  }

  async createWatcher(watcherData) {
    const response = await api.post('api/watchers', watcherData)
    logger.log('Created Watcher!', response.data)
    const watcher = new WatcherProfile(response.data)
    AppState.WatcherProfiles.push(watcher)
    AppState.activeAlbum.watcherCount++
  }

  async getWatchersByAlbumId(albumId) {
    AppState.WatcherProfiles = []
    const response = await api.get(`/api/albums/${albumId}/watchers`)
    logger.log('Got Watchers!', response.data)
    logger.log('First Watcher', response.data[0])
    const watcherProfile = response.data.map(pojo => {
      logger.log('Creating watcherProfile from:', pojo)
      return new WatcherProfile(pojo)
    })
    logger.log('Processed watchers:', watcherProfile)
    AppState.WatcherProfiles = watcherProfile
  }

}

export const watchersService = new WatchersService()