import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Album } from "@/models/Album.js"

class AlbumsService {

  async archiveAlbum(albumId) {
    const response = await api.delete(`api/albums/${albumId}`)
    logger.log('Archived Album', response.data)
    const album = new Album(response.data)
    AppState.activeAlbum = album
  }

  async getAlbumById(albumId) {
    AppState.activeAlbum = null
    const response = await api.get(`api/albums/${albumId}`)
    logger.log('Got Album by Id!!', response.data)
    const album = new Album(response.data)
    AppState.activeAlbum = album
  }

  async createAlbum(albumData) {
    const response = await api.post('api/albums', albumData)
    logger.log('Created Album!', response.data)
    const album = new Album(response.data)
    return album
  }

  async getAlbums() {
    const response = await api.get('api/albums')
    logger.log('Got Albums!', response.data)
    const album = response.data.map(pojo => new Album(pojo))
    AppState.albums = album
  }

}

export const albumsService = new AlbumsService()