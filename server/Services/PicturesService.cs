

namespace post_it_dotnet_fullstack.Services;

public class PicturesService
{
  public PicturesService(PicturesRepository picturesRepository)
  {
    _picturesRepository = picturesRepository;
  }
  private readonly PicturesRepository _picturesRepository;

  internal Picture CreatePicture(Picture pictureData)
  {
    Picture picture = _picturesRepository.CreatePicture(pictureData);
    return picture;
  }

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    List<Picture> pictures = _picturesRepository.GetPicturesByAlbumId(albumId);
    return pictures;
  }
}