import { AppState } from "@/AppState.js"
import { api } from "./AxiosService.js"
import { logger } from "@/utils/Logger.js"
import { WatcherProfile } from "@/models/Watcher.js"

class WatchersService {
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