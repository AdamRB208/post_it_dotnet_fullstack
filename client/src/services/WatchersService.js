import { AppState } from "@/AppState.js"
import { api } from "./AxiosService.js"
import { logger } from "@/utils/Logger.js"
import { WatcherAlbum } from "@/models/Watcher.js"

class WatchersService {
  async getWatchersByAlbumId(albumId) {
    AppState.watcherAlbums = []
    const response = await api.get(`/api/albums/${albumId}/watchers`)
    logger.log('Got Watchers!', response.data)
    const watcherAlbums = response.data.map(pojo => new WatcherAlbum(pojo))
    AppState.watcherAlbums = watcherAlbums
  }

}

export const watchersService = new WatchersService()