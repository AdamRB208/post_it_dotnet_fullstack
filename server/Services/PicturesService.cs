


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

  private Picture GetPictureById(int pictureId)
  {
    Picture picture = _picturesRepository.GetPictureById(pictureId);
    if (picture == null)
    {
      throw new Exception("Invalid picture Id: " + pictureId);
    }
    return picture;
  }

  internal void DeletePicture(int pictureId, Account userInfo)
  {
    Picture picture = GetPictureById(pictureId);

    if (picture.CreatorId != userInfo.Id)
    {
      throw new Exception($"YOU CAN NOT DELETE ANOTHER USER'S PICTURE, {userInfo.Name.ToUpper()}!");
    }
    _picturesRepository.DeletePicture(pictureId);
  }
}