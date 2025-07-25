using System.ComponentModel.DataAnnotations;

namespace post_it_dotnet_fullstack.Models;

public class Picture
{
  [Url, MaxLength(2000)] public string ImgUrl { get; set; }
  public string CreatorId { get; set; }
  public int AlbumId { get; set; }
  public Profile Creator { get; set; }
}