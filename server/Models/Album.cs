using System.ComponentModel.DataAnnotations;
namespace post_it_dotnet_fullstack.Models;

public class Album : RepoItem<int>
{
  [MinLength(3), MaxLength(25)] public string Title { get; set; }
  [MinLength(15), MaxLength(250)] public string Description { get; set; }
  [Url, MaxLength(2000)] public string CoverImg { get; set; }
  public bool Archived { get; set; }
  public string Category { get; set; }
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
}