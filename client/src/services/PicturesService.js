import { AppState } from "@/AppState.js"
import { api } from "./AxiosService.js"
import { logger } from "@/utils/Logger.js"
import { Picture } from "@/models/Picture.js"

class PicturesService {
  async createPicture(pictureData) {
    const response = await api.post('api/pictures', pictureData)
    logger.log('Created Picture!', response.data)
    const picture = new Picture(response.data)
    AppState.pictures.push(picture)
  }

  async getPicturesByAlbumId(albumId) {
    AppState.pictures = []
    const response = await api.get(`api/albums/${albumId}/pictures`)
    logger.log('Got Pictures!', response.data)
    const pictures = response.data.map(pojo => new Picture(pojo))
    AppState.pictures = pictures
  }

}

export const picturesService = new PicturesService()