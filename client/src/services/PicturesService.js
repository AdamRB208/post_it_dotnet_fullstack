import { AppState } from "@/AppState.js"
import { api } from "./AxiosService.js"
import { logger } from "@/utils/Logger.js"
import { Picture } from "@/models/Picture.js"

class PicturesService {
  async getPicturesByAlbumId(albumId) {
    AppState.pictures = []
    const response = await api.get(`api/albums/${albumId}/pictures`)
    logger.log('Got Pictures!', response.data)
    const pictures = response.data.map(pojo => new Picture(pojo))
    AppState.pictures = pictures
  }

}

export const picturesService = new PicturesService()